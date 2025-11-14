using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301._3_Development.Scripts
{
    public class User
    {
        public int UserID { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Name_First { get; set; } = null!;
        public string Name_Last { get; set; } = null!;
        public string? Phone { get; set; }
        public byte[] Verifier { get; set; } = Array.Empty<byte>();
        public byte[] Salt { get; set; } = Array.Empty<byte>();
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }

        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
        public Admin? Admin { get; set; }
        public List<ActivityLog> ActivityLogs { get; set; } = new();
    }


}
