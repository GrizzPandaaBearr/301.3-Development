using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301._3_Development.models
{
    internal class UserAccount
    {
        public required string Name { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}