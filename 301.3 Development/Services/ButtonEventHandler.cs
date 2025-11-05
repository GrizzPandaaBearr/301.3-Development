using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _301._3_Development.Services
{
    public class ButtonEventHandler
    {
        private Frame mainFrame {  get; set; }

        public ButtonEventHandler(Frame frame) // routes navigation to specified frame
        {
            mainFrame = frame;
        }

       
        public void PatientInformationRoute(object sender, RoutedEventArgs e)
        {
            patientdataform dataform = new patientdataform();

            dataform.Content = dataform;

            Debug.Write($"You Pressed routed button: {((FrameworkElement)e.Source).Name}");
        }

    }
}
