using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.Net.NetworkInformation;
using practika.ViewModel;

namespace practika
{
    /// <summary>
    /// Логика взаимодействия для Registr.xaml
    /// </summary>
    public partial class Registr : UserControl
    {
        public Registr()
        {
            InitializeComponent();


        }


        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private DependencyPropertyChangedEventArgs nameof(string username)
        {
            throw new NotImplementedException();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void myComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            string connectionString = "server=ngknn.ru;Trusted_Connection=No;DataBase=43p_praktika_Smolin;User=33П;PWD=12357";
            string query = "SELECT DISTINCT login FROM HistoryLogin";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string login = reader["login"].ToString();
                    myComboBox.Items.Add(login);
                    MessageBox.Show("Добавлено значение в ComboBox: " + login);
                }
            }
        }
    }
}
