using practika.Model;
using practika.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace practika.ViewModel
{
    public class MainViewModel : ViewModelsBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private ViewModelsBase _currentChildView;
        private string _caption;
        //private IconChar _icon;


        private IUserRepository userRepository;


        //Properties
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
        //public IconChar Icon
        //{
        //    get
        //    {
        //        return _icon;
        //    }
        //    set
        //    {
        //        _icon = value;
        //        OnPropertyChanged(nameof(Icon));
        //    }
        //}

        //--> Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowCustomersViewCommand { get; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            //Initials commands
            ShowHomeViewCommand = new ViewModelCommand(ExecutedShowHomeCommand);
            ShowCustomersViewCommand = new ViewModelCommand(ExecutedShowCustomersCommand);

            //Default view
            ExecutedShowHomeCommand(null);

            LoadCurrentUserData();
        }

        private void ExecutedShowCustomersCommand(object obj)
        {
            
            //CurrentChildView = new CustomerViewModel();
            //Caption = "Customers";
            //Icon = IconChar.UserGroup;
        }

        private void ExecutedShowHomeCommand(object obj)
        {

            //CurrentChildView = new HomeViewModel();
            //Caption = "Dashboard";
            //Icon = IconChar.Home;
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount = new UserAccountModel()
                {
                    Username = user.Username,
                    DisplayName = $"{user.Name} {user.LastName}",
                    ProfilePicture = null
                };
            }
            else
            {


            }
        }
    }
}
