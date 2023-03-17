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
using System.Drawing;
using ZXing.Common;
using ZXing;
using System.Runtime.InteropServices;
using System.Windows.Interop;

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


        private BitmapSource GenerateBarcode(string text)
        {
            BarcodeWriter barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128
            };
            var barcodeBitmap = barcodeWriter.Write(text);
            return BitmapToBitmapSource(barcodeBitmap);
        }

        private BitmapSource BitmapToBitmapSource(Bitmap bitmap)
        {
            var handle = bitmap.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(handle);
            }
        }

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr hObject);

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string connectionString = "server=ngknn.ru;Trusted_Connection=No;DataBase=43p_praktika_Smolin;User=33П;PWD=12357";
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT login, ip FROM Workers";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            myComboBox.DisplayMemberPath = "login";
            myComboBox.SelectedValuePath = "ip";

            while (reader.Read())
            {
                string login = reader.GetString(0);
                string ip = reader.GetString(1);

                myComboBox.Items.Add(new { login, ip });
            }

            reader.Close();
            connection.Close();
        }


    }
}
