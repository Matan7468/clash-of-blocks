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
using static Android.Views.View;
using Android.Graphics;
using System.Threading.Tasks;
using System.Threading;
using Android.Util;

namespace clash_of_blocks
{
    [Activity(Label = "Game_Activity",ScreenOrientation =Android.Content.PM.ScreenOrientation.Locked)]
    public class Game_Activity : Activity
    {
        AllLevels AllLevels;
        LinearLayout game, turnsLayout;
        Button hintbtn;
        ImageButton restartbtn;
        ImageButton skinsbtn;
        Button LeaderBoard;
        int counter;
        public TextView tvturns { get; set; }
        Dialog EndingDialog;
        public Button NextLvl { get; set; }
        public Button RetryLvl { get; set; }
        ISharedPreferences sp;
        Skins skins;
        int skin;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Game_Layout);
            hintbtn = FindViewById<Button>(Resource.Id.hintbtn);
            tvturns = FindViewById<TextView>(Resource.Id.tvTurnsLeft);
            restartbtn = FindViewById<ImageButton>(Resource.Id.rstbtn);
            skinsbtn = FindViewById<ImageButton>(Resource.Id.skinsbtn);
            turnsLayout = FindViewById<LinearLayout>(Resource.Id.turns);
            game = FindViewById<LinearLayout>(Resource.Id.Game);
            LeaderBoard = FindViewById<Button>(Resource.Id.LeaderBoardbtn);

            sp = GetSharedPreferences("details", FileCreationMode.Private);
            skins = new Skins();
            try
            {
                skins.CurrentSkin = Intent.Extras.GetInt("currentSkin");
                User.SetSkinById(sp.GetInt("userId", -1), skins.CurrentSkin);
            }
            catch
            {
                skins.CurrentSkin = User.GetSkinById(sp.GetInt("userId", -1));
            }
            skin = skins.CurrentSkin;
            counter = User.GetLevelById(sp.GetInt("userId", -1));
            Point screenSize = new Point();
            this.WindowManager.DefaultDisplay.GetSize(screenSize);

            Cell.CellWidth = (screenSize.X - 100) / GameBoard.NUM_CELLS;
            Cell.CellHeight = game.LayoutParameters.Height / GameBoard.NUM_CELLS;

            hintbtn.Click += Hintbtn_Click;
            restartbtn.Click += Restartbtn_Click;
            skinsbtn.Click += Skinsbtn_Click;
            LeaderBoard.Click += LeaderBoard_Click;       

            AllLevels = new AllLevels(this,tvturns, skins);
            sp.Edit().PutInt("Levels", AllLevels.GetLevels().Count).Apply();
            tvturns.Text = AllLevels.GetLevels()[counter].GetTurns().ToString();

            View v = AllLevels.GetLevels()[counter];
            LinearLayout.LayoutParams parameters = new LinearLayout.LayoutParams(screenSize.X - 100, LinearLayout.LayoutParams.MatchParent);
            parameters.Gravity = GravityFlags.Center;
            v.LayoutParameters = parameters;
            game.AddView(v);
        }

        private void LeaderBoard_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AllRecords));
            StartActivity(intent);
        }

       
        public void ShowScoreDialog()
        {
            double green = AllLevels.GetLevels()[counter].GetPColor(Cell.Type.userGreen);
            double red = AllLevels.GetLevels()[counter].GetPColor(Cell.Type.botRed);
            double blue = AllLevels.GetLevels()[counter].GetPColor(Cell.Type.botBlue);
            Point screenSize = new Point();
            this.WindowManager.DefaultDisplay.GetSize(screenSize);
            int height = (int)(screenSize.Y * 0.3);
            int width = (int)(screenSize.X * 0.75);
            EndingDialog = new Dialog(this);
            LinearLayout.LayoutParams layoutParamsColor = new LinearLayout.LayoutParams(80, 80);
            LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams(0, ViewGroup.LayoutParams.MatchParent);
            layoutParams.Gravity = GravityFlags.CenterHorizontal;
            layoutParams.Weight = 0.3f;
            if (green > red && green > blue)
            {
                EndingDialog.SetContentView(Resource.Layout.WonDialog);
                LinearLayout lScore = EndingDialog.FindViewById<LinearLayout>(Resource.Id.LayoutScoreWinning);
                LinearLayout ScoreGreen = new LinearLayout(this);
                ScoreGreen.SetGravity(GravityFlags.Center);
                ScoreGreen.LayoutParameters = layoutParams;
                ScoreGreen.Orientation = Orientation.Vertical;
                View ivgreen = new View(this);
                ivgreen.SetBackgroundColor(skins.skins[skin].Player);
                ivgreen.LayoutParameters = layoutParamsColor;
                TextView tvGreenScore = new TextView(this);
                tvGreenScore.Text = green.ToString() + "%";
                tvGreenScore.Gravity = GravityFlags.CenterHorizontal;
                ScoreGreen.AddView(ivgreen);
                ScoreGreen.AddView(tvGreenScore);
                lScore.AddView(ScoreGreen);
                if (red > 0)
                {
                    LinearLayout ScoreRed = new LinearLayout(this);
                    ScoreRed.SetGravity(GravityFlags.Center);
                    ScoreRed.LayoutParameters = layoutParams;
                    ScoreRed.Orientation = Orientation.Vertical;
                    View ivRed = new View(this);
                    ivRed.SetBackgroundColor(skins.skins[skin].Bot2);
                    ivRed.LayoutParameters = layoutParamsColor;
                    TextView tvRedScore = new TextView(this);
                    tvRedScore.Text = red.ToString() + "%";
                    tvRedScore.Gravity = GravityFlags.CenterHorizontal;
                    ScoreRed.AddView(ivRed);
                    ScoreRed.AddView(tvRedScore);
                    lScore.AddView(ScoreRed);
                }
                if (blue > 0)
                {
                    LinearLayout ScoreBlue = new LinearLayout(this);
                    ScoreBlue.SetGravity(GravityFlags.Center);
                    ScoreBlue.LayoutParameters = layoutParams;
                    ScoreBlue.Orientation = Orientation.Vertical;
                    View ivBlue = new View(this);
                    ivBlue.SetBackgroundColor(skins.skins[skin].Bot1);
                    ivBlue.LayoutParameters = layoutParamsColor;
                    TextView tvBlueScore = new TextView(this);
                    tvBlueScore.Text = blue.ToString() + "%";
                    tvBlueScore.Gravity = GravityFlags.CenterHorizontal;
                    ScoreBlue.AddView(ivBlue);
                    ScoreBlue.AddView(tvBlueScore);
                    lScore.AddView(ScoreBlue);
                }

                EndingDialog.SetCancelable(false);
                NextLvl = EndingDialog.FindViewById<Button>(Resource.Id.NextLvl);
                NextLvl.Click += (senderNext, eNext) =>
                {
                    game.RemoveView(AllLevels.GetLevels()[counter]);
                    AllLevels.GetLevels()[counter].Restart();
                    Record.AddRecord(User.GetName(sp.GetInt("userId", -1)), green, sp.GetInt("CurrentLevel", 0));
                    counter++;
                    sp.Edit().PutInt("CurrentLevel", counter).Apply();
                    User.SetLevelById(sp.GetInt("userId", -1), counter);

                    try
                    {
                        tvturns.Text = AllLevels.GetLevels()[counter].GetTurns().ToString();
                        game.AddView(AllLevels.GetLevels()[counter]);
                        EndingDialog.Dismiss();
                    }
                    catch
                    {
                        counter = 0;
                        User.SetLevelById(sp.GetInt("userId", -1), 0);
                        Intent intent = new Intent(this, typeof(FinishedActivity));
                        StartActivity(intent);
                    }
                };
                EndingDialog.Show();
                EndingDialog.Window.SetLayout(width, height);
                EndingDialog.Window.SetGravity(GravityFlags.Center);
            }
            else
            {
                EndingDialog.SetContentView(Resource.Layout.LosingDialog);
                LinearLayout lScore = EndingDialog.FindViewById<LinearLayout>(Resource.Id.LayoutScoreLosing);
                LinearLayout ScoreGreen = new LinearLayout(this);
                ScoreGreen.SetGravity(GravityFlags.Center);
                ScoreGreen.LayoutParameters = layoutParams;
                ScoreGreen.Orientation = Orientation.Vertical;
                ImageView ivgreen = new ImageView(this);
                ivgreen.SetBackgroundColor(skins.skins[skin].Player);
                ivgreen.LayoutParameters = layoutParamsColor;
                TextView tvGreenScore = new TextView(this);
                tvGreenScore.Text = green.ToString() + "%";
                tvGreenScore.Gravity = GravityFlags.CenterHorizontal;
                ScoreGreen.AddView(ivgreen);
                ScoreGreen.AddView(tvGreenScore);
                lScore.AddView(ScoreGreen);
                if (red > 0)
                {
                    LinearLayout ScoreRed = new LinearLayout(this);
                    ScoreRed.SetGravity(GravityFlags.Center);
                    ScoreRed.LayoutParameters = layoutParams;
                    ScoreGreen.Orientation = Orientation.Vertical;
                    ImageView ivRed = new ImageView(this);
                    ivRed.SetBackgroundColor(skins.skins[skin].Bot2);
                    ivRed.LayoutParameters = layoutParamsColor;
                    TextView tvRedScore = new TextView(this);
                    tvRedScore.Text = red.ToString() + "%";
                    tvRedScore.Gravity = GravityFlags.CenterHorizontal;
                    ScoreRed.AddView(ivRed);
                    ScoreRed.AddView(tvRedScore);
                    lScore.AddView(ScoreRed);
                }
                if (blue > 0)
                {
                    LinearLayout ScoreBlue = new LinearLayout(this);
                    ScoreBlue.SetGravity(GravityFlags.Center);
                    ScoreBlue.LayoutParameters = layoutParams;
                    ScoreBlue.Orientation = Orientation.Vertical;
                    ImageView ivBlue = new ImageView(this);
                    ivBlue.SetBackgroundColor(skins.skins[skin].Bot1);
                    ivBlue.LayoutParameters = layoutParamsColor;
                    TextView tvBlueScore = new TextView(this);
                    tvBlueScore.Text = blue.ToString() + "%";
                    tvBlueScore.Gravity = GravityFlags.CenterHorizontal;
                    ScoreBlue.AddView(ivBlue);
                    ScoreBlue.AddView(tvBlueScore);
                    lScore.AddView(ScoreBlue);
                }
                RetryLvl = EndingDialog.FindViewById<Button>(Resource.Id.Restartbtn);
                RetryLvl.Click += (senderNext, eNext) =>
                {
                    AllLevels.GetLevels()[counter].Restart();
                    tvturns.Text = AllLevels.GetLevels()[counter].GetTurns().ToString();
                    EndingDialog.Dismiss();
                };
                EndingDialog.Show();
                EndingDialog.Window.SetLayout(width, height);
            }


        }
        private void Skinsbtn_Click(object sender, EventArgs e)
        {
            if (!AllLevels.GetLevels()[counter].playing)
            {
                Intent intent = new Intent(this, typeof(AllSkins));
                StartActivity(intent);
                AllLevels.GetLevels()[counter].Restart();
            }
        }

        private void Restartbtn_Click(object sender, EventArgs e)
        {
            if (!AllLevels.GetLevels()[counter].playing)
            {
                AllLevels.GetLevels()[counter].Restart();
                tvturns.Text = AllLevels.GetLevels()[counter].GetTurns().ToString();
            }
            
        }

        private void Hintbtn_Click(object sender, EventArgs e)
        {
            if (!AllLevels.GetLevels()[counter].playing)
            {
                AllLevels.GetLevels()[counter].UseHint();
            }
        }

    }
}