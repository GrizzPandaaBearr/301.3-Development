namespace _301._3_Development.Models
{
    public class Patient
    {
        public required string Name { get; set; }
        public required string ContactNo { get; set; }
        public required string PassportNo { get; set; }
        public required string AppointmentDate { get; set; }
        public required string PlaceOfBirth { get; set; }
        public required string DateOfBirth { get; set; }
        public required string Sex { get; set; }


        public required string EmergencyName { get; set; }
        public required string EmergencyContact { get; set; }
        public required string EmergencyRelation { get; set; }


        public bool PickupYes { get; set; }
        public bool PickupNo { get; set; }
        public required string ComeBy { get; set; }
        public required string ETA { get; set; }


        public bool Inpatient { get; set; }
        public bool Outpatient { get; set; }
        public bool MedicalCheckUp { get; set; }
        public required string Doctor { get; set; }
        public required string Remark { get; set; }

    }
}