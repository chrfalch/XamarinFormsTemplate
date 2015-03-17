using System;
using Test.NewSolution.Forms.ViewModels;
using Xamarin.Forms;
using Test.NewSolution.Classes;
using Test.NewSolution.Forms.IoC;
using Test.NewSolution.Forms.Mvvm;

namespace Test.NewSolution.Forms.Views
{
    /// <summary>
    /// Master view.
    /// </summary>
    public class MasterView: MasterDetailPage
    {
        #region Private Members

        /// <summary>
        /// The on appearin called.
        /// </summary>
        private bool _onAppearinCalled = false;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Test.NewSolution.Forms.Views.MainView"/> class.
        /// </summary>
        public MasterView()
        {
            ViewModel = Container.Resolve<MasterViewModel>();

            // Set up master
            Master = Container.Resolve<MenuView>();

            // Set up mainview
            var mainView = Container.Resolve<MainView>();
            Detail = new NavigationPage(mainView);
            NavigationManager.SetMainPage(Detail);

            IsGestureEnabled = false;
        }

        #region Properties

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public MasterViewModel ViewModel { get; private set; }

        /// <summary>
        /// To be added.
        /// </summary>
        /// <returns>To be added.</returns>
        /// <remarks>To be added.</remarks>
        public override bool ShouldShowToolbarButton()
        {
            return true;
        }

        /// <summary>
        /// Raises the appearing event.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_onAppearinCalled)
                return;

            _onAppearinCalled = true;

            ViewModel.OnAppearingAsync();
        }

        #endregion
    }
}

