using System;
using System.Collections;
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
    [Activity(Label = "RecordsForLvl")]
    public class RecordsForLvl : Activity
    {
        Spinner spl;
        ArrayAdapter ad;
        ArrayList Levels;
        ListView lv;
        ISharedPreferences sp;
        AllLevels AllLevels;
        Button backButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.ChooseLvlRecords);
            // Create your application here
            spl = FindViewById<Spinner>(Resource.Id.ChooseLvlSpinner);
            lv = FindViewById<ListView>(Resource.Id.LevelLV);
            backButton = FindViewById<Button>(Resource.Id.backButton);

            TextView tv = new TextView(this);
            AllLevels = new AllLevels(this,tv,new Skins());
            this.GetLevels();

            ad = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, Levels);
            spl.Adapter = ad;

            spl.ItemSelected += Spl_ItemSelected;
            backButton.Click += BackButton_Click;

            sp = GetSharedPreferences("details", FileCreationMode.Private);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Home_Activity));
            this.Finish();
            StartActivity(intent);
        }

        public void GetAdapterByLvl(int level)
        {
            List<Record> records = Record.GetAllRecords(level);
            RecordAdapter MyAdapter = new RecordAdapter(records, this);
            lv.Adapter = MyAdapter;
            lv.DeferNotifyDataSetChanged();
        }
        private void Spl_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            this.GetAdapterByLvl(e.Position);
        }

        private void GetLevels()
        {
            Levels = new ArrayList();
            int lvl = AllLevels.GetLevels().Count;
            for (int i = 1; i <= lvl; i++)
            {
                Levels.Add("Level " + i);
            }
        }
       
    }
}