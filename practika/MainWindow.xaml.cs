﻿using System;
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
using System.Windows.Threading;
using practika.CustomControls;

namespace practika
{
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

       
       
        public MainWindow()
        {
            InitializeComponent();

        }

        

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


        private int count = 0;

        private string expectedUsername = "username";
        private string expectedPassword = "password";
        private int loginAttempts = 0;
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {


            UserControl1 user = new UserControl1();

            string username = txtUser.Text;
            string passwordd = user.txtPasswords.Password;


            string password = "myPassword";
            if (user.txtPasswords.Password != password)
            {
                count++;
                if (count == 3)
                {
                    //Открывается капча
                    CAPTCHA form = new CAPTCHA();
                    form.ShowDialog();
                    count = 0;
                }
                
            }
            
           
        }

        
        
    }
}
