using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace clash_of_blocks
{
    [Activity(Label = "Register_Activity")]
    public class Register_Activity : Activity
    {
        EditText etPhoneNumberRegister;
        EditText etPasswordRegister;
        EditText etNameRegister;
        EditText etUserNameRegister;
        Button btnSaveRegister;
        ISharedPreferences sp;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Register_Layout);
            // Create your application here
            etNameRegister = FindViewById<EditText>(Resource.Id.etNameRegister);
            etPasswordRegister = FindViewById<EditText>(Resource.Id.etPasswordRegister);
            etPhoneNumberRegister = FindViewById<EditText>(Resource.Id.etPhoneNumberRegister);
            etUserNameRegister = FindViewById<EditText>(Resource.Id.etUserNameRegister);
            btnSaveRegister = FindViewById<Button>(Resource.Id.btnSaveRegister);
            btnSaveRegister.Click += BtnSaveRegister_Click;

            sp = GetSharedPreferences("details", FileCreationMode.Private);
        }

        private void BtnSaveRegister_Click(object sender, EventArgs e)
        {
            if (CheckFields())
            {
                if (User.AddUser(etNameRegister.Text, etPhoneNumberRegister.Text, etPasswordRegister.Text, etUserNameRegister.Text))
                {
                    Toast.MakeText(this, "welcome " + etNameRegister.Text + " enjoy", ToastLength.Long).Show();
                    sp.Edit().PutInt("userId", User.GetId(etUserNameRegister.Text)).Apply();
                    Intent intent = new Intent(this, typeof(Home_Activity));
                    StartActivity(intent);
                }
                else
                    Toast.MakeText(this, "User name is already used", ToastLength.Short).Show();
            }
        }
        
        public bool CheckFields()
        {
            if (etNameRegister.Text == "" || etPasswordRegister.Text == "" || etPhoneNumberRegister.Text == "" || etUserNameRegister.Text == "")
            {
                if (etNameRegister.Text.Length == 0)
                {
                    etNameRegister.SetError("you must complete this field", null);
                    etUserNameRegister.RequestFocus();
                }

                if (etUserNameRegister.Text.Length == 0)
                {
                    etUserNameRegister.SetError("you must complete this field", null);
                    etUserNameRegister.RequestFocus();
                }

                if (etPasswordRegister.Text.Length == 0)
                {
                    etPasswordRegister.SetError("you must complete this field", null);
                    etUserNameRegister.RequestFocus();
                }
                return false;
            }
            return true;
        }
    }
}