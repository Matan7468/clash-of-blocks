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
    class LevelAdapter : BaseAdapter<Level>
    {
        LevelView activity;
        List<Level> levels;
        int[] Locked =
        {
           Resource.Drawable.Level1,
           Resource.Drawable.Level2,
           Resource.Drawable.Level3,
           Resource.Drawable.Level4
        };
        int[] unLocked =
        {

        };
        
        public LevelAdapter(LevelView activity, List<Level> levels)
        {
            this.activity = activity;
            this.levels = levels;
        }

        public override Level this[int position]
        {
            get
            {
                return levels[position];
            }
        }


        public override int Count
        {
            get
            {
                return levels.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = activity.LayoutInflater.Inflate(Resource.Layout.LevelCube, parent, false);
            }

            TextView Id = convertView.FindViewById<TextView>(Resource.Id.LevelId);
            ImageButton Level = convertView.FindViewById<ImageButton>(Resource.Id.Level1);

            Id.Text = "Level "+(position+1).ToString();
            Level.SetImageResource(Locked[position]);
            Level.Click += (senderSave, eSave) =>
            {
                activity.ChangeLevel(position);
            };
            return convertView;
        }
        
    }
}