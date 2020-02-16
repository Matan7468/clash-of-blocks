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
using SQLite;

namespace clash_of_blocks
{
    [Table("tblUsers")]
    class User
    {
        
        [PrimaryKey, AutoIncrement]
        public int UserId { set; get; }
        public string FullName { set; get; }
        public string PhoneNumber { set; get; }
        public string Password { set; get; }
        public string UserName { set; get; }
        public int RecentLevel { get; set; }
        public int Skin { get; set; }

        public User()
        {

        }

        public User(string fullName, string phoneNumber, string password, string username,int recentlevel)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Password = password;
            UserName = username;
            RecentLevel = recentlevel;
            this.Skin = 0;
        }

        public static bool AddUser(string fullName, string phoneNumber, string password, string username)
        {
            if (!Exists(username))
            {
                User newUser = new User(fullName, phoneNumber, password, username,0);
                SqlHelper.GetConnection().Insert(newUser);
                return true;
            }
            return false;
        }

        public static bool Exists(string username)
        {
            string Query = string.Format("SELECT tblUsers.* FROM tblUsers WHERE(((tblUsers.UserName) = \"{0}\"));", username);
            List<User> users=SqlHelper.GetConnection().Query<User>(Query);
            if (users.Count == 0)
            {
                return false;
            }
            return true;
        }

        public static int GetId(string userName)
        {
            string Query = string.Format("SELECT tblUsers.* FROM tblUsers WHERE(((tblUsers.UserName) = \"{0}\"));", userName);
            List<User> users = SqlHelper.GetConnection().Query<User>(Query);
            if (users.Count == 0)
            {
                return -1;
            }
            return users[0].UserId;
        }

        public static string GetName(int UserId)
        {
            string Query = string.Format("SELECT tblUsers.UserName FROM tblUsers WHERE (((tblUsers.UserId)=\"{0}\"));", UserId);
            List<User> users = SqlHelper.GetConnection().Query<User>(Query);
            if (users.Count == 1)
            {
                return users[0].UserName;
            }
            return null;
        }
        public static bool IsUserNameAndPassWordMatch(string Username,string PassWord)
        {
            string Query = string.Format("SELECT tblUsers.* FROM tblUsers WHERE (((tblUsers.Password)=\"{0}\") AND ((tblUsers.UserName)=\"{1}\"));", PassWord, Username);
            List<User> users = SqlHelper.GetConnection().Query<User>(Query);
            if (users.Count == 0)//didnt find a match
            {
                return false;
            }
            return true;
        }
        public static int GetLevelById(int userid)
        {
            string Query = string.Format("SELECT tblUsers.* FROM tblUsers WHERE (((tblUsers.UserId)=\"{0}\"));", userid);
            List<User> users = SqlHelper.GetConnection().Query<User>(Query);
            if (users.Count == 1)
            {
                return users[0].RecentLevel;
            }
            return -1;
        }
        public static void SetLevelById(int userid,int Level)
        {
            string Query = string.Format("SELECT tblUsers.* FROM tblUsers WHERE (((tblUsers.UserId)=\"{0}\"));", userid);
            List<User> users = SqlHelper.GetConnection().Query<User>(Query);
            if (users.Count == 1)
            {
                users[0].RecentLevel = Level;
                SqlHelper.GetConnection().Update(users[0]);
            }
        }

        public static void SetSkinById(int userid, int skin)
        {
            string Query = string.Format("SELECT tblUsers.* FROM tblUsers WHERE (((tblUsers.UserId)=\"{0}\"));", userid);
            List<User> users = SqlHelper.GetConnection().Query<User>(Query);
            if (users.Count == 1)
            {
                users[0].Skin = skin;
                SqlHelper.GetConnection().Update(users[0]);
            }
        }
        public static int GetSkinById(int userid)
        {
            string Query = string.Format("SELECT tblUsers.* FROM tblUsers WHERE (((tblUsers.UserId)=\"{0}\"));", userid);
            List<User> users = SqlHelper.GetConnection().Query<User>(Query);
            if (users.Count == 1)
            {
                return users[0].Skin;
            }
            return -1;
        }
    }
}
        


