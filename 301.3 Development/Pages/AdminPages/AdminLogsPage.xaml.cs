using _301._3_Development.models;
using _301._3_Development.Scripts.Session;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public ObservableCollection<ActivityLogDTO> AllLogs { get; set; }
        private ICollectionView _logView;
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
                var view = await SessionManager.Instance.Api.GetAsync<List<ActivityLogDTO>>("auth/logs");
                _logView = CollectionViewSource.GetDefaultView(view);
                LogsList.ItemsSource = _logView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load logs: {ex.Message}");
            }
        }

        private void SortCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_logView == null) return;

            _logView.SortDescriptions.Clear();

            switch (SortCombo.SelectedIndex)
            {
                case 0: // Timestamp Newest
                    _logView.SortDescriptions.Add(new SortDescription(nameof(ActivityLogDTO.Timestamp), ListSortDirection.Descending));
                    break;

                case 1: // Timestamp Oldest
                    _logView.SortDescriptions.Add(new SortDescription(nameof(ActivityLogDTO.Timestamp), ListSortDirection.Ascending));
                    break;

                case 2: // UserID A→Z
                    _logView.SortDescriptions.Add(new SortDescription(nameof(ActivityLogDTO.UserID), ListSortDirection.Ascending));
                    break;

                case 3: // UserID Z→A
                    _logView.SortDescriptions.Add(new SortDescription(nameof(ActivityLogDTO.UserID), ListSortDirection.Descending));
                    break;
            }

            _logView.Refresh();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = SearchBox.Text.ToLower();

            _logView.Filter = (item) =>
            {
                if (item is ActivityLogDTO log)
                {
                    return log.Action.ToLower().Contains(query)
                           || log.UserID.ToString().Contains(query)
                           || log.Timestamp.ToString().Contains(query);
                }
                return false;
            };

            _logView.Refresh();
        }
    }
}
