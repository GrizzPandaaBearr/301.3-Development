using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301._3_Development.models
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }

        public DoctorDTO? Doctor { get; set; }
        public AdminDTO? Admin { get; set; }
        public PatientDTO? Patient { get; set; }
    }
}
