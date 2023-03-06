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
using System.Windows.Threading;
using practika.CustomControls;

namespace practika
{
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DispatcherTimer timer;
       
        public MainWindow()
        {
            InitializeComponent();
            //создаем таймер блокировки

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(30);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Получаем таймер из события
            DispatcherTimer timer = (DispatcherTimer)sender;

            // Получаем время начала таймера и интервал таймера из свойств Tag и Interval
            DateTime startTime = (DateTime)timer.Tag;
            TimeSpan interval = timer.Interval;

            // Вычисляем оставшееся время
            TimeSpan timeLeft = interval - (DateTime.Now - startTime);

            // Если оставшееся время меньше или равно нулю, то разблокируем кнопку "Войти"
            if (timeLeft <= TimeSpan.Zero)
            {
                btnLogin.IsEnabled = true;
                TimerLabel.Visibility = Visibility.Hidden;
                timer.Stop();
            }
            else
            {
                // Обновляем метку таймера с обратным отсчетом
                TimerLabel.Content = timeLeft.ToString(@"m\:ss");

                // Если метка таймера еще не видна, то показываем ее
                if (TimerLabel.Visibility == Visibility.Hidden)
                {
                    TimerLabel.Visibility = Visibility.Visible;
                }
            }
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
                loginAttempts++;
                if (loginAttempts >= 5)
                {
                    // Блокируем кнопку "Войти" на 30 секунд
                    DateTime startTime = DateTime.Now;
                    timer.Interval = TimeSpan.FromSeconds(30);
                    timer.Tag = startTime;
                    TimerLabel.Visibility = Visibility.Visible;
                    timer.Start();
                    btnLogin.IsEnabled = false; 
                    
                }
            }
            
           
        }

        
        
    }
}
