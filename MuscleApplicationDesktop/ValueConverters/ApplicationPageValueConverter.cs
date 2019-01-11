using MuscleApplication.Desktop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuscleApplication.Desktop
{
    /// <summary>
    /// Converts the <see cref="ApplicationPage"/> to an actual pagge
    /// </summary>
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Find the page that was sent to convert
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.Homepage:
                    return new HomePage();
                case ApplicationPage.Login:
                    return new LoginPage();
                case ApplicationPage.Profile:
                    return new ProfilePage();
                case ApplicationPage.Workout:
                    return new WorkoutPage();
                default:
                    Debugger.Break();
                    return null;
                    
            }

        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
