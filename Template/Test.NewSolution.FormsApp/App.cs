using System;

using Xamarin.Forms;
using Test.NewSolution.FormsApp.IoC;
using Test.NewSolution.Contracts.Services;
using Test.NewSolution.Data.Services;
using Test.NewSolution.Contracts.Models;
using Test.NewSolution.Data.Repositories;
using Test.NewSolution.Repositories;
using System.Threading.Tasks;
using Test.NewSolution.FormsApp.Views;
using Test.NewSolution.FormsApp.Providers;
using Test.NewSolution.FormsApp.ViewModels;
using Test.NewSolution.FormsApp.Mvvm;

namespace Test.NewSolution.FormsApp
{
    /// <summary>
    /// App.
    /// </summary>
	public class App : Application
	{                
        /// <summary>
        /// Initializes a new instance of the <see cref="Test.NewSolution.App"/> class.
        /// </summary>
        /// <param name="typeResolveProvider">Type resolve provider.</param>
        public App ()
		{
            // The root page of your application
            MainPage = Container.Resolve<MasterView>();
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

