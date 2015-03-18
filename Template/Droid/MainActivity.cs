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

namespace Test.NewSolution.Droid
{
	[Activity (Label = "Test.NewSolution.Droid", Icon = "@drawable/icon", 
        MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

            LoadApplication (new App (new ContainerProvider(), (container) => {

                // Register providers
                container.Register<IRepositoryProvider, RepositoryProvider>();
                container.Register<IImageProvider, ImageProvider>();
            }));
		}
	}
}

