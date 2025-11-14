using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301._3_Development.models
{
    public class PatientDTO
    {
        public int PatientID { get; set; }
        public string BirthPlace { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Sex { get; set; }
        public int? DoctorID { get; set; }
        public string MedicalHistory { get; set; }
    }

}
