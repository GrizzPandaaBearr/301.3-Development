using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _301._3_Development.Scripts;

namespace _301._3_Development.models
{
    public class LoginResponse
    {
        public string Message { get; set; }
        public string Token { get; set; }
        public UserDTO User { get; set; }  
        
    }
}
