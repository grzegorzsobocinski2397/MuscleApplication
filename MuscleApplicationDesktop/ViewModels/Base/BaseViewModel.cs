using GalaSoft.MvvmLight;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace MuscleApplication.Desktop
{
    /// <summary>
    /// Base class for the view models that inherits from the MVVM Light ViewModelBase and 
    /// INotifyPropertyChanged
    /// </summary>
    public class BaseViewModel : ViewModelBase, INotifyPropertyChanged
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the BaseViewModel class.
        /// </summary>
        public BaseViewModel()
        {
            // Creates a connection to the database 
            db = new MuscleApplication_dbEntities();
            // 
            LoadDataCommand = new RelayCommand(() =>
            {
                Data = Guid.NewGuid();
            });
        }
        #endregion
        #region Public Properties
        /// <summary>
        /// The current logged user in the application
        /// </summary>
        public Users CurrentUser { get; set; }
        /// <summary>
        /// The event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        /// <summary>
        /// Connection to the database
        /// </summary>
        public MuscleApplication_dbEntities db { get; set; }
        
        public ICommand LoadDataCommand { get; private set; }
        
        public Guid Data
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
                this.RaisePropertyChanged("Data");
            }
        }
        #endregion
        #region Private Members and Events
        private Guid data;
        /// <summary>
        /// Call this to fire a <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
