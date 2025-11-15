public class Appointment
{
    public int AppointmentID { get; set; }
    public int PatientID { get; set; }
    public int DoctorID { get; set; }
    public DateTime Appointment_Date { get; set; }
    public string Status { get; set; } = "Scheduled"; // Enum: Scheduled, Completed, Cancelled
    public string? Notes { get; set; }
    public DateTime Created_At { get; set; }

    public Doctor? Doctor {get; set;}
    public Patient? Patient {get;set;}
}