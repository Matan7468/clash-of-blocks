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
    [Activity(Label = "AllSkins")]
    public class AllSkins : Activity
    {
        GridView skins;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AllSkins);
            // Create your application here

            skins = FindViewById<GridView>(Resource.Id.gridView1);
            Skins s = new Skins();
            SkinAdapter skinAdapter = new SkinAdapter(this, s.skins);
            skins.Adapter = skinAdapter;
            skins.DeferNotifyDataSetChanged();
        }
        public void ChangeSkin(int x)
        {
            Intent intent = new Intent(this, typeof(Game_Activity));
            intent.PutExtra("currentSkin", x);
            StartActivity(intent);
        }
    }
}