using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MuscleApplication.Desktop
{
    public class RoutineListItemViewModel : BaseViewModel
    {
        #region Public Properties 
        /// <summary>
        /// Id of the routine
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Routine name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// User that created this routine
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// When was this routine last used
        /// </summary>
        public DateTime? LastUsed { get; set; }
        /// <summary>
        /// True if selected
        /// </summary>
        public bool IsSelected { get; set; }
       
        #endregion
        #region Commands
        /// <summary>
        /// Opens the selected routine 
        /// </summary>
        public ICommand OpenRoutineCommand { get; set; }

        #endregion
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public RoutineListItemViewModel()
        {
            // Creates commands
            OpenRoutineCommand = new RelayParameterCommand((parameter) => SendRoutineId(parameter));
        }

        #endregion
        #region Public Methods
        /// <summary>
        /// Sends a routine id to the parent view model
        /// </summary>
        /// <param name="parameter">Clicked routine's id </param>
        public void SendRoutineId(object parameter)
        {
            IsSelected = true;
            // Stores a routine id
            var routineId = parameter as string;
            // Sends a MVVM Light Message
            MessengerInstance.Send(new PropertyChangedMessage<string>("", routineId, "OpenRoutine"));
        }
        #endregion
    }
}
