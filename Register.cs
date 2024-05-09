using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Firebase;
using Firebase.Auth;
using AndroidX.AppCompat.App;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.Firestore;
using Firebase.Firestore.Model;
using Android.Gms.Tasks;
using static Android.Views.View;
using Google.Android.Material.Snackbar;
using Java.Util;
using Android.Gms.Common.Data;
using AppDev_SleepTracker.Common;

namespace AppDev_SleepTracker
{
    [Activity(Label = "Register")]
    public class Register : AppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        public static FirebaseApp app;
        FirebaseFirestore database;
        Button btnRegister1;
        EditText email, password, confpass, fullname;
        TextView signin;
        LinearLayout register;
        FirebaseAuth auth;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.register);
            // Create your application here

            //Load firebase
            auth = FirebaseRepository.getFirebaseAuth();
            database = FirebaseRepository.GetFirebaseFirestore();

            //View
            fullname = FindViewById<EditText>(Resource.Id.editsignupFullname);
            btnRegister1 = FindViewById<Button>(Resource.Id.btnRegister1);
            email = FindViewById<EditText>(Resource.Id.editsignupUsername);
            password = FindViewById<EditText>(Resource.Id.editsignupPassword);
            confpass = FindViewById<EditText>(Resource.Id.editconfirmPassword);
            signin = FindViewById<TextView>(Resource.Id.txtsignin);

            register = FindViewById<LinearLayout>(Resource.Id.signupregister);
            btnRegister1.SetOnClickListener(this);
            signin.SetOnClickListener(this);
        }

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.txtsignin)
            {
                StartActivity(typeof (MainActivity));
            }
            else if (v.Id == Resource.Id.btnRegister1)
            {
                if (email.Text == string.Empty || password.Text == string.Empty || confpass.Text == string.Empty)
                {
                    Snackbar snackbar = Snackbar.Make(register, "Please enter your email/password", Snackbar.LengthShort);
                    snackbar.Show();
                }
                else if (confpass.Text != password.Text)
                {
                    Snackbar snackbar = Snackbar.Make(register, "Confirm Password doesn't match with password", Snackbar.LengthShort);
                    snackbar.Show();
                }
                else if (password.Text.Length < 6)
                {
                    Snackbar snackbar = Snackbar.Make(register, "Please make sure your password is at least 6 characters long", Snackbar.LengthShort);
                    snackbar.Show();
                }
                else
                {
                    if (email.Text.Contains("@mail.com") || email.Text.Contains("@gmail.com") || email.Text.Contains("@yahoo.com"))
                    {
                        RegisterFirestore(email.Text, password.Text);
                    }
                    else
                    {
                        Snackbar snackbar = Snackbar.Make(register, "Please make sure your email is valid'", Snackbar.LengthShort);
                        snackbar.Show();
                    }
                   
                }
            }
        }

        private void RegisterFirestore(string email, string password)
        {
            auth.CreateUserWithEmailAndPassword(email, password).AddOnCompleteListener(this, this);   
        }


        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                HashMap map = new HashMap();
                map.Put("fullname", fullname.Text);
                map.Put("email", email.Text);
                map.Put("password", confpass.Text);

                DocumentReference docRef = database.Collection("Users").Document(auth.CurrentUser.Uid);
                docRef.Set(map);

                Snackbar snackbar = Snackbar.Make(register, "Registered Successfully", Snackbar.LengthShort);
                
                snackbar.Show();

            }
            else if (task.IsSuccessful == false)
            {
                Exception exception = task.Exception;
                if (exception != null)
                {
                    string message = exception.Message;
                    // Show a message to the user
                    Snackbar snackbar = Snackbar.Make(register, message, Snackbar.LengthShort);
                    snackbar.Show();
                }
                else
                {
                    Snackbar snackbar = Snackbar.Make(register, "Register failed", Snackbar.LengthShort);
                    snackbar.Show();
                }
            }
            else
            {
                Exception exception = task.Exception;
                if (exception != null)
                {
                    string message = exception.Message;
                    // Show a message to the user
                }

                Snackbar snackbar = Snackbar.Make(register, "Error occur, please contact pyanz jheo quiros", Snackbar.LengthShort);
                snackbar.Show();
            }
        }
    }
}