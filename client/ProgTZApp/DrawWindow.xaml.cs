using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using TzClasses;

namespace ProgTZApp
{


    public partial class DrawWindow : Window
    {

        List<CreateParams> CreateParamss = new List<CreateParams>();
        string actURL = "https://ExitGamesDesktop.team-3759699.repl.co/api/get_schedule";
        string username;
        string password;

        public DrawWindow()
        {
            InitializeComponent();
            CreateParamssListBox.ItemsSource = CreateParamss;
            
            HideWeekends();
        }

        public DrawWindow(string username, string password)
        {
            InitializeComponent();
            CreateParamssListBox.ItemsSource = CreateParamss;

            HideWeekends();

            this.username = username;
            this.password = password;
        }


        private void HideWeekends()
        {
            CreateParamsNameTextBlock.Visibility = Visibility.Hidden;
            CreateParamsNameTextBox.Visibility = Visibility.Hidden;
            WeekendsStartTextBlock.Visibility = Visibility.Hidden;
            WeekendsStartTimePicker.Visibility = Visibility.Hidden;
            WeekendsEndTextBlock.Visibility = Visibility.Hidden;
            WeekendsEndTimePicker.Visibility = Visibility.Hidden;
            BorderAddCreateParamsButton.Visibility = Visibility.Hidden;
            BorderDeleteCreateParamsButton.Visibility = Visibility.Hidden;
            CreateParamssListBox.Visibility = Visibility.Hidden;
        }

        private void ShowWeekends()
        {
            CreateParamsNameTextBlock.Visibility = Visibility.Visible;
            CreateParamsNameTextBox.Visibility = Visibility.Visible;
            WeekendsStartTextBlock.Visibility = Visibility.Visible;
            WeekendsStartTimePicker.Visibility = Visibility.Visible;
            WeekendsEndTextBlock.Visibility = Visibility.Visible;
            WeekendsEndTimePicker.Visibility = Visibility.Visible;
            BorderAddCreateParamsButton.Visibility = Visibility.Visible;
            BorderDeleteCreateParamsButton.Visibility = Visibility.Visible;
            CreateParamssListBox.Visibility = Visibility.Visible;
        }

        private void WeekendRaioButton_Click(object sender, RoutedEventArgs e)
        {
            ShowWeekends();
        }

        private void WorkdayRaioButton_Click(object sender, RoutedEventArgs e)
        {
            HideWeekends();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();

            mainWindow.Show();
            //loadingWindow.Show();

            this.Close();
        }

        private void AddCreateParamsButton_Click(object sender, RoutedEventArgs e)
        {
            string name = CreateParamsNameTextBox.Text;
            string startTime = WeekendsStartTimePicker.Text;
            string endTime = WeekendsEndTimePicker.Text;

            try
            {
                DateTime newStart = DateTime.Parse(startTime);
                DateTime newEnd = DateTime.Parse(endTime);
                if(newStart.TimeOfDay >= newEnd.TimeOfDay)
                {
                    throw new InvalidOperationException("Dates are wrong!"); 
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Неверный формат времени начала или конца дня!");
                return;
            }

            CreateParamss.Add(new CreateParams(name, startTime, endTime));
            CreateParamssListBox.ItemsSource = null;
            CreateParamssListBox.ItemsSource = CreateParamss;
            

        }

        private void DeleteCreateParamsButton_Click(object sender, RoutedEventArgs e)
        {
            CreateParams CreateParams_now = CreateParamssListBox.SelectedItem as CreateParams;
            CreateParamss.Remove(CreateParams_now);
            CreateParamssListBox.ItemsSource = null;
            CreateParamssListBox.ItemsSource = CreateParamss;
        }

        private void CreateParamsNameLBTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock TaskTextBlock = sender as TextBlock;

            CreateParams CreateParams = TaskTextBlock.DataContext as CreateParams;

            TaskTextBlock.Text = CreateParams.GetName();
        }

        private void StartTimeLBTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock TaskTextBlock = sender as TextBlock;

            CreateParams CreateParams = TaskTextBlock.DataContext as CreateParams;

            TaskTextBlock.Text = CreateParams.GetStartTime();
        }

        private void EndTimeLBTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock TaskTextBlock = sender as TextBlock;

            CreateParams CreateParams = TaskTextBlock.DataContext as CreateParams;

            TaskTextBlock.Text = CreateParams.GetEndTime();
        }

        

        private async void CreateGrpahButton_Click(object sender, RoutedEventArgs e)
        {
            string date = MainDatePicker.Text;

            bool isChecked = (bool)WeekendRaioButton.IsChecked;

            LoadingWindow loadingWindow = new LoadingWindow(date, isChecked, username, password, CreateParamss);

            loadingWindow.Show();

            this.Close();


            // await Application.Current.Dispatcher.InvokeAsync(new System.Action(() => HttpDateInfo()));
            // Application.Current.Dispatcher.InvokeAsync(new ThreadStart(Task.Run(HttpDateInfo)));

            //await Task.Run(HttpDateInfo);

        }
    }
}
