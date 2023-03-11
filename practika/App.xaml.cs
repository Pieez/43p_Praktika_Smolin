using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.Net.Sockets;

namespace practika
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {



        protected void Start(object sender, StartupEventArgs e)
        {

            Download download = new Download();

            download.Show();

            Thread.Sleep(3000);

            var loginView = new MainWindow();

            download.Close();

            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible != true && loginView.IsLoaded)
                {

                    var mainView = new Glavn();
                    mainView.Show();
                    loginView.Close();
                }
                
            };
        }
    }
}
