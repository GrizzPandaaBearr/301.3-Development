using _301._3_Development.models;
using _301._3_Development.Scripts.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _301._3_Development.Pages.AdminPages
{
    /// <summary>
    /// Interaction logic for AdminLogsPage.xaml
    /// </summary>
    public partial class AdminLogsPage : Page
    {
        private readonly UserDTO _user;

        public AdminLogsPage(UserDTO user)
        {
            InitializeComponent();
            _user = user;

            Header.Text = "System Logs";
            LoadLogs();
        }
        private async void LoadLogs()
        {
            try
            {
                var logs = await SessionManager.Instance.Api.GetAsync<List<ActivityLogDTO>>("auth/logs");
                LogsList.ItemsSource = logs;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load logs: {ex.Message}");
            }
        }
    }
}
