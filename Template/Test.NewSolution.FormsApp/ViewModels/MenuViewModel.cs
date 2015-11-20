using System;
using Test.NewSolution.Localization;

namespace Test.NewSolution.FormsApp.ViewModels
{
    /// <summary>
    /// Menu view model.
    /// </summary>
    public class MenuViewModel: BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Test.NewSolution.FormsAppViewModels.MenuViewModel"/> class.
        /// </summary>
        public MenuViewModel()
        {
            Title = Strings.TitleMenu;
        }
            
    }
}

