using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows.Input;

namespace MuscleApplication.Desktop
{

    /// <summary>
    /// View Model that creates list of avaiable exercises 
    /// and makes a list of chosen exercises
    /// </summary>
    public class AvailableExerciseListViewModel : BaseViewModel
    {
        #region Private Members
        /// <summary>
        /// What the user searched for
        /// </summary>
        private string searchText;
        #endregion
        #region Public Properties
        /// <summary>
        /// List of all the created exercises
        /// </summary>
        public ObservableCollection<AvailableExerciseListItemViewModel> ExercisesList { get; set; }
        /// <summary>
        /// List of selected exercises by the user
        /// </summary>
        public ObservableCollection<AvailableExerciseListItemViewModel> SelectedExercisesList { get; set; }
        /// <summary>
        /// List of the exercises from the database
        /// </summary>
        public List<Exercises> ExercisesFromDatabase { get; set; }
        /// <summary>
        /// What the user searched for 
        /// </summary>
        public string SearchText
        {
            get { return searchText; }
            set
            {
                UpdateExercises(value);
                searchText = value;
            }
        }
        /// <summary>
        /// Routine name that user typed in
        /// </summary>
        public string RoutineName { get; set; }
        #region Commands
        /// <summary>
        /// Parameter is <see cref="RoutineName"/>, this command creates new routine with <see cref="SelectedExercisesList"/>
        /// as its exercises
        /// </summary>
        public ICommand CreateRoutineCommand { get; set; }
        #endregion
        #endregion
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public AvailableExerciseListViewModel()
        {
            // Gets the exercises from the database
            ExercisesFromDatabase = db.Exercises.ToList();
            // Creates the lists
            ExercisesList = new ObservableCollection<AvailableExerciseListItemViewModel>();
            SelectedExercisesList = new ObservableCollection<AvailableExerciseListItemViewModel>();
            // Gets all the exercises
            UpdateExercises(null);
            // Registers the message from the Item View Model
            MessengerInstance.Register<PropertyChangedMessage<AvailableExerciseListItemViewModel>>(this, SelectExercises);
            // Registers the message from the WorkoutViewModel
            MessengerInstance.Register<NotificationMessage>(this, ClearExercises);
            // Creates commands
            CreateRoutineCommand = new RelayParameterCommand((parameter) => CreateRoutine(parameter));
        }
        
        #endregion
        #region Private Methods
        /// <summary>
        /// Gets the exercises from the database
        /// </summary>
        private void UpdateExercises(string value)
        {
            // Clears the list just in case
            ExercisesList.Clear();
            if (value == null)
            {
                // Adds every exercise from database 
                foreach (var exercise in ExercisesFromDatabase)
                {
                    // Creates list item 
                    var availableExercise = new AvailableExerciseListItemViewModel
                    {
                        id = exercise.id,
                        ExerciseName = exercise.ExerciseName,
                        ExerciseType = exercise.ExerciseType,
                        ExerciseType2 = exercise.ExerciseType2
                    };
                    // Adds the exercise to the list
                    ExercisesList.Add(availableExercise);
                }
            }
            else
            {
                // Changes the value to uppercase so searching for "ben" can work for "Bench" 
                var searchedValue = value.ToUpper();

                
                foreach (var exercise in ExercisesFromDatabase)
                {
                    // Creates list item 
                    var availableExercise = new AvailableExerciseListItemViewModel
                    {
                        id = exercise.id,
                        ExerciseName = exercise.ExerciseName,
                        ExerciseType = exercise.ExerciseType,
                        ExerciseType2 = exercise.ExerciseType2
                    };
                    // Looks for the searched phrase 
                    if (availableExercise.ExerciseName.ToUpper().Contains(searchedValue) || availableExercise.ExerciseType.ToUpper().Contains(searchedValue))
                    {
                        // Adds the exercise to the list
                        ExercisesList.Add(availableExercise);
                    }
                }
            }
        }
        /// <summary>
        /// Selects an exercise
        /// </summary>
        /// <param name="obj"></param>
        private void SelectExercises(PropertyChangedMessage<AvailableExerciseListItemViewModel> obj)
        {
            // Checks if the object is null
            if (obj != null)
            {
                // Checks the property name 
                if (obj.PropertyName == "ExerciseSelected")
                {
                    // Creates new exercise from the object value
                    var exercise = obj.NewValue;

                    bool doesExist = false;
                    // If the list is empty, add the exercise
                    if (SelectedExercisesList.ToList().Count == 0)
                    {
                        SelectedExercisesList.Add(exercise);
                    }
                    // Else looks for the exercise in the Selected Exercises List
                    else
                    {
                        // Using .ToList() to execute enumeration operation 
                        foreach (var selectedExercise in SelectedExercisesList.ToList())
                        {
                            // If exercise already exists in this list that means the exercise was deselected
                            if (exercise.id == selectedExercise.id)
                            {
                                doesExist = true;
                                SelectedExercisesList.Remove(selectedExercise);
                            }

                        }

                        if (doesExist == false)
                            SelectedExercisesList.Add(exercise);
                    }



                }
            }
        }
        /// <summary>
        /// Deletes all of the selected exercises
        /// </summary>
        /// <param name="message"></param>
        private void ClearExercises(NotificationMessage message)
        {
            // Checks if the message notification matches
            if(message.Notification == "ClearExercises")
            {
                foreach(var exercise in SelectedExercisesList.ToList())
                {
                    exercise.IsSelected = false;
                }
                // Clears the list
                SelectedExercisesList.Clear();
            }
        }
        /// <summary>
        /// Creates new routine with <see cref="SelectedExercisesList"/>
        /// as its exercises
        /// </summary>
        /// <param name="parameter">Routine name </param>
        private async void CreateRoutine(object parameter)
        {
            // Casts routine name as parameter 
            var routineName = parameter as string;

            // Creates new routine 
            var newRoutine = new Routines
            {
                routineName = routineName as string,
                userId = "41790E2E-081B-4D65-B54A-C16F78D39398",
                id = Guid.NewGuid().ToString(),
            };
            // Adds new routine to the database
            db.Routines.Add(newRoutine);


            // Saves changes to the database
            try
            {
                db.SaveChanges();
            }
            catch(DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            /*
            // Looks for that routine in the database
            var routineId = db.Routines.Where(r => r.routineName == routineName && r.userId == newRoutine.userId).FirstOrDefault();

            // Adds exercises selected by the user to the database
            foreach(var exercise in SelectedExercisesList.ToList())
            {
                db.RoutineExercises.SqlQuery("INSERT INTO [RoutineExercises] (exerciseId, routineId)" +
                    "VALUES ({0}, {1})", exercise.id, routineId.id);
            }

            // Saves changes to the database
            await db.SaveChangesAsync();*/
        }
        #endregion
    }
}
