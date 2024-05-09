using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AppDev_SleepTracker.Common;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Google.Android.Material.Snackbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Views.View;

namespace AppDev_SleepTracker
{
    [Activity(Label = "Dashboard")]
    public class Dashboard : AppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        Button btnLogout;
        LinearLayout dashboardLayout; 
        public static FirebaseApp app;
        FirebaseAuth auth;
        FirebaseFirestore database;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);  
            SetContentView(Resource.Layout.dashboard);
            auth = FirebaseRepository.getFirebaseAuth();
            database = FirebaseRepository.GetFirebaseFirestore();
            btnLogout = FindViewById<Button>(Resource.Id.btnLogout);
            dashboardLayout = FindViewById<LinearLayout>(Resource.Id.dashboardlayout);

            btnLogout.SetOnClickListener(this);


        }

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.btnLogout)
            {
                StartActivity(typeof(MainActivity));
                Finish();
            }
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                Snackbar snackbar = Snackbar.Make(dashboardLayout, "Logout successfully ", Snackbar.LengthShort);
                snackbar.Show();
                StartActivity(new Android.Content.Intent(this, typeof(MainActivity)));
                Finish();
            }
            else if (task.IsSuccessful == false)
            {
                Snackbar snackbar = Snackbar.Make(dashboardLayout, "Logout failed", Snackbar.LengthShort);
                snackbar.Show();
            }
        }
    }
}