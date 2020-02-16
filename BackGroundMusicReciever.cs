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
using Android.Telephony;


namespace clash_of_blocks
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] {"my.music"}, Priority = (int)IntentFilterPriority.HighPriority)]
    class BackGroundMusicReciever : BroadcastReceiver
    {
        public BackGroundMusicReciever()
        {

        }

        public override void OnReceive(Context context, Intent intent)
        {
            bool started = intent.GetBooleanExtra("musicStarted", false);

            if (started)
                Toast.MakeText(context, "music started", ToastLength.Short).Show();
           else
                Toast.MakeText(context, "music stopped", ToastLength.Short).Show();
        }
    }
}