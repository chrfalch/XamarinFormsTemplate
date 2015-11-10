using System;
using Test.NewSolution.FormsApp.IoC;
using Test.NewSolution.Contracts.Services;
using Test.NewSolution.Contracts.Repositories;
using Test.NewSolution.Contracts.Models;
using Test.NewSolution.Data.Repositories;
using Test.NewSolution.Data.Services;
using Test.NewSolution.FormsApp.Mvvm;
using Test.NewSolution.FormsApp.ViewModels;
using Test.NewSolution.FormsApp.Views;
using Test.NewSolution.Contracts.Clients;
using Test.NewSolution.Data.Clients;

namespace Test.NewSolution.FormsApp
{
    /// <summary>
    /// App builder.
    /// </summary>
    public class AppBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Test.NewSolution.FormsApp.AppBuilder"/> class.
        /// </summary>
        public virtual App Build(IContainerProvider containerProvider)
        {
            SetupContainer(containerProvider);
            RegisterViews();
            SetupPlatform();

            return new App();
        }

        #region Virtual Members

        /// <summary>
        /// Registers and sets up platform specific implementations
        /// </summary>
        protected virtual void SetupPlatform()
        {
            
        }
        #endregion

        #region Private Members

        /// <summary>
        /// Setups the container.
        /// </summary>
        private void SetupContainer(IContainerProvider containerProvider)
        {
            Container.Initialize(containerProvider);

            // Services
            Container.RegisterSingleton<IPreferenceService, PreferenceService>();
            Container.RegisterSingleton<ILoggingService, LoggingService>();

            // Clients
            Container.Register<IApiClientProvider, HttpApiClientProvider>();

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

