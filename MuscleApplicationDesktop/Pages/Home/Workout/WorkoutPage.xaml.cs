

namespace MuscleApplication.Desktop
{
    /// <summary>
    /// Interaction logic for WorkoutPage.xaml
    /// </summary>
    public partial class WorkoutPage : BasePage<WorkoutViewModel>
    {
        
        public WorkoutPage()
        {
            InitializeComponent();
            DataContext = new WorkoutViewModel();
        }

        
    }
}
