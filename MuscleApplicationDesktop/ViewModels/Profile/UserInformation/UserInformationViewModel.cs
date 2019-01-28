using System;

namespace MuscleApplication.Desktop
{
    public class UserInformationViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// User's age
        /// </summary>
        public string Age { get; set; }
        /// <summary>
        /// User's weight
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// User's height
        /// </summary>
        public string Height { get; set; }
        /// <summary>
        /// User's BMI
        /// </summary>
        public string BMI { get; set; }

        #endregion

        #region Constructor
        public UserInformationViewModel()
        {
            // Converts date of birth to 4 characters string
            Age = String.Format("You are {0} years old", (DateTime.Now.Year - CurrentUser.DateOfBirth.Year).ToString());

           
            if (CurrentUser.Weight != null && CurrentUser.Height != null)
            {
                // Formats user's weight
                Weight = String.Format("You weigh {0} kg", CurrentUser.Weight.ToString());
                // Formats user's height
                Height = String.Format("Your height is {0} cm", CurrentUser.Height.ToString());

                // Casts height and weight into double
                var height = (double)CurrentUser.Height;
                var weight = (double)CurrentUser.Weight;

                // Calculates the BMI 
                var bmi = weight * (height / 100 * height / 100);

                // Displays in proper format 
                BMI = String.Format("Your BMI is {0}!", bmi);
            }
            else
            {
                Weight = "Add some information about you by clicking the button above!";
            }
        }
        #endregion
    }
}
