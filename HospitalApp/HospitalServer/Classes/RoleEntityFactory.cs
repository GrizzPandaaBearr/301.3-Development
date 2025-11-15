public class RoleEntityFactory : IRoleEntityFactory
{
    private readonly MyDbContext _context;

    public RoleEntityFactory(MyDbContext context)
    {
        _context = context;
    }

    public async Task CreateForUserAsync(User user)
    {
        switch (user.Role)
        {
            case "Patient":
                _context.Patients.Add(new Patient { PatientID = user.UserID });
                break;

            case "Doctor":
                _context.Doctors.Add(new Doctor { 
                    DoctorID = user.UserID,
                    Specialization = "default",
                    License_Number = "default",
                    Office_Phone = "default",
                    Experience_Years = 1 
                    });
                break;

            case "Admin":
                _context.Admins.Add(new Admin { AdminID = user.UserID });
                break;
        }

        await _context.SaveChangesAsync();
    }
}