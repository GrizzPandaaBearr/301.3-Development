using System.Configuration;
using System.Data;
using System.Windows;
using _301._3_Development.Security;

namespace _301._3_Development
{
    public partial class App : Application
    {
        public static byte[] AppEncryptionKey { get; private set; }

        public static string AdminUsername = "admin";
        public static string AdminPassword = "admin123";

        public static string RegisteredUsername { get; set; }
        public static string RegisteredPassword { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AppEncryptionKey = KeyManager.GetOrCreateKey();
        }
    } 
}