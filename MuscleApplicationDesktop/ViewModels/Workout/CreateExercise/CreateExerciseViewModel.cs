using System.Windows.Input;

namespace MuscleApplication.Desktop
{
    /// <summary>
    /// View model for creating exercise in <see cref="CreateExerciseControl"/>
    /// </summary>
    public class CreateExerciseViewModel : BaseViewModel
    {
        #region Private Members
        /// <summary>
        /// User's exercise name
        /// </summary>
        private string Name;
        /// <summary>
        /// User's exercise
        /// </summary>
        private Exercise exercise;
        #endregion
        #region Public Properties

        /// <summary>
        /// User's exercise name
        /// </summary>
        public string ExerciseName
        {
            get { return Name; }
            set
            {
                Name = value;
                Exercise = new Exercise
                {
                    Name = value,
                };

            }
        }
        /// <summary>
        /// User's exercise
        /// </summary>
        public Exercise Exercise
        {
            get { return exercise; }
            set
            {
                exercise = value;
                exercise = new Exercise
                {
                    Name = Name,
                };
            }
        }

        #endregion
        #region Commands
        /// <summary>
        /// Creates exercise based on user's input
        /// </summary>
        public ICommand CreateExerciseCommand { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Default constuctor
        /// </summary>
        public CreateExerciseViewModel()
        {
            CreateExerciseCommand = new RelayParameterCommand((parameter) => CreateExercise(parameter));
        }
        #endregion
        #region Private Methods
        private void CreateExercise(object parameter)
        {
            // Casts parameter into as exercise
            var exercise = parameter as Exercise;

            // If exercise is not null then...
            if(exercise != null)
            {
                // ...add exercise to the database
                db.Exercises.Add(exercise);
                // Save changes made to the database
                SaveDbChanges();
            }

        }
        #endregion
    }
}

