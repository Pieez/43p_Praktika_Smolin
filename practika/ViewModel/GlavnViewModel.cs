using practika.Model;
using practika.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace practika.ViewModel
{
    public class GlavnViewModel : ViewModelsBase
    {
        private UserAccountModel _currentUserAccount;
        private ViewModelsBase _currentChildView;
        private string _caption;


        private IUserRepository userRepository;



        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }
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

            userRepository = new UserRepository();

            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeCommand);
            ExecuteShowHomeCommand(null);

            ShowProductViewCommand = new ViewModelCommand(ExecuteProductViewCommand);

            LoadCurrentUserData();


        }

        private void ExecuteProductViewCommand(object obj)
        {
            Glavn glavn = new Glavn();

            if (_currentUserAccount != null && (_currentUserAccount.Role == "admin" || _currentUserAccount.Role == "lab"))
            {
                CurrentChildView = new ProductModel();
                Caption = "Product";
                glavn.productButton.Visibility = Visibility.Visible;
            }
            else
            {
                glavn.productButton.Visibility = Visibility.Collapsed;
            }
        }
        private void ExecuteShowHomeCommand(object obj)
        {
           
                CurrentChildView = new Dash();
                Caption = "Dashboard";
            
            
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount = new UserAccountModel()
                {
                    login = user.login,
                    DisplayName = $"{user.name} {user.analysator}",
                    ProfilePicture = null,
                    Role = user.login
                };
            }
            else
            {


            }
        }
    }
}
