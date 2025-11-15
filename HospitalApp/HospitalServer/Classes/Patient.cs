public class Patient
{
    public int PatientID { get; set; } // FK to User.UserID
    public string? Birth_Place { get; set; }
    public DateTime? Birth_Date { get; set; }
    public string? Sex { get; set; } // 'M', 'F', 'Other'
    public int? DoctorID { get; set; } // FK to User.UserID
    public string? Medical_History { get; set; }

    public User? User { get; set; }
}