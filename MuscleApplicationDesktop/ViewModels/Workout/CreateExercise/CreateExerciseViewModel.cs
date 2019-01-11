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
        private string exerciseName;
        /// <summary>
        /// User's exercise type
        /// </summary>
        private string exerciseType;
        /// <summary>
        /// User's exercise
        /// </summary>
        private Exercises exercise;
        #endregion
        #region Public Properties

        /// <summary>
        /// User's exercise name
        /// </summary>
        public string ExerciseName
        {
            get { return exerciseName; }
            set
            {
                exerciseName = value;
                Exercise = new Exercises
                {
                    ExerciseName = value,
                    ExerciseType = exerciseType
                };

            }
        }
        /// <summary>
        /// User's exercise type
        /// </summary>
        public string ExerciseType
        {
            get { return exerciseType; }
            set
            {
                exerciseType = value;
                Exercise = new Exercises
                {
                    ExerciseName = exerciseName,
                    ExerciseType = value
                };
            }
        }
        /// <summary>
        /// User's exercise
        /// </summary>
        public Exercises Exercise
        {
            get { return exercise; }
            set
            {
                exercise = value;
                exercise = new Exercises
                {
                    ExerciseName = ExerciseName,
                    ExerciseType = ExerciseType
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
            var exercise = parameter as Exercises;

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

