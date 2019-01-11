namespace MuscleApplication.Desktop
{
    /// <summary>
    /// View model for the exercises 
    /// </summary>
    public class ExerciseListItemViewModel : BaseViewModel
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
        #endregion



    }
}
