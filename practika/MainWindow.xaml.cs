using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
using practika.CustomControls;

namespace practika
{
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Frame frame1;

       UserControl userControl;
        public MainWindow()
        {
            InitializeComponent();
        }

        public int vx = 0;
        List<practika.Users> Users = new List<Users>();
        List<practika.Workers> Workers = new List<Workers>();


        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string user = txtUser.Text;
            string pas = txtPasswords.Password;

            int count = Entities.GetContext().Users.Count();
            int count_w = Entities.GetContext().Workers.Count();
            Workers = Entities.GetContext().Workers.ToList();
            Users = Entities.GetContext().Users.ToList();
            for(int i = 0; i < count; i++)
            {
                if(Workers[i].login == user)
                {
                    if (Workers[i].password == pas)
                    {
                        
                    }
                }
            }
        }
    }
}
