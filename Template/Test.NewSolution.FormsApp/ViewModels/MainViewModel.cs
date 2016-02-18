using System;
using Test.NewSolution.Localization;
using Xamarin.Forms;
using System.Threading.Tasks;
using NControl.Mvvm;

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
    }
}

