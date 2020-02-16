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
using SQLite;

namespace clash_of_blocks
{
    [Table("tblRecord")]
    class Record
    {
        [AutoIncrement,PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Score { set; get; }
        public int Level { get; set; }

        public Record(string name, double score,int level)
        {
            Name = name;
            Date = DateTime.Now;
            Score = score;
            Level = level;
        }

        public Record()
        {
        }

        public static void AddRecord(string name, double score,int level)
        {
            Record r = new Record(name, score,level);
            SqlHelper.GetConnection().Insert(r);
        }

        public static List<Record> GetAllRecords(int level)
        {
            string Query = string.Format("SELECT tblRecord.* FROM tblRecord WHERE (((tblRecord.Level)=\"{0}\")) ORDER BY tblRecord.Score DESC;",level);
            List<Record> Allrecords=SqlHelper.GetConnection().Query<Record>(Query);
            return Allrecords;
        }

        public static Record GetRecord(string name)
        {
            string Query = string.Format("SELECT tblRecord.* FROM tblRecord WHERE (((tblRecord.Name)=\"{0}\"));", name);
            List<Record> Records = SqlHelper.GetConnection().Query<Record>(Query);
            if (Records.Count == 1)
            {
                return Records[0];
            }
            return null;
        }
    }
}