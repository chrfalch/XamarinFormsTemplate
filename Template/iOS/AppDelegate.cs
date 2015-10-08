using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Test.NewSolution.FormsApp;
using Test.NewSolution.iOS.Platform.IoC;

namespace Test.NewSolution.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();
            NControl.iOS.NControlViewRenderer.Init();
            NControl.Controls.iOS.NControls.Init();


            LoadApplication(new TouchAppBuilder().Build(new ContainerProvider()));

			return base.FinishedLaunching (app, options);
		}
	}
}

