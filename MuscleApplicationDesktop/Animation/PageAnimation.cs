using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuscleApplication.Desktop
{
    /// <summary>
    /// Styles of page animations 
    /// </summary>
    public enum PageAnimation
    {
        /// <summary>
        /// No animation
        /// </summary>
        None = 0,
        /// <summary>
        /// Page slides from the right 
        /// </summary>
        SlideAndFadeInFromRight = 1,
        /// <summary>
        /// Page slides out to the left
        /// </summary>
        SlideAndFadeOutToLeft = 2,

    }
}
