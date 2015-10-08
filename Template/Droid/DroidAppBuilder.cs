using System;
using Test.NewSolution.FormsApp.IoC;
using Test.NewSolution.FormsApp;
using Test.NewSolution.Contracts.Repositories;
using Test.NewSolution.FormsApp.Mvvm;
using Test.NewSolution.Droid.Platform.Mvvm;
using Test.NewSolution.Droid.Platform.Repositories;

namespace Test.NewSolution.Droid
{
    /// <summary>
    /// App composer.
    /// </summary>
    public class DroidAppBuilder: AppBuilder
    {
        /// <summary>
        /// Registers and sets up platform specific implementations
        /// </summary>
        protected override void SetupPlatform()
        {
            base.SetupPlatform();

            // Register providers
            Container.Register<IConnectionProvider, DroidConnectionProvider>();
            Container.Register<IImageProvider, DroidImageProvider>();
        }
    }
}

