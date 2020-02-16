using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace clash_of_blocks
{
    [Activity(Label = "Home_Activity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Locked)]
    public class Home_Activity : AppCompatActivity
    {
        Button btnInstructions;
        Button btnPlay;
        Button btnLeaderBoards;
        ISharedPreferences sp;
        BroadcastReceiver MyMusic;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Home_Layout);

            btnInstructions = FindViewById<Button>(Resource.Id.btnInstructions);
            btnPlay = FindViewById<Button>(Resource.Id.btnPlay);
            btnLeaderBoards = FindViewById<Button>(Resource.Id.btnLeaderBoards);

            btnInstructions.Click += BtnInstructions_Click;
            btnPlay.Click += BtnPlay_Click;
            btnLeaderBoards.Click += BtnLeaderBoards_Click;


            sp = GetSharedPreferences("details", FileCreationMode.Private);

            Intent i = new Intent(this, typeof(BackGroundMusic));
            StartService(i);

            this.MyMusic = new BackGroundMusicReciever();
            RegisterReceiver(this.MyMusic, new IntentFilter("my.music"));
            
        }

        private void BtnLeaderBoards_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RecordsForLvl));
            StartActivity(intent);
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this,typeof(Game_Activity));
            StartActivity(intent);
        }

        

        private void BtnInstructions_Click(object sender, System.EventArgs e)
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(this);
            builder.SetTitle("Instructiuons");
            builder.SetMessage(Resource.String.InstructionsText);
            builder.SetPositiveButton("confirm", (senderDialog, eDialog) =>
            {
                Toast.MakeText(this, "lets play then", ToastLength.Long).Show();
            });
            Dialog d = builder.Create();
            d.Show();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Menu.GameMenu, menu);

            
            return base.OnCreateOptionsMenu(menu);

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_Logout:
                    {
                        sp.Edit().PutInt("userId", -1).Apply();
                        this.Finish();
                        Intent intent = new Intent(this, typeof(MainActivity));
                        StartActivity(intent);
                        break;
                    }
                case Resource.Id.action_stop:
                    {
                        Intent intent = new Intent(this, typeof(BackGroundMusic));
                        if (BackGroundMusic.IsPlaying)
                        {
                            item.SetTitle("Start Music");
                            StopService(intent);
                        }
                        else
                        {
                            item.SetTitle("Stop Music");
                            StartService(intent);
                        }
                        break;
                    }
            }
            return base.OnContextItemSelected(item);
        }
       
    }
}