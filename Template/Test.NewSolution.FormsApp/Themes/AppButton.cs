using System;
using Xamarin.Forms;

namespace Test.NewSolution.FormsApp.Themes
{
    /// <summary>
    /// App button.
    /// </summary>
    public class AppButton: Button
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Test.NewSolution.FormsApp.Themes.AppButton"/> class.
        /// </summary>
        public AppButton()
        {
            FontFamily = FontConstants.DefaultFontName;
            FontSize = Device.GetNamedSize (NamedSize.Default, typeof(Button));
        }
    }
}

