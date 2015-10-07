using System;
using Test.NewSolution.FormsApp.ViewModels;
using Xamarin.Forms;
using Test.NewSolution.FormsApp.Controls;
using Test.NewSolution.FormsApp.Mvvm;

namespace Test.NewSolution.FormsApp.Views
{
    /// <summary>
    /// Main view.
    /// </summary>
    public class MainView: BaseContentsView<MainViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Test.NewSolution.FormsApp.Views.MainView"/> class.
        /// </summary>
        public MainView()
        {
            DefaultBackButtonBehaviour = true;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        /// <summary>
        /// Implement to create the layout on the page
        /// </summary>
        /// <returns>The layout.</returns>
        protected override View CreateContents()
        {
            return new Button{
                Text = "Click",
                BackgroundColor = Color.Transparent,
                Command = new Command((obj) => {
                    NavigationManager.ToggleDrawer();
                })
            };            
        }
    }
}

