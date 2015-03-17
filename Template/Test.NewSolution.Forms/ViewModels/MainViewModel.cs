using System;
using Test.NewSolution.Localization;

namespace Test.NewSolution.Forms.ViewModels
{
    /// <summary>
    /// Main view model.
    /// </summary>
    public class MainViewModel: BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Test.NewSolution.Forms.ViewModels.MainViewModel"/> class.
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
    }
}

