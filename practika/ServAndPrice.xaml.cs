using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZXing;
using System.IO;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Data.SqlTypes;

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
                string connectionString = "server=ngknn.ru;Trusted_Connection=No;DataBase=43p_praktika_Smolin;User=33П;PWD=12357";
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                // создание объекта для генерации штрих-кода в векторном формате
                BarcodeWriterSvg writer = new BarcodeWriterSvg();

                // установка типа кодирования (например, EAN13)
                writer.Format = BarcodeFormat.EAN_13;

                // генерация кода
                string barcodeText = "123456789012";

                // установка размеров штрих-кода
                writer.Options.Height = 25;
                writer.Options.Width = 120;
                writer.Options.Margin = 2;

                // создание векторного изображения штрих-кода
                var svg = writer.Write(barcodeText);
                string svgString = svg.ToString(); // конвертация в строку
                byte[] imageBytes = Encoding.UTF8.GetBytes(svgString); // преобразование векторного изображения в байты массив

                // сохранение в базу данных
                string query = "INSERT INTO Service (service, price, barcode) VALUES (@service, @price, @barcode)";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@service", txtServ.Text);
                command.Parameters.AddWithValue("@price", txtPrice.Text);
                command.Parameters.AddWithValue("@barcode", svgString); // передаем строку вместо байтового массива
                int result = command.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }
    }
}
