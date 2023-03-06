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
    /// Логика взаимодействия для Registr.xaml
    /// </summary>
    public partial class Registr : UserControl
    {
        public Registr()
        {
            InitializeComponent();


            //// Подключение к базе данных
            //SqlConnection connection = new SqlConnection("server=ngknn.ru;Trusted_Connection=No;DataBase=43p_praktika_Smolin;User=33П;PWD=12357");
            //connection.Open();

            //// Выполнение запроса
            //SqlCommand command = new SqlCommand("SELECT * FROM [Service] ", connection);
            //SqlDataReader reader = command.ExecuteReader();

            //// Заполнение ListBox элементами
            //while (reader.Read())
            //{
            //    DataListBox.Items.Add(new
            //    {
            //        ColumnName1 = "service",
            //        ColumnValue1 = reader["service"],
                    
                    
            //        //Column2Options = new List<string>() { "service", "price" },

            //        // Добавьте другие столбцы, если это необходимо
            //    });

            //}
        }
    }
}
