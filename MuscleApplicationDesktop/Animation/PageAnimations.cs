
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Animation;

namespace MuscleApplication.Desktop
{
    /// <summary>
    /// Helpers to animate pages in specific ways
    /// </summary>
    public static class PageAnimations
    {
        /// <summary>
        /// Adds a fade in and slide from the right animation 
        /// </summary>
        /// <param name="page">Page to animate on</param>
        /// <param name="seconds">How long the animation takes</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromRight(this Page page, float seconds)
        {
            //Create the storyboard 
            var sb = new Storyboard();

            // Add slide from right animation
            sb.AddSlideFromTheBottom(seconds, page.WindowHeight);

            // Add a fade in animation 
            sb.AddFadeIn(seconds, page.WindowHeight);

            // Begin the animation 
            sb.Begin(page);

            // Make the page visible
            page.Visibility = Visibility.Visible;

            // Wait for it to finish
            await Task.Delay((int)seconds * 1000);
        }
        /// <summary>
        /// Adds a fade out and slide to the left animation
        /// </summary>
        /// <param name="page">Page to animate on</param>
        /// <param name="seconds">How long the animation takes</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToLeft(this Page page, float seconds)
        {
            //Create the storyboard 
            var sb = new Storyboard();

            // Add slide from right animation
            sb.AddSlideToTheLeft(seconds, page.WindowHeight);

            // Add fade out animation
            sb.AddFadeOut(seconds, page.WindowHeight);

            // Begin the animation 
            sb.Begin(page);

            // Make the page visible
            page.Visibility = Visibility.Visible;

            // Wait for it to finish
            await Task.Delay((int)seconds * 1000);
        }
    }
}
