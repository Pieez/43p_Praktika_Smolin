using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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


        

      

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchText)));
            }
        }

        public List<Registr> MyDataList
        {
            get { return _myDataList; }
            set
            {
                _myDataList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MyDataList)));
            }
        }

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
        }

        private void LoadData()
        {
            // Retrieve data from SQL database and store it in MyDataList
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







        private void ExecuteSearchCommand(object obj)
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                // если SearchText пустой или равен null, то ничего не делаем
                return;
            }

            _filteredDataList = new List<Registr>();

            foreach (var item in MyDataList)
            {
                if (item.service.Contains(SearchText))
                {
                    _filteredDataList.Add(item);
                }
            }

            MyDataList = _filteredDataList;
        }

        private void ExecuteSortByServiceCommand(object obj)
        {
            _filteredDataList.Sort((a, b) => a.service.CompareTo(b.service));
            MyDataList = _filteredDataList;
        }

        private void ExecuteSortByPriceCommand(object obj)
        {
            _filteredDataList.Sort((a, b) => a.price.CompareTo(b.price));
            MyDataList = _filteredDataList;
        }

        private void ExecuteFilterByCheapCommand(object obj)
        {
            _filteredDataList = new List<Registr>();

            foreach (var item in MyDataList)
            {
                if (item.price > 50)
                {
                    _filteredDataList.Add(item);
                }
            }

            MyDataList = _filteredDataList;
        }

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

        private void ExecuteGoToPage1Command(object obj)
        {
            _currentPage = 1;
            UpdatePagedData();
        }

        private void ExecuteGoToPage2Command(object obj)
        {
            _currentPage = 2;        
            UpdatePagedData();
        }

        private void ExecuteGoToPage3Command(object obj)
        {

            if (MyDataList.Count > 0)
            {
                return; // если список пустой, то выход из метода без изменения _currentPage
            }
            _currentPage = 3;
            UpdatePagedData();
        }

        private void ExecuteGoToPage4Command(object obj)
        {
            if (MyDataList.Count > 0)
            {
                return; // если список пустой, то выход из метода без изменения _currentPage
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


