using TzClasses;
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
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Spire.Xls;

namespace ProgTZApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        

        public MainWindow()
        {
            InitializeComponent();

            BitmapImage borderwavetop = new BitmapImage();
            borderwavetop.BeginInit();
            borderwavetop.UriSource = new Uri(System.IO.Directory.GetCurrentDirectory() + @"..\..\..\..\images\border-wave-top.png");
            borderwavetop.EndInit();

            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(System.IO.Directory.GetCurrentDirectory() + @"..\..\..\..\images\logo.png");
            logo.EndInit();

            BitmapImage loginicon = new BitmapImage();
            loginicon.BeginInit();
            loginicon.UriSource = new Uri(System.IO.Directory.GetCurrentDirectory() + @"..\..\..\..\images\login-icon.png");
            loginicon.EndInit();

            BitmapImage passicon = new BitmapImage();
            passicon.BeginInit();
            passicon.UriSource = new Uri(System.IO.Directory.GetCurrentDirectory() + @"..\..\..\..\images\password-icon.png");
            passicon.EndInit();

            BorderWaveTop.Source = borderwavetop;
            LogoImage.Source = logo;
            LoginIcon.Source = loginicon;
            PassIcon.Source = passicon;






            //MessageBox.Show(test);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            string username = LoginTextBox.Text;
            string password = PassPasswordBox.Password;

            LoadingWindow loadingWindow = new LoadingWindow(username, password);

            loadingWindow.Show();

            this.Close();

        }
    }
}
