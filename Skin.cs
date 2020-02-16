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

namespace clash_of_blocks
{
    class Skin
    {
        public Color Player { get; set; }
        public Color Bot1 { get; set; }
        public Color Bot2 { get; set; }

        public Bitmap Icon { get; set; }

        public int Price { get; set; }

        public Skin(Color player, Color bot1, Color bot2, Bitmap icon, int price)
        {
            Player = player;
            Bot1 = bot1;
            Bot2 = bot2;
            Icon = icon;
            Price = price;
        }
    }
}