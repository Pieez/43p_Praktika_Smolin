using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using practika.ViewModel;
using System.ComponentModel;

namespace practika
{
    /// <summary>
    /// Логика взаимодействия для CAPTCHA.xaml
    /// </summary>
    public partial class CAPTCHA : Window
    {

        private string captchaText;

        public CAPTCHA()
        {
            InitializeComponent();
            RefreshCaptcha();
        }

        private void RefreshCaptcha()
        {
            captchaText = GenerateRandomText();
            BitmapImage СaptchaImage = CreateCaptchaImage(captchaText);
            captchaImage.Source = СaptchaImage;
            captchaTextBox.Clear();
        }

        private string GenerateRandomText()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private BitmapImage CreateCaptchaImage(string text)
        {
            Bitmap bitmap = new Bitmap(150, 50);
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.Clear(System.Drawing.Color.White);
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            System.Drawing.Font font = new System.Drawing.Font(new System.Drawing.FontFamily("Bradley Hand ITC"), 16);
            System.Drawing.Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

            graphics.DrawString(text, font, brush, 10, 10);

            MemoryStream memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, ImageFormat.Png);

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(memoryStream.ToArray());
            bitmapImage.EndInit();

            return bitmapImage;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshCaptcha();
        }



        
        

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (captchaTextBox.Text.Equals(captchaText, StringComparison.OrdinalIgnoreCase))
            {
                
                Close();
                
            }
            else
            {
                ErrorMessage.Text = "* Неправильно введен текст";
                RefreshCaptcha();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}

