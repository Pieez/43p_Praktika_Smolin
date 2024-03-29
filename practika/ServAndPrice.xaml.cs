﻿using System;
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
    /// Логика взаимодействия для ServAndPrice.xaml
    /// </summary>
    public partial class ServAndPrice : Page
    {
        public Delete delete;


        public ServAndPrice(Delete _delete)
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
                string query = "INSERT INTO Service (service , price) VALUES (@service, @price)";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@service", txtServ.Text);
                command.Parameters.AddWithValue("@price", txtPrice.Text);
                int result = command.ExecuteNonQuery(); // добавлено выполнение запроса
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // добавлено вывод сообщения об ошибке
            }
        }
    }
}
