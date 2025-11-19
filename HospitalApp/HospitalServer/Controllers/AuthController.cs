using Microsoft.AspNetCore.Mvc;
using SecureRemotePassword;
using Eneter.SecureRemotePassword;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private static readonly SrpServer SrpServer = new SrpServer();
    private readonly MyDbContext _context;
    private readonly IConfiguration _config;
    private readonly IRoleEntityFactory _roleFactory;
    private readonly EncryptionService _encryption;

    public AuthController(IConfiguration config ,MyDbContext context, IRoleEntityFactory rolefactory, EncryptionService encryption)
    {
        _config = config;
        _context = context;
        _roleFactory = rolefactory;
        _encryption = encryption;
    }


    //SaltCreator
    public static byte[] CreateSalt(int size = 16)
    {
        var salt = new byte[size];
        RandomNumberGenerator.Fill(salt);
        return salt;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _context.Users
            .Include(u => u.Patient)
            .Include(u => u.Doctor)
            .Include(u => u.Admin)
            .FirstOrDefaultAsync(u => u.Email == model.Email);

        if (user == null)
            return Unauthorized("Invalid email or password");

        var salt = user.Salt;
        var hash = Convert.FromHexString(user.PasswordHash);

        var derivedKey = Rfc2898DeriveBytes.Pbkdf2(
            model.Password,
            salt,
            100000,
            HashAlgorithmName.SHA256,
            32
        );

        if (!CryptographicOperations.FixedTimeEquals(derivedKey, hash))
            return Unauthorized("Invalid credentials");

        var token = GenerateJwtToken(user.UserID.ToString(), user.Role);
        //var roleData = GetRoleData(user);
        UserDTO userDTO = await BuildUserDTO(user);
        await LogActivity(userDTO.UserID,"User Logged in");
        return Ok(new
        {
            Message = "Login success",
            Token = token,
            User = userDTO
            
        });
    }

    [Authorize]
    [HttpGet("patient/export/{userId}")]
    public async Task<IActionResult> ExportPatient(int userId)
    {
        // Load user + patient details
        var user = await _context.Users
            .Include(u => u.Patient)
            .FirstOrDefaultAsync(u => u.UserID == userId);

        if (user == null || user.Patient == null)
            return NotFound("Patient not found");

        // Load all appointments for this patient
        var appointments = await _context.Appointments
            .Where(a => a.PatientID == user.Patient.PatientID)
            .Select(a => new
            {
                a.AppointmentID,
                a.DoctorID,
                a.Appointment_Date,
                a.Notes,
                a.Status
            })
            .ToListAsync();

        // Build export object
        string medicalHistory = _encryption.Decrypt(user.Patient.Medical_History);
        var exportData = new 
        {
            User = new
            {
                user.UserID,
                user.Email,
                user.Name_First,
                user.Name_Last,
                user.Phone,
                user.Role,
                user.Created_At
            },
            Patient = new
            {
                user.Patient.PatientID,
                user.Patient.Birth_Date,
                user.Patient.Birth_Place,
                user.Patient.Sex,
                medicalHistory,
                user.Patient.DoctorID
            },
            Appointments = appointments
        };
        await LogActivity(userId,"User Exported User data");
        return Ok(exportData);
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                return BadRequest("Email already registered");

            var (hash, salt) = HashPassword(model.Password);
            Console.WriteLine("Passed email check and pass hashing/salting");
            var newUser = new User
            {
                Email = model.Email,
                PasswordHash = hash,
                Salt = salt,
                Name_First = model.FirstName,
                Name_Last = model.LastName,
                Phone = model.Phone,
                Role = model.Role ?? "Patient",
                Created_At = DateTime.UtcNow,
                Updated_At = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            
            Console.WriteLine("TRYING NEW ROLE CREATION");

            // Create corresponding role entry
            switch (model.Role)
            {
                case "Patient":
                    if (model.Patient != null)
                    {
                        var patient = new Patient
                        {
                            PatientID = newUser.UserID,
                            Birth_Place = model.Patient.BirthPlace,
                            Birth_Date = model.Patient.BirthDate ?? null,
                            Sex = model.Patient.Sex,
                            DoctorID = model.Patient.DoctorID,
                            Medical_History = _encryption.Encrypt(model.Patient.MedicalHistory) 

                        };
                        _context.Patients.Add(patient);
                        newUser.Patient = patient;
                        break;
                    }
                    return BadRequest("user.patient == null");
                    

                case "Doctor":
                    if(model.Doctor != null)
                    {
                        var doctor = new Doctor
                        {
                            DoctorID = newUser.UserID,
                            Specialization = model.Doctor.Specialization,
                            License_Number = model.Doctor.LicenseNumber,
                            Office_Phone = model.Doctor.OfficePhone,
                            Experience_Years = model.Doctor.ExperienceYears

                        };
                        _context.Doctors.Add(doctor);
                        newUser.Doctor = doctor;
                        break;
                    }
                    return BadRequest("user.doctor == null");
                    

                case "Admin":
                    if(model.Admin != null)
                    {
                        var admin = new Admin
                        {
                            AdminID = newUser.UserID,
                            Access_Level = model.Admin.AccessLevel
                        };
                        _context.Admins.Add(admin);
                        newUser.Admin = admin;
                        break;
                    }
                    return BadRequest("user.admin == null");
                default:
                return BadRequest("Something went wrong in role creation");
            }
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            var token = GenerateJwtToken(newUser.UserID.ToString(), newUser.Role);
            var roleData = GetRoleData(newUser);
            await LogActivity(newUser.UserID,$"User Registered as {model.Role}");
            return Ok(new
            {
                message = $"Registered as {newUser.Name_First}, Role = {newUser.Role}",
                accessToken = token,
                roleData
            });
        }
        catch(Exception ex)
        {
            //Roll back changes if anything fails, preserve data integrity
            await transaction.RollbackAsync();
            return BadRequest(ex.Message);
        }
        
    }

    [Authorize]
    [HttpPost("book")]
    public async Task<IActionResult> BookAppointment([FromBody] AppointmentCreateDTO dto)
    {
        var patient = await _context.Patients.FindAsync(dto.PatientID);
        if (patient == null)
            return BadRequest("Invalid patient");

        var doctor = await _context.Doctors.FindAsync(dto.DoctorID);
        if (doctor == null)
            return BadRequest("Invalid doctor");

        var appointment = new Appointment
        {
            PatientID = dto.PatientID,
            DoctorID = dto.DoctorID,
            Appointment_Date = dto.Date,
            Notes = dto.Reason,
            Status = "Scheduled",
            Created_At = DateTime.UtcNow
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        await LogActivity(dto.PatientID, $"User Booked an appointment with doctor:{dto.DoctorID}, on {dto.Date}");

        return Ok(new
        {
            Message = "Appointment booked",
            Appointment = appointment
        });
    }

    
    private async Task<UserDTO> BuildUserDTO(User user)
    {
        var dto = new UserDTO
        {
            UserID = user.UserID,
            Email = user.Email,
            Role = user.Role,
            Name_First = user.Name_First,
            Name_Last = user.Name_Last,
            Phone = user.Phone
        };

        // Load Patient Data
        if (user.Role == "Patient")
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.PatientID == user.UserID);

            if (patient != null)
            {
                dto.Patient = new PatientDTO
                {
                    PatientID = patient.PatientID,
                    BirthPlace = patient.Birth_Place,
                    BirthDate = patient.Birth_Date,
                    Sex = patient.Sex,
                    DoctorID = patient.DoctorID,
                    MedicalHistory = _encryption.Decrypt(patient.Medical_History)
                };
            }
        }

        // Load Doctor Data
        if (user.Role == "Doctor")
        {
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.DoctorID == user.UserID);

            if (doctor != null)
            {
                dto.Doctor = new DoctorDTO
                {
                    DoctorID = doctor.DoctorID,
                    Specialization = doctor.Specialization,
                    LicenseNumber = doctor.License_Number,
                    OfficePhone = doctor.Office_Phone,
                    ExperienceYears = doctor.Experience_Years
                };
            }
        }

        // Load Admin Data
        if (user.Role == "Admin")
        {
            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.AdminID == user.UserID);

            if (admin != null)
            {
                dto.Admin = new AdminDTO
                {
                    AdminID = admin.AdminID,
                    AccessLevel = admin.Access_Level
                };
            }
        }

        return dto;
    }
    private object? GetRoleData(User user)
    {
        switch (user.Role)
        {
            case "Patient":
                return user.Patient == null ? null : new
                {
                    user.Patient.PatientID,
                    user.Patient.Birth_Place,
                    user.Patient.Birth_Date,
                    user.Patient.Sex,
                    user.Patient.Medical_History,
                    AssignedDoctor = user.Patient.DoctorID
                };

            case "Doctor":
                return user.Doctor == null ? null : new
                {
                    user.Doctor.DoctorID,
                    user.Doctor.Specialization,
                    user.Doctor.License_Number,
                    user.Doctor.Office_Phone,
                    user.Doctor.Experience_Years,
                    Patients = _context.Patients
                        .Where(p => p.DoctorID == user.UserID)
                        .Select(p => new
                        {
                            p.PatientID,
                            p.Birth_Date,
                            p.Sex
                        })
                        .ToList()
                };

            case "Admin":
                return user.Admin == null ? null : new
                {
                    user.Admin.AdminID,
                    user.Admin.Access_Level,
                    RecentLogs = _context.ActivityLogs
                        .OrderByDescending(l => l.Timestamp)
                        .Take(20)
                        .Select(l => new
                        {
                            l.LogID,
                            l.UserID,
                            l.Action,
                            l.Timestamp
                        })
                        .ToList()
                };

            default:
                return null;
        }
    }
    public static (string hash, byte[] salt) HashPassword(string password)
    {
        var salt = CreateSalt(16);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100000, HashAlgorithmName.SHA256, 32);

        return (Convert.ToHexString(hash), salt);
    }

    public static bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        var saltBytes = Convert.FromHexString(storedSalt);
        var computedHash = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, 100000, HashAlgorithmName.SHA256, 32);

        return CryptographicOperations.FixedTimeEquals(computedHash, Convert.FromHexString(storedHash));
    }

    private string GenerateJwtToken(string userId, string? role =null)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId)
        };
        if (!string.IsNullOrEmpty(role))
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],

            audience: _config["Jwt:Audience"],

            claims: claims,

            expires: DateTime.UtcNow.AddMinutes(15),

            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    string PadBase64(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return base64.Replace('-', '+').Replace('_', '/');
    }

    [Authorize]
    [HttpGet("logs")]
    public async Task<IActionResult> GetLogs()
    {
        //TESTING JWT TOKEN
        var rawToken = Request.Headers["Authorization"]
        .ToString()
        .Replace("Bearer ", "");
        Console.WriteLine("Raw Token:");
        Console.WriteLine(rawToken);

        var payload = rawToken.Split('.')[1];
        string jsonraw = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(PadBase64(payload)));
        var jsonPretty = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonraw);
        Console.WriteLine("Token Decoded:\n");

        foreach (var kvp in jsonPretty)
        {
            string key = kvp.Key switch
            {
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" => "UserId",
                "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" => "Role",
                _ => kvp.Key
            };

            Console.WriteLine($"{key}: {kvp.Value}");
        }

        var logs = await _context.ActivityLogs
            .OrderByDescending(l => l.Timestamp)
            .Take(100) // limit to recent 100
            .Select(l => new ActivityLogDTO
            {
                LogID = l.LogID,
                UserID = l.UserID,
                Action = l.Action,
                Timestamp = l.Timestamp 
            })
            .ToListAsync();

        return Ok(logs);
    }
    private async Task LogActivity(int userId, string action)
    {
        var log = new ActivityLog
        {
            UserID = userId,
            Action = action,
            Timestamp = DateTime.UtcNow
        };
        try
        {
            _context.ActivityLogs.Add(log);
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {

            Console.WriteLine("Error occured in logging",ex.Message);
        }
        
    }

}


