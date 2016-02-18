using System;
using Test.NewSolution.FormsApp;
using Test.NewSolution.Contracts.Repositories;
using Test.NewSolution.iOS.Platform.Repositories;
using NControl.Mvvm.iOS;
using NControl.Mvvm;

namespace Test.NewSolution.iOS
{
    /// <summary>
    /// Touch app platform.
    /// </summary>
    public class TouchAppPlatform: TouchPlatform
    {
        /// <summary>
        /// Initialize this instance.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            Container.Register<IConnectionProvider, TouchConnectionProvider>();
        }
    }
}

