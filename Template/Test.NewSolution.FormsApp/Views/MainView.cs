using System;
using Test.NewSolution.FormsApp.ViewModels;
using Xamarin.Forms;
using Test.NewSolution.FormsApp.Controls;

namespace Test.NewSolution.FormsApp.Views
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
            return new StackLayout{
                Children = {
                    new FontAwesomeLabel{Text = FontAwesomeLabel.FAAlignJustify}
                }
            };
        }
    }
}

