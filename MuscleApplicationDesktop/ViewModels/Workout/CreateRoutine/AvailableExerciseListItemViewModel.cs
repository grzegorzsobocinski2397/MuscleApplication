using GalaSoft.MvvmLight.Messaging;
using System.Linq;
using System.Windows.Input;

namespace MuscleApplication.Desktop
{
    /// <summary>
    /// List View Model for the exercises that can be added to the new routine
    /// </summary>
    public class AvailableExerciseListItemViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// Exercise id 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// Exercise name
        /// </summary>
        public string ExerciseName { get; set; }
        /// <summary>
        /// Exercise type
        /// </summary>
        public string ExerciseType { get; set; }
        /// <summary>
        /// Secondary exercise type
        /// </summary>
        public string ExerciseType2 { get; set; }
        /// <summary>
        /// True if the user selected the checkbox
        /// </summary>
        public bool IsSelected { get; set; } = false;
        #endregion
        #region Commands
        /// <summary>
        /// Sends the message to the <see cref="AvailableExerciseListViewModel"/> stating that the exercise was selected by the user 
        /// </summary>
        public ICommand IsSelectedCommand { get; set; }

        #endregion
        #region Constructor
        public AvailableExerciseListItemViewModel()
        {
            // Creates the command
            IsSelectedCommand = new RelayParameterCommand((parameter) => ChangeIsSelectedAndSend(parameter));
        }
        #endregion
        #region Private Methods
        private void ChangeIsSelectedAndSend(object parameter)
        {
            // Casts the parameter (exercise id) into a string
            var exerciseId = parameter as string;

            // Looks into the database for the exercise
            var exercise = db.Exercises.Where(e => e.id == exerciseId).FirstOrDefault();

            // Creates new List Item
            var availableExercise = new AvailableExerciseListItemViewModel {
                id = exercise.id,
                ExerciseName = exercise.ExerciseName,
                ExerciseType = exercise.ExerciseType,
                ExerciseType2 = exercise.ExerciseType2,
                IsSelected = true,
            };
            

            // Sends the message to the List View Model
            MessengerInstance.Send(new PropertyChangedMessage<AvailableExerciseListItemViewModel>(null, availableExercise, "ExerciseSelected"));
        }

        #endregion
    }
}
