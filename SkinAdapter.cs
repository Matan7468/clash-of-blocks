using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace clash_of_blocks
{
    class SkinAdapter : BaseAdapter<Skin>
    {
        AllSkins activity;
        List<Skin> skins;
        int[] images =
        {
            Resource.Drawable.skin1,
            Resource.Drawable.skin2,
            Resource.Drawable.skin3
        };

        public SkinAdapter(AllSkins activity, List<Skin> skins)
        {
            this.activity = activity;
            this.skins = skins;
        }

        public override Skin this[int position]
        {
            get
            {
                return skins[position];
            }
        }


        public override int Count
        {
            get
            {
                return skins.Count;
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
                convertView = activity.LayoutInflater.Inflate(Resource.Layout.SkinRow, parent,false);
            }
            
            TextView Price = convertView.FindViewById<TextView>(Resource.Id.Price);
            ImageButton skin = convertView.FindViewById<ImageButton>(Resource.Id.skin1);

            Price.Text = skins[position].Price.ToString();
            skin.SetImageResource(images[position]);
            skin.Click+=(senderSave, eSave)=>
            {
                activity.ChangeSkin(position);
            };
            return convertView;
        }
    }
}


          
       
          