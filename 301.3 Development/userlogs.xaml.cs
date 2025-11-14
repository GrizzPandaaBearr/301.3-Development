using System.Linq;
using System.Windows;
using System.Windows.Controls;
using _301._3_Development.models;
using _301._3_Development.Security;
using _301._3_Development.Services;

namespace _301._3_Development
{
    public partial class userlogs : Page
    {
        private readonly AesGcmEncryptionService _encService;

        public userlogs()
        {
            InitializeComponent();
            _encService = new AesGcmEncryptionService(App.AppEncryptionKey);
            LoadLogs();
        }
        private void LoadLogs()
        {
            /*var users = EncryptedStorage.LoadEncrypted<UserDTO>(_encService);
            var display = users.Select(u =>
            {
                var pwd = "[decryption error]";
                try { pwd = _encService.DecryptString(u.PasswordHash); }
                catch { }
                return new {Username = u.Username, DecryptedPassword = pwd };
            }).ToList();

            LogsDataGrid.ItemsSource = display;*/
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadLogs();
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new adminscreen());
        }
    }
}