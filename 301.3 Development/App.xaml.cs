using System.Configuration;
using System.Data;
using System.Windows;

namespace _301._3_Development
{
    public partial class App : Application
    {
        public static string AdminUsername = "admin";
        public static string AdminPassword = "admin123";

        public static string RegisteredUsername { get; set; }
        public static string RegisteredPassword { get; set; }

        public static byte[] AppEncryptionKey { get; private set; }
        
    }
}
