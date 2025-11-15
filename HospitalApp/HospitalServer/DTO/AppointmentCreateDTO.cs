public class AppointmentCreateDTO
{
    public int PatientID { get; set; }
    public int DoctorID { get; set; }
    public DateTime Date { get; set; } 
    public string Reason { get; set; }
}