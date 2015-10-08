using System;
using Test.NewSolution.FormsApp;
using Test.NewSolution.FormsApp.IoC;
using Test.NewSolution.Contracts.Repositories;
using Test.NewSolution.iOS.Platform.Repositories;
using Test.NewSolution.iOS.Platform.Mvvm;
using Test.NewSolution.FormsApp.Mvvm;

namespace Test.NewSolution.iOS
{
    public class TouchAppBuilder: AppBuilder
    {
        /// <summary>
        /// Registers and sets up platform specific implementations
        /// </summary>
        protected override void SetupPlatform()
        {
            base.SetupPlatform();

            Container.Register<IConnectionProvider, TouchConnectionProvider>();
            Container.Register<IImageProvider, TouchImageProvider>();
        }
    }
}

