using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301._3_Development.models
{
    public class AppointmentDTO
    {
        public int AppointmentID { get; set; }
        public DateTime Appointment_Date { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string DoctorName { get; set; }
        public string Specialization { get; set; }
        public DoctorDTO Doctor { get; set; }
        public PatientDTO Patient { get; set; }
    }
}
