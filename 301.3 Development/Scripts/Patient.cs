using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301._3_Development.Scripts
{
    class Patient : User
    {
        public int PatientID { get; set; }
        public string Birth_Place { get; set; }
        public string Birth_Date { get; set; }
        public string Sex { get; set; }
        public int DoctorID { get; set; }
        public string Medical_History { get; set; }
             
    }
}
