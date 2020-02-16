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
    class RecordAdapter:BaseAdapter<Record>
    {
        List<Record> records;
        Activity activity;

        public RecordAdapter(List<Record> records, Activity activity)
        {
            this.records = records;
            this.activity = activity;
        }

        public override Record this[int position]
        {
            get
            {
                return records[position];
            }
        }


        public override int Count
        {
            get
            {
                return records.Count;
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
                convertView = activity.LayoutInflater.Inflate(Resource.Layout.Record_Row,parent,false);
            }
            TextView UserName = convertView.FindViewById<TextView>(Resource.Id.tvUserName);
            TextView Score = convertView.FindViewById<TextView>(Resource.Id.tvScore);
            TextView Date = convertView.FindViewById<TextView>(Resource.Id.tvDate);

            UserName.Text = records[position].Name;
            Score.Text = records[position].Score.ToString()+"%";
            Date.Text = records[position].Date.ToString("dd/MM/yyyy");

            return convertView;
        }
    }
}