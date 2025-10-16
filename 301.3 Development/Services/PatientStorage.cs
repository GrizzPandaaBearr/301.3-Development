using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using _301._3_Development.Security;

namespace _301._3_Development.Services
{
    public static class PatientStorage
    {
        private static readonly string FilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "_301._3_Development",
            "patients.enc");
    }
}
