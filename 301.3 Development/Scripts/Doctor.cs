using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301._3_Development.Scripts
{
    public class Doctor
    {
        public int DoctorID { get; set; } // FK to User.UserID
        public string? Specialization { get; set; }
        public string? License_Number { get; set; }
        public string? Office_Phone { get; set; }
        public int? Experience_Years { get; set; }

        public User? User { get; set; }
    }
}
