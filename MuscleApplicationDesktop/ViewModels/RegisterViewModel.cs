using System.Windows;
using System.Windows.Input;

namespace MuscleApplication.Desktop
{
    public class RegisterViewModel : BaseViewModel
    {
        #region Private Members
        /// <summary>
        /// User's email typed in on the register page
        /// </summary>
        private string email;
        /// <summary>
        /// User's password typed in on the register page
        /// </summary>
        private string password;
        /// <summary>
        /// What the user typed on the register page in the "Confirm Password" placeholder
        /// </summary>
        private string confirmPassword;
        /// <summary>
        /// User's credentials
        /// </summary>
        private Users user;

        #endregion
        #region Public Properties

        /// <summary>
        /// User's email typed in on the register page
        /// </summary>
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                // Creates new user based on the input provided by the user
                user = new Users
                {
                    email = Email,
                    password = Password

                };
            }
        }
        /// <summary>
        /// User's password typed in on the register page
        /// </summary>
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                // Creates new user based on the input provided by the user
                user = new Users
                {
                    email = Email,
                    password = Password

                };
            }
        }
        /// <summary>
        /// What the user typed on the register page in the "Confirm Password" placeholder
        /// </summary>
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
            }
        }
        /// <summary>
        /// User's credentials
        /// </summary>
        public Users User
        {
            get { return user; }
            set { user = value; }
        }
        #endregion
        #region Commands
        /// <summary>
        /// Adds the user credentials to the database 
        /// </summary>
        public ICommand RegisterCommand { get; set; }
        /// <summary>
        /// Returns the user to the login page
        /// </summary>
        public ICommand ReturnCommand { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public RegisterViewModel()
        {
            // Creates the commands
            ReturnCommand = new RelayCommand(() => ((WindowViewModel)((MainWindow)Application.Current.MainWindow).DataContext).ChangePage(ApplicationPage.Login));
            RegisterCommand = new RelayCommand(() => RegisterUser());
        }
        #endregion
        #region Private Methods
        /// <summary>
        /// Registers the user
        /// </summary>
        /// <param name="parameter">User credentials</param>
        private void RegisterUser()
        {
            // Checks if the user has written all the data required
            if (User.password != null && User.email != null && ConfirmPassword != null)
            {
                // Continue if the password and confirm password textbox matches 
                if (User.password == ConfirmPassword)
                {
                    db.Users.Add(User);
                    SaveDbChanges();
                }
            }
        }
        #endregion
    }
}
