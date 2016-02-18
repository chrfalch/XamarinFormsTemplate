using System;

using Xamarin.Forms;
using Test.NewSolution.Contracts.Services;
using Test.NewSolution.Data.Services;
using Test.NewSolution.Contracts.Models;
using Test.NewSolution.Data.Repositories;
using Test.NewSolution.Contracts.Repositories;
using System.Threading.Tasks;
using Test.NewSolution.FormsApp.Views;
using Test.NewSolution.FormsApp.ViewModels;
using NControl.Mvvm;
using Test.NewSolution.Contracts.Clients;
using Test.NewSolution.Data.Clients;
using Test.NewSolution.FormsApp.Themes;

namespace Test.NewSolution.FormsApp
{
    /// <summary>
    /// App.
    /// </summary>
	public class App : MvvmApp
	{                
        /// <summary>
        /// Initializes a new instance of the <see cref="Test.NewSolution.App"/> class.
        /// </summary>
        /// <param name="typeResolveProvider">Type resolve provider.</param>
        public App (IMvvmPlatform platform):base(platform){}

        /// <summary>
        /// Registers the views.
        /// </summary>
        protected override void RegisterViews()
        {
            ViewContainer.RegisterView<MainViewModel, MainView>();
        }
        /// <summary>
        /// Registers the services.
        /// </summary>
        protected override void RegisterServices()
        {
            base.RegisterServices();

            // Services
            Container.RegisterSingleton<IPreferenceService, PreferenceService>();
            Container.RegisterSingleton<ILoggingService, LoggingService>();

            // Clients
            Container.Register<IApiClientProvider, HttpApiClientProvider>();

            // Repositories
            Container.RegisterSingleton<IRepository<PreferenceModel>, Repository<PreferenceModel>>();
        }

        /// <summary>
        /// Gets the main page.
        /// </summary>
        /// <returns>The main page.</returns>
        protected override Page GetMainPage()
        {
            return new AppNavigationPage(Container.Resolve<MainView>());
        }

        #region App Lifecycle Callbacks

        /// <summary>
        /// Application developers override this method to perform actions when the application starts.
        /// </summary>
        /// <remarks>To be added.</remarks>
		protected override void OnStart ()
		{
			// Handle when your app starts
		}

        /// <summary>
        /// Application developers override this method to perform actions when the application enters the sleeping state.
        /// </summary>
        /// <remarks>To be added.</remarks>
		protected override void OnSleep ()
		{
			// Persist preferences
            Task.Run(async () => await Container.Resolve<IPreferenceService>().PersistAsync()).Wait();
		}

        /// <summary>
        /// Application developers override this method to perform actions when the application resumes from a sleeping state.
        /// </summary>
        /// <remarks>To be added.</remarks>
		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        #endregion
	}
}

