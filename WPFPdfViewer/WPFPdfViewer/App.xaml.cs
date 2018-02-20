using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
//using WPF.PdfViewer;

namespace WPF.PdfViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        void App_Startup(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();   
            mainWindow.Show();
        }

        
    }
}
