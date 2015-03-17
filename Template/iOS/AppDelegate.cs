using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Test.NewSolution.iOS.Native.IoC;
using Test.NewSolution.Forms;
using Test.NewSolution.Contracts.Repositories;
using Test.NewSolution.iOS.Native.Repositories;

namespace Test.NewSolution.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

            LoadApplication (new App (new ContainerProvider(), (container) => {

                // Register providers
                container.Register<IRepositoryProvider, RepositoryProvider>();

            }));

			return base.FinishedLaunching (app, options);
		}
	}
}

