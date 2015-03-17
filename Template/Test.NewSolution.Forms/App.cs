using System;

using Xamarin.Forms;
using Test.NewSolution.Forms.IoC;
using Test.NewSolution.Contracts.Services;
using Test.NewSolution.Data.Services;
using Test.NewSolution.Contracts.Models;
using Test.NewSolution.Data.Repositories;
using Test.NewSolution.Repositories;
using System.Threading.Tasks;
using Test.NewSolution.Forms.Views;
using Test.NewSolution.Forms.Providers;
using Test.NewSolution.Forms.ViewModels;
using Test.NewSolution.Forms.Mvvm;

namespace Test.NewSolution.Forms
{
    /// <summary>
    /// App.
    /// </summary>
	public class App : Application
	{                
        #region Private Members

        /// <summary>
        /// The initialized.
        /// </summary>
        private bool _initialized = false;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Test.NewSolution.App"/> class.
        /// </summary>
        /// <param name="typeResolveProvider">Type resolve provider.</param>
        public App (IContainerProvider containerProvider, Action<IContainerProvider> setupContainerCallback)
		{
            // Save container
            Container = containerProvider;

            // Only fill container if it has not been filled yet
            if (!_initialized)
            {
                _initialized = true;

                // Set up container
                SetupContainer();

                // Let the caller setup its container
                if (setupContainerCallback != null)
                    setupContainerCallback(Container);

                // Register views
                RegisterViews();
            }

            // Initialize 
            Task.Run(async() => {

                // Initialize services
                await Container.Resolve<IPreferenceService>().InitializeAsync().ContinueWith(t => 
                    Container.Resolve<ILoggingService>().InitializeAsync());

            });

            // The root page of your application
            MainPage = Container.Resolve<MasterView>();
		}

        #region App Properties

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>The container.</value>
        public IContainerProvider Container { get; private set;}

        #endregion

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

        #region Private Members

        /// <summary>
        /// Setups the container.
        /// </summary>
        private void SetupContainer()
        {
            // Services
            Container.RegisterSingleton<IPreferenceService, PreferenceService>();
            Container.RegisterSingleton<ILoggingService, LoggingService>();

            // Repositories
            Container.RegisterSingleton<IRepository<PreferenceModel>, Repository<PreferenceModel>>();
        }

        /// <summary>
        /// Registers the views.
        /// </summary>
        private void RegisterViews()
        {
            ViewManager.RegisterView<MasterViewModel, MasterView>();
            ViewManager.RegisterView<MenuViewModel, MenuView>();
            ViewManager.RegisterView<MainViewModel, MainView>();
        }
        #endregion
	}
}

