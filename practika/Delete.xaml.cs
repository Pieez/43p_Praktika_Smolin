using practika.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace practika
{
    /// <summary>
    /// Логика взаимодействия для Delete.xaml
    /// </summary>
    public partial class Delete : UserControl
    {
       // ServAndPrice Serv = new ServAndPrice();

        public Delete()
        {
            InitializeComponent();

            
        }



        public enum page
        {
            serv
        }

        void Open(page page)
        {
            if (page == page.serv)
            {
                frame.Navigate(new ServAndPrice(this));
            }

        }

        void Close(page page)
        {
            if (page == page.serv)
            {
                frame.NavigationService.Navigate(null);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Open(page.serv);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close(page.serv);
        }
    }
}
