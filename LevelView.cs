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
    [Activity(Label = "LevelView")]
    public class LevelView : Activity
    {
        GridView levels;
        AllLevels Levels;
        ISharedPreferences sp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AllLevels);
            // Create your application here

            sp = GetSharedPreferences("details", FileCreationMode.Private);
            levels = FindViewById<GridView>(Resource.Id.LevelsgridView);
            Levels = new AllLevels( this, new TextView(this),new Skins(sp.GetInt("userId", -1)),sp.GetInt("userId", -1));
            LevelAdapter LevelAdapter = new LevelAdapter(this, Levels.level);
            levels.Adapter = LevelAdapter;
            levels.DeferNotifyDataSetChanged();
        }

       
        public void ChangeLevel(int x)
        {
            if (Level.GetDoneById(x,sp.GetInt("userId",-1)))
            {
                Intent intent = new Intent(this, typeof(Game_Activity));
                intent.PutExtra("CurrentLevel", x);
                StartActivity(intent);
            }
            else
            {
                Toast.MakeText(this, "this level is still unlocked", ToastLength.Short).Show();
            }
        }
    }
}