using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Test.NewSolution.iOS.Platform.IoC;
using Test.NewSolution.FormsApp;
using Test.NewSolution.Contracts.Repositories;
using Test.NewSolution.iOS.Platform.Repositories;
using Test.NewSolution.FormsApp.Mvvm;
using Test.NewSolution.iOS.Platform.Mvvm;

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
                container.Register<IImageProvider, ImageProvider>();

            }));

			return base.FinishedLaunching (app, options);
		}
	}
}

