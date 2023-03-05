using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace practika.ViewModel
{
    public class GlavnViewModel : ViewModelsBase
    {
        private ViewModelsBase _currentChildView;
        private string _caption;

        public ViewModelsBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }


        }
        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }


        public ICommand ShowHomeViewCommand { get; set; }
        public ICommand ShowProductViewCommand { get; set; }


        public GlavnViewModel()
        {
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeCommand);
            ExecuteShowHomeCommand(null);

            ShowProductViewCommand = new ViewModelCommand(ExecuteProductViewCommand);
            

        }

        private void ExecuteProductViewCommand(object obj)
        {
            CurrentChildView = new ProductModel();
            Caption = "Product";
        }

        private void ExecuteShowHomeCommand(object obj)
        {
            CurrentChildView = new Dash();
            Caption = "Dashboard";
        }
    }
}
