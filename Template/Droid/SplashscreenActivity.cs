using System;
using Android.App;
using Android.OS;

namespace Test.NewSolution.Droid
{
    [Activity(MainLauncher = true, NoHistory = true)]
    public class SplashscreenActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            StartActivity(typeof(MainActivity));
        }
    }
}

