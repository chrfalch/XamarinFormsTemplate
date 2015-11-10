using System;
using Test.NewSolution.Localization;
using Xamarin.Forms;
using System.Threading.Tasks;
using Test.NewSolution.FormsApp.Mvvm;

namespace Test.NewSolution.FormsApp.ViewModels
{
    /// <summary>
    /// Main view model.
    /// </summary>
    public class MainViewModel: BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Test.NewSolution.FormsAppViewModels.MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
        }

        /// <summary>
        /// Returns the view title
        /// </summary>
        /// <value>The view title.</value>
        public override string Title
        {
            get
            {
                return Strings.AppName;
            }
        }

        /// <summary>
        /// Gets the show menu command.
        /// </summary>
        /// <value>The show menu command.</value>
        public Command ShowMenuCommand
        {
            get{
                return GetOrCreateCommand(new Command(async (obj) => {
                    IsBusy = true;
                    await Task.Delay(1500);
                    NavigationManager.ToggleDrawer();
                    IsBusy = false;
                }));
            }
        }
    }
}

