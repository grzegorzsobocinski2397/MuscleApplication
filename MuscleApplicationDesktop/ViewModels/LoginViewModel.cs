using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MuscleApplication.Desktop
{
    public class LoginViewModel : BaseViewModel
    {
        #region Private Members
        /// <summary>
        /// Email that was typed into TextBox
        /// </summary>
        private string email;
        /// <summary>
        /// Secure password that was typed into PasswordBox
        /// </summary>
        private string password;
        #endregion
        #region Public Properties
        /// <summary>
        /// Email that was typed into TextBox
        /// </summary>
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        /// <summary>
        /// Secure password that was typed into PasswordBox
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        #endregion
        #region Commands
        /// <summary>
        /// Parametrized command that uses email and password of a user in order to login
        /// </summary>
        public ICommand LoginCommand { get; set; }
        /// <summary>
        /// Moves the user to register page
        /// </summary>
        public ICommand RegisterCommand { get; set; }


        #endregion
        #region Constructor
        public LoginViewModel()
        {
            // Creates new commands
            LoginCommand = new RelayParameterCommand(async (parameter) => await LoginAsync(parameter));
            /*RegisterCommand = new RelayCommand(() =>
                ((WindowViewModel)((MainWindow)Application.Current.MainWindow).DataContext).ChangePage(ApplicationPage.Homepage));*/
            RegisterCommand = new RelayCommand(() => ((WindowViewModel)((MainWindow)Application.Current.MainWindow).DataContext).ChangePage(ApplicationPage.Register));

        }

        #endregion
        #region Private Methods
        private async Task LoginAsync(object parameter)
        {
            // User credentials taken from the login page
            var email = Email;
            var password = Password;

            if (email != null && password != null)
            {
                bool canLogin = LoginCredentialsCheckAsync(email, password);
                if (canLogin)
                    ((WindowViewModel)((MainWindow)Application.Current.MainWindow).DataContext).ChangePage(ApplicationPage.Homepage);
                else
                    MessageBox.Show("Error", "Please try again", MessageBoxButton.OK);

                await Task.Delay(10);

            }

        }
        /// <summary>
        /// Checks if the email and password have a match up in the database
        /// </summary>
        /// <param name="email">Email written by the user in the login screen</param>
        /// <param name="password">Password written by the user in the login screen</param>
        /// <returns></returns>
        public bool LoginCredentialsCheckAsync(string email, string password)
        {
            // Looks for the given email address in the database
            var user = db.Users.Where(u => u.Email == email).FirstOrDefault();

            // If there is none, return false
            if (user != null)
            {

                // Selects user of this application as a given user
                if (user.Password == password)
                {

                    CurrentUser = user;
                    return true;
                }
                else
                    return false;
            }
            return false;
        }
        #endregion
        
    }
}
