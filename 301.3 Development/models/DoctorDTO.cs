using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301._3_Development.models
{
    public class DoctorDTO
    {
        public int DoctorID { get; set; }
        public string Name_First { get; set; }
        public string Name_Last { get; set; }
        public string Specialization { get; set; }
        public string LicenseNumber { get; set; }
        public string OfficePhone { get; set; }
        public int ExperienceYears { get; set; }
    }
}
