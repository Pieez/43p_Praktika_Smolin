using System;
using System.Collections.Generic;
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

namespace practika
{
    /// <summary>
    /// Логика взаимодействия для ServAndPriceDel.xaml
    /// </summary>
    public partial class ServAndPriceDel : Page
    {
        public Delete delete;
        public ServAndPriceDel(Delete _delete)
        {
            InitializeComponent();
            delete = _delete;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String connectionString = "server=ngknn.ru;Trusted_Connection=No;DataBase=43p_praktika_Smolin;User=33П;PWD=12357";
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string query = "DELETE FROM Service WHERE service = @service";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@service", txtServ.Text);
                int result = command.ExecuteNonQuery();
                con.Close();
                if (result > 0)
                {
                    MessageBox.Show("Услуга успешно удалена");
                }
                else
                {
                    MessageBox.Show("Услуга не была удалена, возможно ее нет в базе данных");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
