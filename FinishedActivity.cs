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
    [Activity(Label = "FinishedActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Locked)]
    public class FinishedActivity : Activity
    {
        Button BackToHome;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Finished_Layout);
            // Create your application here
            BackToHome = FindViewById<Button>(Resource.Id.btnBackHome);
            BackToHome.Click += BackToHome_Click;
        }

        private void BackToHome_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Home_Activity));
            StartActivity(intent);
        }
    }
}