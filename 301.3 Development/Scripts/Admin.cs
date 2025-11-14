using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301._3_Development.Scripts
{
    public class Admin
    {
        public int AdminID { get; set; } // FK to User.UserID
        public string? Access_Level { get; set; } // 'Low', 'Medium', 'High'

        public User? User { get; set; }
    }
}
