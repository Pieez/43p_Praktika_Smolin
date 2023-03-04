using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace practika
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void Start(object sender, StartupEventArgs e)
        {
            var loginView = new MainWindow();

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
