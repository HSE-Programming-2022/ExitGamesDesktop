using Microsoft.Win32;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using TzClasses;
using WpfAnimatedGif;

namespace ProgTZApp
{
    /// <summary>
    /// Логика взаимодействия для LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {

        string authURL = "https://exitgamesdesktop.team-3759699.repl.co/api/login";
        bool _shown;
        string username;
        string password;
        List<CreateParams> CreateParamss;
        string givenDate;
        bool isChecked;
        bool loadType;
        string actURL = "https://ExitGamesDesktop.team-3759699.repl.co/api/get_schedule";

        public LoadingWindow()
        {

            InitializeComponent();

        }

        public LoadingWindow(string username, string password)
        {


            InitializeComponent();

            this.username = username;
            this.password = password;

            loadType = true;
            
        }

        public LoadingWindow(string givenDate, bool isChecked, string username, string password, List<CreateParams> paramss)
        {

            InitializeComponent();

            this.CreateParamss = paramss;
            this.givenDate = givenDate;
            this.isChecked = isChecked;
            this.username = username;
            this.password = password;

            loadType = false;

        }

        public void OpenLogWin()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainWindow mainWindow = new MainWindow();

                mainWindow.Show();

                this.Close();
            });
            
        }

       

        public void OpenDrawWin()
        {

            Application.Current.Dispatcher.Invoke(() =>
            {
                DrawWindow drawWindow = new DrawWindow(username, password);

                drawWindow.Show();

                this.Close();
            });
            
        }

        protected override async void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            if (_shown)
                return;

            _shown = true;

            if (loadType)
            {
                await Task.Run(HttpAuth);
            }
            else
            {
                await Task.Run(HttpDateInfo);
            }


            
        }

        internal async Task HttpDateInfo()
        {
            System.Text.RegularExpressions.Regex.Unescape("\u4430\u0446");

            string result;
            string date = givenDate;
            string data = "";


            var httpRequest = HttpWebRequest.Create(actURL);

            httpRequest.Method = "POST";

            httpRequest.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));
            httpRequest.ContentType = "application/json";

            if (isChecked)
            {

                if (CreateParamss.Count == 0)
                {
                    MessageBox.Show("Добавьте аниматоров!");
                    return;
                }

                for (int i = 0; i < CreateParamss.Count; i++)
                {

                    string startPreAdd = CreateParamss[i].GetStartTime();

                    string endPreAdd = CreateParamss[i].GetEndTime();

                    if (startPreAdd.Length < 5)
                    {
                        startPreAdd = "0" + startPreAdd;
                    }

                    if (endPreAdd.Length < 5)
                    {
                        endPreAdd = "0" + endPreAdd;
                    }

                    DateTime start = DateTime.ParseExact(startPreAdd, "HH:mm", null);

                    DateTime end = DateTime.ParseExact(endPreAdd, "HH:mm", null);

                    try
                    {
                        if ((start.TimeOfDay < DateTime.ParseExact("09:00", "HH:mm", null).TimeOfDay) || (start.TimeOfDay > end.TimeOfDay))
                        {
                            MessageBox.Show("Не верно указано время старта! Должно быть больше 9:00 и меньше времени конца");
                            return;
                        }

                        if (((end.TimeOfDay > DateTime.ParseExact("23:00", "HH:mm", null).TimeOfDay) || (start.TimeOfDay > end.TimeOfDay)))
                        {
                            MessageBox.Show("Не верно указано время конца! Должно быть меньше 23:00 и больше времени начала");
                            return;
                        }
                    }

                    catch
                    {
                        MessageBox.Show("Не верно указаны даты!");
                        return;
                    }

                }


                data = @$"{{
	                    ""mode"": ""weekends"",
                        ""date"": ""{date}"",
                        ""timedelta_info"": [
                         ";

                for (int i = 0; i < CreateParamss.Count; i++)
                {
                    string toAdd = $@"{{
                         ""name"": ""{CreateParamss[i].GetName()}"",
                         ""timedelta"": [""{CreateParamss[i].GetStartTime()}"", ""{CreateParamss[i].GetEndTime()}""]
                    }}";

                    if (i != (CreateParamss.Count - 1))
                    {
                        toAdd += ",";
                    }

                    data += toAdd;
                }

                data += "]}";



            }
            else
            {
                data = @$"{{
	                    ""mode"": ""weekdays"",
                        ""date"": ""{date}""

                         }} ";
            }

            //MessageBox.Show(data);



            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                Stream httpResponseStream = httpResponse.GetResponseStream();

                //MessageBox.Show(httpResponseStream.Read);


                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show("Проверьте выбранные данные!");
                    OpenDrawWin();
                }

                else
                {
                    int bufferSize = 1024;
                    byte[] buffer = new byte[bufferSize];
                    int bytesRead = 0;

                    SaveFileDialog saveFileDialog = new SaveFileDialog();

                    saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        using (Stream fileStream = File.OpenWrite(saveFileDialog.FileName))
                            while ((bytesRead = httpResponseStream.Read(buffer, 0, bufferSize)) != 0)
                            {
                                fileStream.Write(buffer, 0, bytesRead);
                            }
                    }
                    OpenDrawWin();
                    // Read from response and write to file
                    //System.IO.File.WriteAllText(saveFileDialog.FileName, string.Empty);



                    //MessageBox.Show("Done");
                   
                   
                }



                return;


            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    //MessageBox.Show(ex.ToString());
                    MessageBox.Show("Ошибка сервера!");
                }
                else
                {
                    //MessageBox.Show(ex.ToString());
                    MessageBox.Show("Ошибка сервера!");
                }

                //OpenLogWin();
                return;
            }
            catch (Exception ex)
            {
                //OpenLogWin();
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        internal async Task HttpAuth()
        {
            string result;

            var httpRequest = HttpWebRequest.Create(authURL);

            httpRequest.Method = "POST";

            httpRequest.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password)); ;
            httpRequest.ContentType = "application/json";

            var data = @$"{{
	                    ""login"": ""{username}"", ""password"": ""{password}""
                         }} ";

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();

                }

                Dictionary<string, string> resDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                //MessageBox.Show(resDict["status"]);

                if (resDict["status"] == "false")
                {
                    MessageBox.Show("Проверьте данные авторизации!");
                    OpenLogWin();
                }
                else if (resDict["status"] == "Error")
                {
                    MessageBox.Show("Ошибка в запросе!");
                    OpenLogWin();
                }
                else if (resDict["status"] == "true")
                {
                    OpenDrawWin();
                }
                else
                {
                    MessageBox.Show("Unknown error");
                    OpenLogWin();
                    return;
                }

                return;


            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }
                else
                {
                    MessageBox.Show(ex.ToString());
                }

                OpenLogWin();
                return;
            }
            catch (Exception)
            {
                OpenLogWin();
                return;
            }
        }


    }
}
