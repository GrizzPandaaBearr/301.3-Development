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
using _301._3_Development.Scripts;

namespace _301._3_Development.Pages
{
    /// <summary>
    /// Interaction logic for RemovePatients.xaml
    /// </summary>
    public partial class RemovePatients : Page
    {
        public RemovePatients()
        {
            InitializeComponent();
        }

        private void DeleteAllUsersButton_Click(object sender, RoutedEventArgs e)
        {
            DataHandler handler = new DataHandler();
            handler.RemoveAllUsers();

        }

        private void DeleteSingleUser_Click(object sender, RoutedEventArgs e)
        {
            DataHandler handler = new DataHandler();
            handler.RemoveUser(FirstName_box.Text, Phone_box.Text);
        }
    }
}
