using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Test.NewSolution.FormsApp;
using Test.NewSolution.Droid.Platform.IoC;
using Test.NewSolution.Contracts.Repositories;
using Test.NewSolution.Droid.Platform.Repositories;
using Test.NewSolution.Droid.Platform.Mvvm;
using Test.NewSolution.FormsApp.Mvvm;
using Test.NewSolution.FormsApp.IoC;
using Acr.UserDialogs;
using Xamarin.Forms.Platform.Android;

namespace Test.NewSolution.Droid
{
    [Activity (Label = "Test.NewSolution", Icon = "@drawable/icon", 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
        /// <summary>
        /// Raises the create event.
        /// </summary>
        /// <param name="bundle">Bundle.</param>
		protected override void OnCreate (Bundle bundle)
		{
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;

			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
            NControl.Droid.NControlViewRenderer.Init();
            NControl.Controls.Droid.NControls.Init();
            UserDialogs.Init(this);

            LoadApplication(new DroidAppBuilder().Build(new ContainerProvider()));
		}
	}
}

