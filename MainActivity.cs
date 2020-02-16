using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;

namespace clash_of_blocks
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, Icon ="@drawable/mainpic1", ScreenOrientation = Android.Content.PM.ScreenOrientation.Locked)]
    public class MainActivity : Activity
    {
        Button btnLogin;
        Button btnRegister;
        ISharedPreferences sp;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);

            btnLogin.Click += BtnLogin_Click;
            btnRegister.Click += BtnRegister_Click;

            SqlHelper.GetConnection().CreateTable<User>();
            SqlHelper.GetConnection().CreateTable<Record>();

            

            sp = GetSharedPreferences("details", FileCreationMode.Private);

            int id = sp.GetInt("userId", -1);
            if (id != -1) // User connected
            {
                Intent intent = new Intent(this, typeof(Home_Activity));
                StartActivity(intent);
                string name = User.GetName(sp.GetInt("userId", -1));
                Toast.MakeText(this, "welcome " + name + " enjoy", ToastLength.Long).Show();
            }
        }

        private void BtnRegister_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Register_Activity));
            StartActivity(intent);
        }

        private void BtnLogin_Click(object sender, System.EventArgs e)
        {
            Dialog d = new Dialog(this);
            d.SetContentView(Resource.Layout.Dialog_Login);
            EditText etUsername = d.FindViewById<EditText>(Resource.Id.editTextUsernameDialog_Login);
            EditText etPassword = d.FindViewById<EditText>(Resource.Id.editTextPasswordDialog_Login);
            Button btnSave = d.FindViewById<Button>(Resource.Id.btnContinueDialog_Login);
            btnSave.Click += (senderSave, eSave)=>
            {
                
                if (User.IsUserNameAndPassWordMatch(etUsername.Text, etPassword.Text))
                {
                    sp.Edit().PutInt("userId", User.GetId(etUsername.Text)).Apply();
                    Intent intent = new Intent(this, typeof(Home_Activity));
                    StartActivity(intent);
                    d.Dismiss();
                }
                else
                {
                    Toast.MakeText(this, "incorrect user name or password", ToastLength.Short).Show();
                }
            };
            d.Show();
        }
    }
}