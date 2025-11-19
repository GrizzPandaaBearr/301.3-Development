using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301._3_Development.models
{
    public class ActivityLogDTO
    {
        public int LogID { get; set; }
        public int UserID { get; set; }
        public string Action { get; set; } = "";
        public DateTime? Timestamp { get; set; } // nullable
    }
}
