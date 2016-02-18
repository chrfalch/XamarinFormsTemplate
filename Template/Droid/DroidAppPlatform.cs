using System;
using Test.NewSolution.FormsApp;
using Test.NewSolution.Contracts.Repositories;
using Test.NewSolution.Droid.Platform.Repositories;
using NControl.Mvvm;
using Android.App;
using NControl.Mvvm.Droid;

namespace Test.NewSolution.Droid
{
    /// <summary>
    /// App composer.
    /// </summary>
    public class DroidAppPlatform: DroidPlatform
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="com.skjeri.app.Droid.DroidAppPlatform"/> class.
        /// </summary>
        /// <param name="activity">Activity.</param>
        public DroidAppPlatform(Activity activity): base(activity) { }

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            // Register providers
            Container.Register<IConnectionProvider, DroidConnectionProvider>();
        }
    }
}

