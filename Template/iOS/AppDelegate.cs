using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Test.NewSolution.iOS.Providers;
using Test.NewSolution.Forms;

namespace Test.NewSolution.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

            LoadApplication (new App (new TypeResolverProvider()));

			return base.FinishedLaunching (app, options);
		}
	}
}

