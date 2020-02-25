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
using SQLite;

namespace clash_of_blocks
{
    [Table("tblLevel")]
    class Level
    {
        [PrimaryKey,AutoIncrement]
        public int LevelId { get; set;}
        public int userId { get; set; }
        public int Icon { get; set; }
        public bool Done { get; set; }

        public Level()
        {
        }

        public Level( int userid,int icon, bool done)
        {
            Icon = icon;
            Done = done;
            userId = userId;
        }

        public static bool AddLevel(int userid,int id,int icon, bool done)
        {
            if (!Exists(id,userid))
            {
                Level newLevel = new Level(userid,icon, done);
                SqlHelper.GetConnection().Insert(newLevel);
                return true;
            }
            return false;
        }

        public static bool Exists(int id,int userId)
        {
            string Query = string.Format("SELECT tblLevel.* FROM tblLevel WHERE ((tblLevel.LevelId) = \"{0}\") AND ((tblLevel.userId) = \"{1}\");", id,userId);
            List<Level> levels = SqlHelper.GetConnection().Query<Level>(Query);
            if (levels.Count == 0)
            {
                return false;
            }
            return true;
        }

        public static bool GetDoneById(int id, int userId)
        {
            string Query = string.Format("SELECT tblLevel.* FROM tblLevel WHERE ((tblLevel.LevelId)=\"{0}\") AND ((tblLevel.userId) = \"{1}\");", id, userId);
            List<Level> levels = SqlHelper.GetConnection().Query<Level>(Query);
            if (levels.Count == 1)
            {
                return levels[0].Done;
            }
            return false;
        }

        public static void SetDoneById(int id, bool done, int userId)
        {
            string Query = string.Format("SELECT tblLevel.* FROM tblLevel WHERE ((tblLevel.LevelId)=\"{0}\") AND ((tblLevel.userId) = \"{1}\");", id, userId);
            List<Level> levels = SqlHelper.GetConnection().Query<Level>(Query);
            if (levels.Count == 1)
            {
                levels[0].Done = done;
                SqlHelper.GetConnection().Update(levels[0]);
            }
        }

        public static void SetIconById(int id, int icon, int userId)
        {
            string Query = string.Format("SELECT tblLevel.* FROM tblLevel WHERE ((tblLevel.LevelId)=\"{0}\") AND ((tblLevel.userId) = \"{1}\");", id, userId);
            List<Level> levels = SqlHelper.GetConnection().Query<Level>(Query);
            if (levels.Count == 1)
            {
                levels[0].Icon = icon;
                SqlHelper.GetConnection().Update(levels[0]);
            }
        }
        
    }
}