

using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace MuscleApplication.Desktop
{
    /// <summary>
    /// Animation helpers for Storyboard
    /// </summary>
    public static class StoryboardHelpers
    {
        /// <summary>
        /// Adds a slide animation to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="offset">Page width</param>
        /// <param name="decelerationRatio"></param>
        public static void AddSlideFromTheBottom(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f)
        {
            // Create the margin animate from the bottom
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0, offset, 0, -offset),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio
            };
            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Add the animation to the storyboard
            storyboard.Children.Add(animation);
        }
        /// <summary>
        /// Adds a fade in animation to the storyboard
        /// </summary>
        /// <param name="storyboard"></param>
        /// <param name="seconds"></param>
        /// <param name="offset"></param>
        /// <param name="decelerationRatio"></param>
        public static void AddFadeIn(this Storyboard storyboard, float seconds, double offset)
        {
            // Create the margin animate from the bottom
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 0,
                To = 1,
            };
            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

            // Add the animation to the storyboard
            storyboard.Children.Add(animation);
        }
        /// <summary>
        /// Adds a slide animation to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="offset">Page width</param>
        /// <param name="decelerationRatio"></param>
        public static void AddSlideToTheLeft(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f)
        {
            // Create the margin animate from right
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0),
                To = new Thickness(-offset, 0, offset, 0),
                DecelerationRatio = decelerationRatio
            };
            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Add the animation to the storyboard
            storyboard.Children.Add(animation);
        }
        /// <summary>
        /// Adds a fade out animation to the storyboard
        /// </summary>
        /// <param name="storyboard"></param>
        /// <param name="seconds"></param>
        /// <param name="offset"></param>
        /// <param name="decelerationRatio"></param>
        public static void AddFadeOut(this Storyboard storyboard, float seconds, double offset)
        {
            // Create the margin animate from right
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 1,
                To = 0,
            };
            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

            // Add the animation to the storyboard
            storyboard.Children.Add(animation);
        }
    }
}
