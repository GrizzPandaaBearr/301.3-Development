using Microsoft.AspNetCore.Mvc;
using SecureRemotePassword;
using Eneter.SecureRemotePassword;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly MyDbContext _context;

    public AppointmentsController(MyDbContext context)
    {
        _context = context;
    }
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusModel model)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
            return NotFound("Appointment not found.");

        // Validate status
        var valid = new[] { "Scheduled", "Completed", "Cancelled" };
        if (!valid.Contains(model.Status))
            return BadRequest("Invalid status.");

        appointment.Status = model.Status;
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Status updated" });
    }
    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> CancelAppointment(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
            return NotFound("Appointment not found");

        appointment.Status = "Cancelled";
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Appointment cancelled" });
    }

    [HttpGet("doctor/{doctorId}/patients")]
    public async Task<IActionResult> GetPatientsForDoctor(int doctorId)
    {
        var patients = await _context.Appointments
            .Include(a => a.Patient)
                .ThenInclude(p => p.User)
            .Where(a => a.DoctorID == doctorId)
            .Select(a => new
            {
                a.Patient.PatientID,
                FirstName = a.Patient.User.Name_First,
                LastName = a.Patient.User.Name_Last,
                Phone = a.Patient.User.Phone,
                Email = a.Patient.User.Email
            })
            .Distinct()
            .ToListAsync();

        return Ok(patients);
    }
    [HttpGet("doctor/{doctorId}")]
    public async Task<IActionResult> GetDoctorAppointments(int doctorId)
    {
        var appointments = await _context.Appointments
            .Where(a => a.DoctorID == doctorId)
            .Include(a => a.Patient)
                .ThenInclude(p => p.User) // needed for patient's full name
            .Select(a => new AppointmentDTO
            {
                AppointmentID = a.AppointmentID,
                PatientName = a.Patient.User.Name_First + " " + a.Patient.User.Name_Last,
                Appointment_Date = a.Appointment_Date,
                Status = a.Status
            })
            .ToListAsync();

        return Ok(appointments);
    }

    // GET: api/appointments/patient/5
    [HttpGet("patient/{patientId}")]
    public async Task<IActionResult> GetAppointmentsForPatient(int patientId)
    {
        var appointments = await _context.Appointments
            .Include(a => a.Doctor)
                .ThenInclude(d => d.User) // Include doctor's user info
            .Where(a => a.PatientID == patientId)
            .Select(a => new
            {
                a.AppointmentID,
                a.Appointment_Date,
                a.Status,
                a.Notes,
                DoctorName = a.Doctor.User.Name_First + " " + a.Doctor.User.Name_Last,
                a.Doctor.Specialization
            })
            .ToListAsync();

        return Ok(appointments);
    }
    
}
public class UpdateStatusModel
{
    public string Status {get;set;}
} 