using System;
using Xamarin.Forms;

namespace Test.NewSolution.FormsApp.Themes
{
    /// <summary>
    /// App label.
    /// </summary>
    public class AppLabel: Label
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Test.NewSolution.FormsApp.Themes.AppLabel"/> class.
        /// </summary>
        public AppLabel()
        {
            FontFamily = FontConstants.DefaultFontName;
            FontSize = Device.GetNamedSize (NamedSize.Default, typeof(Label));
        }
    }
}

