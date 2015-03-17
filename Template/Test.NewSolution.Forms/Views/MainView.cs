using System;
using Test.NewSolution.Forms.ViewModels;
using Xamarin.Forms;

namespace Test.NewSolution.Forms.Views
{
    /// <summary>
    /// Main view.
    /// </summary>
    public class MainView: BaseContentsView<MainViewModel>
    {
        /// <summary>
        /// Implement to create the layout on the page
        /// </summary>
        /// <returns>The layout.</returns>
        protected override View CreateContents()
        {
            return new StackLayout();
        }
    }
}

