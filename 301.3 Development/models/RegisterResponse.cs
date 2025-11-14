using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301._3_Development.models
{
    public class RegisterResponse
    {
        public string Message { get; set; }
        public string Token { get; set; } // If you want to auto-login after registration
        public UserDTO User { get; set; } // Optional, your user DTO from the server
    }
}
