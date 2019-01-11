using MuscleApplication.Desktop;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MuscleApplication.Desktop
{


    public class BasePage : Page
    {
        #region Public Properties
        /// <summary>
        /// The animation that plays when page is loaded
        /// </summary>
        public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.SlideAndFadeInFromRight;
        /// <summary>
        /// The animation that play when page is unloaded
        /// </summary>
        public PageAnimation PageUnLoadAnimation { get; set; } = PageAnimation.SlideAndFadeOutToLeft;
        /// <summary>
        /// The time any slide animation takes to complete
        /// </summary>
        public float SlideSeconds { get; set; } = 0.8f;
        #endregion
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public BasePage()
        {
            // If we are animating in, hide to begin with
            if (this.PageLoadAnimation != PageAnimation.None)
                this.Visibility = Visibility.Collapsed;

            //Listen out for the page loading
            this.Loaded += BasePage_LoadedAsync;
        }
        #endregion

        #region Animation Load/Unload
        /// <summary>
        /// Once the page is loaded, perform any required animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BasePage_LoadedAsync(object sender, RoutedEventArgs e)
        {
            await AnimateInAsync();
        }

        public async Task AnimateInAsync()
        {

            if (PageLoadAnimation == PageAnimation.None)
                return;

            switch (PageLoadAnimation)
            {
                case PageAnimation.SlideAndFadeInFromRight:
                    // Start the animation
                    await this.SlideAndFadeInFromRight(SlideSeconds);
                    break;
            }

        }

        public async Task AnimateOutAsync()
        {

            if (PageUnLoadAnimation == PageAnimation.None)
                return;

            switch (PageLoadAnimation)
            {
                case PageAnimation.SlideAndFadeOutToLeft:
                    // Start the animation
                    await this.SlideAndFadeOutToLeft(SlideSeconds);
                    break;
            }
        }
        #endregion


    }

    /// <summary>
    /// A base page for all pages to gain base functionality
    /// </summary>
    public class BasePage<VM> : BasePage
        where VM : BaseViewModel, new()
    {
        #region Private Members
        private VM viewModel;
        #endregion
        #region Public Properties
        public VM ViewModel
        {
            get { return viewModel;
            }
            set
            {
                // Checks if anything changed...
                if (viewModel == value)
                    return;
                // ...if something changed then update the value...
                viewModel = value;
                // ...and set the data context of this page
                this.DataContext = viewModel;
            }
        }
        #endregion
        #region Default Constructor
        public BasePage()
        {
            ViewModel = new VM();
        }
        #endregion

    }
   
}
