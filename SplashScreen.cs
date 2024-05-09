using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDev_SleepTracker
{
    
    [Activity(Label = "@string/app_name", Theme = "@style/Splash" , MainLauncher = true, NoHistory = true)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.splash);
            // Create your application here
        }

        protected override async void OnResume()
        {
            base.OnResume();
            await SimulateStartup();
        }

        async Task SimulateStartup()
        {
            await Task.Delay(TimeSpan.FromSeconds(4));
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}