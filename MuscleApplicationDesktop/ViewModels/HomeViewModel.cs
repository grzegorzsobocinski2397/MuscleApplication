using MuscleApplication.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MuscleApplication.Desktop
{
    public class HomeViewModel : BaseViewModel
    {
        #region Public properties
        /// <summary>
        /// Displays which page user is seeing in his homepage
        /// </summary>
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Profile;

        #endregion
        #region Constructor
        public HomeViewModel()
        {
            WorkoutCommand = new RelayCommand(() => ChangePage(ApplicationPage.Workout));
            ProfileCommand = new RelayCommand(() => ChangePage(ApplicationPage.Profile));
        }
        #endregion
        #region Commands
        /// <summary>
        /// Changes the page to WorkoutPage
        /// </summary>
        public ICommand WorkoutCommand { get; set; }
        /// <summary>
        /// Changes the page to ProfilePage
        /// </summary>
        public ICommand ProfileCommand { get; set; }
        /// <summary>
        /// Changes the page to LogsPage
        /// </summary>
        public ICommand LogsCommand { get; set; }
        #endregion
        #region Public Methods
        /// <summary>
        /// Changes the current page
        /// </summary>
        /// <param name="page"></param>
        public void ChangePage(ApplicationPage page)
        {
            
            CurrentPage = page;
        }
        #endregion

    }
}
