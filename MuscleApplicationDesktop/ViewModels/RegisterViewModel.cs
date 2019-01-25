using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace MuscleApplication.Desktop
{
    public class RegisterViewModel : BaseViewModel
    {

        #region Public Properties

        /// <summary>
        /// User's email typed in on the register page
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User's password typed in on the register page
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// What the user typed on the register page in the "Confirm Password" placeholder
        /// </summary>
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// User's birthday
        /// </summary>
        public string DateOfBirth { get; set; }
        /// <summary>
        /// If any error appears, user can see it 
        /// </summary>
        public string ErrorText { get; set; } = string.Empty;
        /// <summary>
        /// What is being displayed on the register button
        /// </summary>
        public string ButtonContent { get; set; } = "Register";
        /// <summary>
        /// User sees that everything went ok 
        /// </summary>
        public bool IsContinueTextVisible { get; set; } = false;
        #endregion
        #region Commands
        /// <summary>
        /// Adds the user credentials to the database 
        /// </summary>
        public ICommand ButtonCommand { get; set; }
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
            ButtonCommand = new RelayCommand(() => ButtonOptions());
        }
        #endregion
        #region Private Methods
        /// <summary>
        /// Two choices for the button (Register) or (Continue)
        /// </summary>
        private void ButtonOptions()
        {
            // If the button displays "Register" go to RegisterUser
            if (IsContinueTextVisible)
                ReturnCommand.Execute(null);
            else
                RegisterUser();
        }
        /// <summary>
        /// Registers the user
        /// </summary>
        /// <param name="parameter">User credentials</param>
        private void RegisterUser()
        {
            // Cleans the error text
            ErrorText = string.Empty;

            // Checks if the user has written all the data required
            if (Password != null && Email != null && ConfirmPassword != null)
            {
                // Continue if the password and confirm password textbox matches 
                if (Password == ConfirmPassword)
                {
                    try
                    {
                        // Parse the string into datetime
                        var birthdayDate = DateTime.Parse(DateOfBirth);

                        // Checks if the date of birth is correct
                        if (birthdayDate > DateTime.Parse("01/01/1900") && birthdayDate < DateTime.Now)
                        {
                            // Creates new user
                            var user = new User
                            {
                                Password = this.Password,
                                Email = this.Email,
                                DateOfBirth = birthdayDate,

                            };

                            // Adds the user to the database
                            db.Users.Add(user);

                            // Saves changes made to the database
                            SaveDbChanges();

                            // Shows the user that everything went ok 
                            IsContinueTextVisible = true;

                            // Changes the button text
                            ButtonContent = "Continue";
                        }
                        else
                            ErrorText = "You couldn't be born that day!";
                    }
                    catch(System.FormatException fe)
                    {
                        ErrorText = "Please type in proper date, DD/MM/YYYY";
                    }
                   catch(SqlException se)
                    {
                        ErrorText = "This email is already used";
                    }

                }
                else
                    ErrorText = "Passwords don't match!";
            }
            else
                ErrorText = "You left some boxes empty!";
        }
        #endregion
    }
}
