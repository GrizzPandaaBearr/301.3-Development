public class RegisterModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string? Role { get; set; }

    public DoctorDTO? Doctor { get; set; }
    public PatientDTO? Patient { get; set; }
    public AdminDTO? Admin { get; set; }
}