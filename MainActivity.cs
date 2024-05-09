using Android.App;
using Android.OS;
using Android.Runtime;
using Firebase;
using Firebase.Auth;
using AndroidX.AppCompat.App;
using Firebase.Firestore;
using Android.Views;
using static Android.Views.View;
using Android.Gms.Tasks;
using Android.Widget;
using Google.Android.Material.Snackbar;
using System;
using AppDev_SleepTracker.Common;

namespace AppDev_SleepTracker
{
    [Activity(Label = "@string/app_name", MainLauncher = false  , Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        public static FirebaseApp app;
        FirebaseAuth auth;
        FirebaseFirestore database;

        Button btnLogin, btnRegister;
        EditText email, password;
        TextView forgotPass;
        LinearLayout activity_main;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            auth = FirebaseRepository.getFirebaseAuth();
            database = FirebaseRepository.GetFirebaseFirestore();

            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
            email = FindViewById<EditText>(Resource.Id.editUsername);
            password = FindViewById<EditText>(Resource.Id.editPassword);
            forgotPass = FindViewById<TextView>(Resource.Id.forgotPassword);
            activity_main = FindViewById<LinearLayout>(Resource.Id.loginlayout);

            btnLogin.SetOnClickListener(this);
            btnRegister.SetOnClickListener(this);
            forgotPass.SetOnClickListener(this);

            auth = FirebaseAuth.Instance;

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        //public FirebaseFirestore InitFirebaseAuth()
        //{
        //    if (app == null)
        //    {
        //        var options = new FirebaseOptions.Builder()
        //            .SetProjectId("sleeptracker-88cfa")
        //            .SetApplicationId("1:95367222695:android:b7c7aaf4a080fea16c5282")
        //            .SetApiKey("AIzaSyCTFUuY-w7Xp9JmuJT2q1_pVvOIw7PqO1I")
        //            .SetStorageBucket("sleeptracker-88cfa.appspot.com")
        //            .Build();

        //        app = FirebaseApp.InitializeApp(this, options);
        //        database = FirebaseFirestore.GetInstance(app);
        //    }
        //    else
        //    {
        //        database = FirebaseFirestore.GetInstance(app);
        //    }

        //    return database;
        //}

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.forgotPassword)
            {
                StartActivity(new Android.Content.Intent(this, typeof(ForgotPassword)));
                Finish();
            }
            else if (v.Id == Resource.Id.btnRegister)
            {
                StartActivity(new Android.Content.Intent(this, typeof(Register)));
                Finish();
            }
            else if (v.Id == Resource.Id.btnLogin)
            {
                if(email.Text == string.Empty || password.Text == string.Empty)
                {
                    Snackbar snackbar = Snackbar.Make(activity_main, "Please enter your email/password", Snackbar.LengthShort);
                    snackbar.Show();
                }
                else
                {
                    Login(email.Text, password.Text);
                }

            }
        }

        private void Login (string email, string password)
        {
            auth.SignInWithEmailAndPassword(email, password).AddOnCompleteListener(this);
        }
        public void OnComplete(Task task)
        {

            if (task.IsSuccessful)
            {
                StartActivity(new Android.Content.Intent(this, typeof(Dashboard)));
                Finish();
            }
            else if (task.IsSuccessful == false)
            {
                Snackbar snackbar = Snackbar.Make(activity_main, "Email/Password is incorrect, please try again", Snackbar.LengthShort);
                snackbar.Show();
            }
            else
            {
                Exception exception = task.Exception;
                if (exception != null)
                {
                    string message = exception.Message;
                    // Show a message to the user
                }

                Snackbar snackbar = Snackbar.Make(activity_main, "Error occur, please contact pyanz jheo quiros", Snackbar.LengthShort);
                snackbar.Show();
            }
        }
 
    }
}