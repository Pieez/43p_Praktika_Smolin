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
using System.Windows.Controls;

namespace practika.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(SecureString), typeof(UserControl1));

        public SecureString Password
        {
            get { return (SecureString)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }
        public UserControl1()
        {
            InitializeComponent();
            txtPasswords.PasswordChanged += OnPasswordChanged;
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = txtPasswords.SecurePassword;
        }

        private void showPasswordToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            txtPasswords.Visibility = Visibility.Collapsed;
            visibleTextBox.Visibility = Visibility.Visible;
            visibleTextBox.Text = txtPasswords.Password;
        }

        private void showPasswordToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            txtPasswords.Visibility = Visibility.Visible;
            visibleTextBox.Visibility = Visibility.Collapsed;
            txtPasswords.Password = visibleTextBox.Text;
        }

    }
}
