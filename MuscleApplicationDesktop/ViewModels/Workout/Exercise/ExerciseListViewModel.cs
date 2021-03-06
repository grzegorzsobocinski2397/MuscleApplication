﻿using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MuscleApplication.Desktop
{
    /// <summary>
    /// View model for the routine exercises list that is placed in the <see cref="ExercisePage"/>
    /// </summary>
    public class ExerciseListViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// List containg all the routine exercises
        /// </summary>
        public ObservableCollection<ExerciseListItemViewModel> ExercisesList { get; set; }
        
        #endregion
        #region Commands
        /// <summary>
        /// Makes the <see cref="CreateExerciseControl"/> visible
        /// </summary>
        public ICommand CreateExerciseCommand { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ExerciseListViewModel()
        {
            // Create the list
            ExercisesList = new ObservableCollection<ExerciseListItemViewModel>();
            // Registers a messenger instance to populate exercises list
            MessengerInstance.Register<PropertyChangedMessage<string>>(this, GetExercises);
            // Creates commands
            CreateExerciseCommand = new RelayCommand(() => MessageOpenCreateExercisePage());
        }


        #endregion
        #region Public Methods
        /// <summary>
        /// Looks for the exercises of the selected routine
        /// </summary>

        public void GetExercises(PropertyChangedMessage<string> message)
        {



            // If message's property name matches then continue
            if (message.PropertyName == "GetExercises")
            {
                // Deselects other routines
                MessengerInstance.Send(new PropertyChangedMessage<string>("", message.NewValue, "DeselectRoutines"));

                // Stores the given routine id value 
                var currentRoutineId = message.NewValue;
                if (currentRoutineId != null)
                {
                    // Gets the routine exercises from the database
                    var routineExercises = db.RoutineExercises.Where(re => re.Id == currentRoutineId).ToList();
                    if (routineExercises != null)
                    {
                        // Clears the ObservableCollection just in case 
                        ExercisesList.Clear();
                        // Adds every exercise to the observable collection
                        foreach (var routineExercise in routineExercises)
                        {
                            // Searches the database for exercise that has the id of the RoutineExercise's exercise id
                            var exerciseFromDatabase = db.Exercises.Where(e => e.Id == routineExercise.ExerciseId).FirstOrDefault();

                            var exercise = new ExerciseListItemViewModel
                            {
                                id = exerciseFromDatabase.Id,
                                ExerciseName = exerciseFromDatabase.Name
                            };
                            // Adds the exercise to the collection
                            ExercisesList.Add(exercise);
                        }

                        // Clears the list containing routines from the database
                        routineExercises.Clear();
                    }
                }
            }

        }
        #endregion
        #region Private Methods
        private void MessageOpenCreateExercisePage()
        {
            // Sends the message 
            MessengerInstance.Send(new PropertyChangedMessage<string>("", "True", "OpenCreateExercise"));
        }
        #endregion

    }


}

