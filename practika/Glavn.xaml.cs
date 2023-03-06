using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace practika
{
    /// <summary>
    /// Логика взаимодействия для Glavn.xaml
    /// </summary>
    public partial class Glavn : Window
    {

        private DispatcherTimer autoCloseTimer;
        private DateTime startTime;
        public Glavn()
        {
            InitializeComponent();

            startTime = DateTime.Now;

            autoCloseTimer = new DispatcherTimer();
            autoCloseTimer.Interval = TimeSpan.FromSeconds(1) - (DateTime.Now - startTime);
            autoCloseTimer.Tick += AutoCloseTimer_Tick;
            autoCloseTimer.Start();

            TimerLabel.DataContext = TimeSpan.FromSeconds(7200);

        }

        private void AutoCloseTimer_Tick(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                TimeSpan remainingTime = TimeSpan.FromSeconds(7200) - (DateTime.Now - startTime);
                TimerLabel.Content = remainingTime.ToString(@"hh\:mm\:ss");
            });
            if (DateTime.Now.Subtract(startTime).TotalSeconds >= 7200)
            {
                autoCloseTimer.Stop();
                MessageBox.Show("Timer Out");
                Application.Current.Shutdown();
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }
    }
}
