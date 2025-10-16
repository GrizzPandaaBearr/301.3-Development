using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301._3_Development.models
{
    public class UserDTO
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string EncryptedPassword { get; set; }
    }
}
