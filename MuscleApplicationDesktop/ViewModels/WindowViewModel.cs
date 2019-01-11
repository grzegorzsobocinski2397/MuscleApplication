
using MuscleApplication.Desktop;
using System;
using System.Windows;
using System.Windows.Input;

namespace MuscleApplication.Desktop
{
    /// <summary>
    /// View Model for the custom flat window
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
       
        #region Private Properties
        /// <summary>
        /// The window this view model controls
        /// </summary>
        private Window window;
        /// <summary>
        /// Margin around the window to allow for a drop shadow
        /// </summary>
        private int outerMarginSize = 10;
        /// <summary>
        /// Radius of the edges (the more, the roundier)
        /// </summary>
        private int windowRadius = 10;
        /// <summary>
        /// The last known dock position
        /// </summary>
        private WindowDockPosition dockPosition = WindowDockPosition.Undocked;
        #endregion
        #region Public Properties
        /// <summary>
        /// The minimum width size a window can get 
        /// </summary>
        public double WindowMinimumWidth { get; set; } = 600;
        /// <summary>
        /// The minimum height size a window can get
        /// </summary>
        public double WindowMinimumHeight { get; set; } = 600;
        public bool Borderless { get { return (window.WindowState == WindowState.Maximized || dockPosition != WindowDockPosition.Undocked); } }

        /// <summary>
        /// The margin around the window that allows for a drop shadow
        /// </summary>
        public int OuterMarginSize
        {
            get
            {
                // Checks if the window is maximized 
                return Borderless ? 0 : outerMarginSize;
            }
            set
            {
                outerMarginSize = value;
            }
        }
        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public int WindowRadius
        {
            get
            {
                // Checks if the window is maximized
                return Borderless ? 0 : windowRadius;
            }
            set
            {
                windowRadius = value;
            }

        }
        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public Thickness OuterMarginSizeThickness => new Thickness(OuterMarginSize);


        /// <summary>
        /// The size of the reisize border around the window, taking into account the outer margin
        /// </summary>
        public Thickness ResizeBorderThickness => new Thickness(ResizeBorder + OuterMarginSize);
        /// <summary>
        /// The size of the resize border around the window (if the window is maximized it is 0)
        /// </summary>
        public int ResizeBorder { get { return Borderless ? 0 : 6; } }
        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public CornerRadius WindowCornerRadius => new CornerRadius(WindowRadius);
        /// <summary>
        /// The height of the title bar
        /// </summary>
        public int TitleHeight { get; set; } = 25;
        /// <summary>
        /// The height of the title bar
        /// </summary>
        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorder); } }
        /// <summary>
        /// The padding of the inner content of the main window
        /// </summary>
        public Thickness InnerContentPadding { get; set; } = new Thickness(0);
        /// <summary>
        /// Page in the MainWindow frame
        /// </summary>
        public ApplicationPage CurrentPage { get; set; }

        #endregion
        #region Constructor



        /// <summary>
        /// The default constructor
        /// </summary>
        /// 
        public WindowViewModel(Window window)
        {
            ChangePage(ApplicationPage.Login);

            this.window = window;
            window.StateChanged += (sender, e) =>
            {
                // Fire off events for all properties that are affected by a resize
                WindowResized();
            };

            // Fix window resize issue
            var resizer = new WindowResizer(window);

            // Listen out for dock changes
            resizer.WindowDockChanged += (dock) =>
            {
                // Store last position
                dockPosition = dock;
                // Fire off resize events
                WindowResized();
            };

            // Create commands
            MinimizeCommand = new RelayCommand(() => window.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => window.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => window.Close());
        }

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
        #region Commands
        /// <summary>
        /// The command to minimize window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }
        /// <summary>
        /// The command to maximize window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }
        /// <summary>
        /// The command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }
        /// <summary>
        /// The command to show the system menu when icon is clicked
        /// </summary>
        public ICommand MenuCommand { get; set; }
        #endregion
        #region Private Helpers
        private void WindowResized()
        {
            
            // Fire off events for all properties that are affected by a resize
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
            
        }

        
        #endregion
    }
}
