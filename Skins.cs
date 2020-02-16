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
using Android.Graphics;

namespace clash_of_blocks
{
    class Skins
    {
        public List<Skin> skins { get; set; }

        public int CurrentSkin { get; set; }

        public Skins()
        {
            skins = new List<Skin>();

            BuildSkin1();
            BuildSkin2();
            BuildSkin3();
        } 
        private void BuildSkin1()
        {
            Color Player=Color.Green;
            Color Bot1=Color.Blue;
            Color Bot2=Color.Red;
            Bitmap bitmap = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.skin1);
            skins.Add(new Skin(Player, Bot1, Bot2, bitmap, 0));
        }
        private void BuildSkin2()
        {
            Color Player = Color.Rgb(0, 255, 255);
            Color Bot1 = Color.Rgb(255, 153, 51);
            Color Bot2 = Color.Rgb(255, 102, 102);
            Bitmap bitmap = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.skin2);
            skins.Add(new Skin(Player, Bot1, Bot2, bitmap, 50));
        }
        private void BuildSkin3()
        {
            Color Player = Color.Rgb(228, 0, 255);
            Color Bot1 = Color.Rgb(0, 209, 103);
            Color Bot2 = Color.Rgb(149, 0, 56);
            Bitmap bitmap = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.skin3);
            skins.Add(new Skin(Player, Bot1, Bot2, bitmap, 100));
        }


    }
}