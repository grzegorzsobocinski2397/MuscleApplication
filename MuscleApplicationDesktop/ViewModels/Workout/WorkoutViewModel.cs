using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows.Input;

namespace MuscleApplication.Desktop
{
    /// <summary>
    /// View model for the workout page 
    /// </summary>
    public class WorkoutViewModel : BaseViewModel
    {
        #region Public Properties 

        /// <summary>
        /// True if user selected some Routine
        /// </summary>
        public bool IsExercisePageVisible { get; set; } = false;
        /// <summary>
        /// True if user selected add new routine or new exercise 
        /// </summary>
        public bool IsOverlayEffectUsed { get; set; } = false;
        /// <summary>
        /// When user clicks the overlay effect background then he leaves the create routine window
        /// </summary>
        public ICommand IsOverlayEffectUsedCommand { get; set; }
        #endregion
        #region Constructor
        public WorkoutViewModel()
        {
            // Listens for a message to change exercise page
            MessengerInstance.Register<PropertyChangedMessage<string>>(this, ChangeExercisePage);
            // Listens for a message to display create routine page
            MessengerInstance.Register<PropertyChangedMessage<string>>(this, OpenCreateRoutinePage);
            // Creates commands
            IsOverlayEffectUsedCommand = new RelayCommand(() => ChangeOverlayEffect());

        }

       
        #endregion
        #region Public Methods
        /// <summary>
        /// Hides the create window and resets the selected exercises
        /// </summary>
        public void ChangeOverlayEffect()
        {
            // Hides the create routine window
            IsOverlayEffectUsed = false;
            // Sends the message to the List View Model to clear the lists
            MessengerInstance.Send(new NotificationMessage("ClearExercises"));
        }
        /// <summary>
        /// Changes the selected routine and updates the routine exercises
        /// </summary>
        /// <param name="message"></param>
        public void ChangeExercisePage(PropertyChangedMessage<string> message)
        {
            // Continues if the propertyName matches
            if(message.PropertyName == "OpenRoutine")
            {
                // User selected some routine
                IsExercisePageVisible = true;
                // Sends the MVVM light message to populate the exercise list
                MessengerInstance.Send(new PropertyChangedMessage<string>("", message.NewValue, "GetExercises"));
            }
            
        }
        /// <summary>
        /// Opens the Create Routine User Control
        /// </summary>
        /// <param name="obj"></param>
        public void OpenCreateRoutinePage(PropertyChangedMessage<string> obj)
        {
            // Continues if the propertyName matches
            if (obj.PropertyName == "OpenCreateRoutine")
            {
                // Sets the overlay effect on 
                IsOverlayEffectUsed = true;
            }
        }
        #endregion

    }
}
