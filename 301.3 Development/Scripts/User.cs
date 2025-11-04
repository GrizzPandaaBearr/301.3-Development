using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301._3_Development.Scripts
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        
        public enum UserRole
        {
            Admin,
            Doctor,
            Patient
        }

        public UserRole Role {  get; set; }
        public string Phone { get; set; }


        public void SetDummyUser()
        {
            Id = 0;
            FirstName = "DummyFirstName";
            LastName = "DummyLastName";
            Email = "DummyFirstName@gmail.com";
            PasswordHash = "DummyPasswordHash";
            Role = UserRole.Patient;
            Phone = "092843752787";
        }
    }

    
}
