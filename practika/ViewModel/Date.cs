using practika.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZXing;

namespace practika.ViewModel
{
    public class Date : INotifyPropertyChanged
    {
        private string _searchText;
        private List<Registr> _myDataList;
        private List<Registr> _filteredDataList;
        private int _currentPage = 1;
        private const int PageSize = 10;

        public event PropertyChangedEventHandler PropertyChanged;


        

      
       
        public string SearchTextBox
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchTextBox)));
            }
        }

        // Для показа в DataGrid
        public List<Registr> MyDataList
        {
            get { return _myDataList; }
            set
            {
                _myDataList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MyDataList)));
            }
        }

        // Комманды для подключения к элементам
        public ICommand SearchCommand { get; set; }
        public ICommand SortByServiceCommand { get; set; }
        public ICommand SortByPriceCommand { get; set; }
        public ICommand FilterByCheapCommand { get; set; }
        public ICommand FilterByExpensiveCommand { get; set; }
        public ICommand GoToPage1Command { get; set; }
        public ICommand GoToPage2Command { get; set; }
        public ICommand GoToPage3Command { get; set; }
        public ICommand GoToPage4Command { get; set; }

        public Date()
        {
            SearchCommand = new ViewModelCommand(ExecuteSearchCommand);
            SortByServiceCommand = new ViewModelCommand(ExecuteSortByServiceCommand);
            SortByPriceCommand = new ViewModelCommand(ExecuteSortByPriceCommand);
            FilterByCheapCommand = new ViewModelCommand(ExecuteFilterByCheapCommand);
            FilterByExpensiveCommand = new ViewModelCommand(ExecuteFilterByExpensiveCommand);
            GoToPage1Command = new ViewModelCommand(ExecuteGoToPage1Command);
            GoToPage2Command = new ViewModelCommand(ExecuteGoToPage2Command);
            GoToPage3Command = new ViewModelCommand(ExecuteGoToPage3Command);
            GoToPage4Command = new ViewModelCommand(ExecuteGoToPage4Command);

            

            LoadData();


            MyDataList = new List<Registr>();
            for (int i = 1; i <= 10; i++)
            {
                MyDataList.Add(new Registr()
                {
                    service = $"Service {i}",
                    price = i * 100,
                    Barcode = GenerateRandomBarcode()
                });
            }


        }

        private string GenerateRandomBarcode()
        {
            Random random = new Random();
            string barcode = "";
            for (int i = 0; i < 12; i++)
            {
                int digit = random.Next(0, 10);
                barcode += digit.ToString();
            }

            var writer = new BarcodeWriterSvg
            {
                Format = BarcodeFormat.EAN_13
            };

            var svg = writer.Write(barcode);
            return barcode;
        }


        private void LoadData()
        {
            // Подключения в DataGrid
            string connectionString = "server=ngknn.ru;Trusted_Connection=No;DataBase=43p_praktika_Smolin;User=33П;PWD=12357";
            string sql = "SELECT service, price FROM Service";
            MyDataList = new List<Registr>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MyDataList.Add(new Registr
                    {
                        service = reader.GetString(0),
                        price = Convert.ToDecimal(reader.GetValue(1))
                    });
                }

                reader.Close();
            }

            _filteredDataList = MyDataList;
        }



       

        // Для подключения к элементам "Поиск"
        private void ExecuteSearchCommand(object obj)
        {
            if (string.IsNullOrEmpty(SearchTextBox))
            {
                // если SearchText пустой или равен null, то ничего не делаем
                return;
            }

            _filteredDataList = new List<Registr>();

            foreach (var item in MyDataList)
            {
                if (item.service.Contains(SearchTextBox))
                {
                    _filteredDataList.Add(item);
                }
            }

            MyDataList = _filteredDataList;
        }


        // Для подключения к элементам "Сортировка сервис"
        private void ExecuteSortByServiceCommand(object obj)
        {
            _filteredDataList.Sort((a, b) => a.service.CompareTo(b.service));
            MyDataList = _filteredDataList;
        }

        // Для подключения к элементам "Сортировка Цена"
        private void ExecuteSortByPriceCommand(object obj)
        {
            _filteredDataList.Sort((a, b) => a.price.CompareTo(b.price));
            MyDataList = _filteredDataList;
        }

        // Для подключения к элементам "Фильтрация"
        private void ExecuteFilterByCheapCommand(object obj)
        {
            _filteredDataList = new List<Registr>();

            foreach (var item in MyDataList)
            {
                if (item.price < 300)
                {
                    _filteredDataList.Add(item);
                }
            }

            MyDataList = _filteredDataList;
        }

        // Для подключения к элементам "Фильтрация"
        private void ExecuteFilterByExpensiveCommand(object obj)
        {
            _filteredDataList = new List<Registr>();

            foreach (var item in MyDataList)
            {
                if (item.price >= 50)
                {
                    _filteredDataList.Add(item);
                }
            }

            MyDataList = _filteredDataList;
        }

        // Открывает 1 страницу
        private void ExecuteGoToPage1Command(object obj)
        {
            _currentPage = 1;
            UpdatePagedData();
        }

        // Открывает 2 страницу
        private void ExecuteGoToPage2Command(object obj)
        {
           
            _currentPage = 2;        
            UpdatePagedData();
        }

        // Открывает 3 страницу
        private void ExecuteGoToPage3Command(object obj)
        {

            if (MyDataList.Count < PageSize * 3)
            {
                return; // если список пустой или не содержит нужного количества элементов, то выход из метода без изменения _currentPage
            }
            _currentPage = 3;
            UpdatePagedData();
            
        }


        // Открывает 4 страницу
        private void ExecuteGoToPage4Command(object obj)
        {
            if (MyDataList.Count < PageSize * 4)
            {
                return; // если список пустой или не содержит нужного количества элементов, то выход из метода без изменения _currentPage
            }
            _currentPage = 4;
            UpdatePagedData();
        }


        private void UpdatePagedData()
        {


            int startIndex = (_currentPage - 1) * PageSize;
            int endIndex = startIndex + PageSize;

            if (endIndex > _filteredDataList.Count)
            {
                endIndex = _filteredDataList.Count;
            }


            MyDataList = _filteredDataList.GetRange(startIndex, endIndex - startIndex);
        }
    }


}


