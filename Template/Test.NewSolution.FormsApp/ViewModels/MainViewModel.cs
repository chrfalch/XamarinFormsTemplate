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
            Title = Strings.AppName;
        }
            
        /// <summary>
        /// Gets the show menu command.
        /// </summary>
        /// <value>The show menu command.</value>
        public Command ShowMenuCommand
        {
            get{
                return GetOrCreateCommand(async () => {
                    IsBusy = true;
                    await Task.Delay(1500);
                    NavigationManager.ToggleDrawer();
                    IsBusy = false;
                });
            }
        }
    }
}

