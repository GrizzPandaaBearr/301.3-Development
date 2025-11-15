public class UserDTO
{
    public int UserID { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string Name_First { get; set; }
    public string Name_Last { get; set; }
    public string Phone { get; set; }

    public PatientDTO Patient { get; set; }
    public DoctorDTO Doctor { get; set; }
    public AdminDTO Admin { get; set; }
}