using practika.Model;
using practika.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace practika.ViewModel
{
    public class LoginViewModel : ViewModelsBase
    {
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        private IUserRepository userRepository;

        public UserRepository UserRepository;

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
         public SecureString Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }

            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        //-> Commands
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }


        //Constructor

        public LoginViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand("", ""));
        }

        private void ExecuteRecoverPassCommand(string v1, string v2)
        {
            throw new NotImplementedException();
        }
        private bool CanExecuteLoginCommand(object obj)
        {
            //Вход в главный
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 1 ||
            Password == null || Password.Length < 1)
                validData = false;
            else
            {
                validData = true;

                string connectionString = "server=ngknn.ru;Trusted_Connection=No;DataBase=43p_praktika_Smolin;User=33П;PWD=12357";
                string computerName = Environment.MachineName;

                //записывает истории входа
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO HistoryLogin (login, ip,date) VALUES ( @Username, @NewComputerName,@LoginTime)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", Username);
                    command.Parameters.AddWithValue("@LoginTime", DateTime.Now);
                    command.Parameters.AddWithValue("@NewComputerName", computerName);
                    int rowsAffected = command.ExecuteNonQuery();
                }


                //Записывает имя компьютера
                
                
                string userName = Environment.UserName;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Workers " +
                                   "SET ip = @NewComputerName " +
                                   "WHERE login=@login";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@NewComputerName", computerName);
                    command.Parameters.AddWithValue("@OldComputerName", computerName);
                    command.Parameters.AddWithValue("@login", Username);
                    int rowsAffected = command.ExecuteNonQuery();
                }

            }
            return validData;

            


        }
        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = userRepository.AutheticateUser(new NetworkCredential(Username, Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                IsViewVisible = false;
            }
            else
            {
                ErrorMessage = "* Неправильное имя пользователя или пароль";
            }


        }

       

       
    }
}
