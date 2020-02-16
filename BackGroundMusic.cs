using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace clash_of_blocks
{
    [Service]
    class BackGroundMusic : Service
    {
        MediaPlayer BackGround;
        public static bool IsPlaying;

        public override void OnCreate()
        {
            this.BackGround = MediaPlayer.Create(this, Resource.Raw.TheNerdSong);
            BackGround.Looping = true;
            base.OnCreate();
        }
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            this.BackGround.Start();
            IsPlaying = true;
            Intent intentBroadcast = new Intent("my.music");  //  יצירת מסר לרסיבר
            intentBroadcast.PutExtra("musicStarted", true);
            SendBroadcast(intentBroadcast); // שליחת המסר לרסיבר

            return base.OnStartCommand(intent, flags, startId);
        }
        public override void OnDestroy()
        {
            this.BackGround.Pause();
            IsPlaying = false;
            Intent intentBroadcast = new Intent("my.music"); //  יצירת מסר לרסיבר
            intentBroadcast.PutExtra("musicStarted", false);
            SendBroadcast(intentBroadcast); // שליחת המסר לרסיבר

            base.OnDestroy();

        }
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
    }
}