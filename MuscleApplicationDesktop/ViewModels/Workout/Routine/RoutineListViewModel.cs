using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MuscleApplication.Desktop
{
    public class RoutineListViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// List for the routines made by user 
        /// </summary>
        public ObservableCollection<RoutineListItemViewModel> RoutinesList { get; set; }
        #endregion
        #region Constructor
        public RoutineListViewModel()
        {
            // Creates new observable collections
            RoutinesList = new ObservableCollection<RoutineListItemViewModel>();
            // Gets the routines from the database
            UpdateRoutines();
            // Register a messenger instance to deselect other routines
            MessengerInstance.Register<PropertyChangedMessage<string>>(this, DeselectRoutines);
            // Creates commands
            CreateRoutineCommand = new RelayCommand(() => MessageoOpenCreateRoutinePage());
        }
        #endregion

        #region Commands
        /// <summary>
        /// Navigates user to the create routine page
        /// </summary>
        public ICommand CreateRoutineCommand { get; set; }
        #endregion
        #region Public Methods
       

        /// <summary>
        /// Gets the user routines from the database and populates <see cref="Routines"/>
        /// </summary>
        public void UpdateRoutines()
        {
            try
            {
                // Gets the routines from database 
                var routines = db.Routines.ToList();
                
                if (routines != null)
                {
                    // Clears the ObservableCollection just in case 
                    RoutinesList.Clear();
                    // Adds every item to the observable collection
                    foreach (var routine in routines)
                    {
                        // Creates a new routineListItem so I can access commands 
                        var routineListItem = new RoutineListItemViewModel
                        {
                            id = routine.id,
                            createdAt = routine.createdAt,
                            routineName = routine.routineName,
                            userId = routine.userId,
                            lastUsed = routine.lastUsed

                        };
                        // Adds new item to the list
                        RoutinesList.Add(routineListItem);
                    }
                    // Clears the list containing routines from the database
                    routines.Clear();
                }
            }
            catch (Exception)
            {
            }
        }
        public void DeselectRoutines(PropertyChangedMessage<string> message)
        {
            if(message.PropertyName == "DeselectRoutines")
            {
                foreach(var routine in RoutinesList)
                {
                    if(routine.id != message.NewValue)
                    {
                        routine.IsSelected = false;
                    }
                    else
                    {
                        routine.IsSelected = true;
                    }
                }
            }
        }
        #endregion
        #region Private Methods
        /// <summary>
        /// Sends the message to the workout view model to display the Open Create Routine Page
        /// </summary>
        private void MessageoOpenCreateRoutinePage()
        {
            // Sends the message 
            MessengerInstance.Send(new PropertyChangedMessage<string>("", "True", "OpenCreateRoutine"));
        }
        #endregion
    }
}
