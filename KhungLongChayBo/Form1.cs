﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KhungLongChayBo.Properties;

namespace KhungLongChayBo
{
    public partial class Form1 : Form
    {
        private GameScreen mainGameScreen;
        private static DateTime previousTime;
        private static Random rand = new Random();
        private static Graphics frame;
        private Obstacle roadObstacle;
        private int highestScore = 100;
        private TextBox highScore;
        private bool isEndGame = false;
        private Panel picture;
        public Form1()
        {
            InitializeComponent();
            mainGameScreen = null;
            picture = new Panel();
            picture.Width = ClientRectangle.Width;
            picture.Height = ClientRectangle.Height;
            picture.Visible = false;
            picture.Enabled = false;
            this.Controls.Add(picture);
            frame = picture.CreateGraphics();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void init()
        {
            isEndGame = false;
            picture.Enabled = true;
            picture.Visible = true;
            //Init game screen
            if (mainGameScreen != null) //If the main game screen is already init just clear all              
            {
                ClearAllGameScreen();
            }
            else
            {
                Bitmap gameScreen = new Bitmap(ClientSize.Width, ClientSize.Height);
                Image temp = Image.FromFile(Application.StartupPath +
                    @"\Dino Run\Maps\Background.png");
                Bitmap backGround = new Bitmap(temp, ClientSize.Width, ClientSize.Height);
                mainGameScreen = new GameScreen(gameScreen, backGround);
            }

            //Create road map
            int roadWidth = ClientSize.Width;
            int roadHeight = 30;
            Ground road = new Ground(0,ClientSize.Height-roadHeight,roadWidth,roadHeight,0,mainGameScreen);
            road.ObjectImage = Image.FromFile(Application.StartupPath +
                @"\Dino Run\Maps\ground.png");
            mainGameScreen.AddGameObjects(road);

            //Create player
            GreenDino dino;
            int playerHeight = 80;
            int playerWidth = 80;
            Rectangle playerShape = new Rectangle(35, ClientSize.Height - roadHeight - playerHeight, 
                playerWidth, playerHeight);
            dino = new GreenDino(playerShape, 5, mainGameScreen);
            //dino.Hittable = false;
            mainGameScreen.AddGameObjects(dino);

            //Create a obstacle
            roadObstacle = new Obstacle(mainGameScreen.Screen.Width - 50, dino.ObjectShape.Y,
                    80, 100, 0, mainGameScreen);
            roadObstacle.ObjectImage = Image.FromFile(Application.StartupPath +
                @"\Dino Run\Maps\Obstacles\Obstacle Tree.png");

            //Add score object to game
            int scoreWidth = 120;
            int scoreHeight = 50;
            int paddingRight = 0;
            TextBox score = new Score(mainGameScreen.Screen.Width - scoreWidth - paddingRight, 
                0 , scoreWidth, scoreHeight, 0, mainGameScreen);
            score.StringFormat.Alignment = StringAlignment.Near;
            score.StringFormat.LineAlignment = StringAlignment.Center;
            score.StringFormat.FormatFlags = StringFormatFlags.NoWrap;
            try
            {
                score.Font = new Font(new FontFamily("Sans-serif"), 16F);
            }
            catch(ArgumentException)
            {
                score.Font = new Font(new FontFamily("Arial"), 16F);
            }
            mainGameScreen.AddGameObjects(score);

            //Add highest score to game
            highScore = new Score(score.ObjectShape.X - scoreWidth,
                0, scoreWidth, scoreHeight,0, mainGameScreen);
            highScore.StringFormat.Alignment = StringAlignment.Near;
            highScore.StringFormat.LineAlignment = StringAlignment.Center;
            highScore.StringFormat.FormatFlags = StringFormatFlags.NoWrap;
            Score.HighestPoint = highestScore;
            highScore.Text = String.Format("{0}", Score.HighestPoint);
            mainGameScreen.AddGameObjects(highScore);

            //Init the timer
            timer.Enabled = true;
            previousTime = DateTime.Now;

        }
        private void EndGame()
        {
            isEndGame = true;
            //Console.WriteLine(mainGameScreen.ListOfGameObjects.Count);
            PauseGame();
            GC.GetTotalMemory(true);
            int score = Convert.ToInt32(highScore.Text);
            if (score > highestScore)
            {
                highestScore = score;
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            frame.DrawImage(mainGameScreen.Screen, new Point(0, 0));
            isEndGame = mainGameScreen.UpdateFrame();
            if (isEndGame)
            {
                //Draw a final picture
                frame.DrawImage(mainGameScreen.Screen, new Point(0, 0));
                EndGame();
                ShowPauseMenu();
            }
            DateTime now = DateTime.Now;
            int time = rand.Next(3, 10);
            if (Convert.ToInt32((now - previousTime).TotalSeconds) == time)
            {
                previousTime = now;
                Obstacle ob = new Obstacle(roadObstacle.ObjectShape.X,
                    roadObstacle.ObjectShape.Y,
                    roadObstacle.ObjectShape.Width,
                    roadObstacle.ObjectShape.Height, 0,
                    mainGameScreen);
                ob.ObjectImage = roadObstacle.ObjectImage;
                ob.Speed = 25;
                mainGameScreen.AddGameObjects(ob);
                //Item i = new SoldierItem(ClientSize.Width - 50, 0, 50, 50, 5, mainGameScreen);
                Item i = new ArmorItem(ClientSize.Width - 50, 0, 50, 50, 5, mainGameScreen);
                mainGameScreen.AddGameObjects(i);
            }
        }
        private GreenDino SearchPlayer()
        {
            if (mainGameScreen == null)
                return null;
            foreach (GameObjects ob in mainGameScreen.ListOfGameObjects)
            {
                if (ob.GetType() == typeof(GreenDino) ||
                    ob.GetType().BaseType == typeof(GreenDino))
                {
                    return (GreenDino)ob;
                }
            }
            return null;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (mainGameScreen == null)
                return;
            e.Handled = true;
            e.SuppressKeyPress = true;
            GreenDino p = SearchPlayer();
            if (p == null)
                return;
            switch (e.KeyCode)
            {
                case Keys.W:
                    if (p.OnGround() != null)
                    {
                        p.Jumping();
                    }
                    break;
                case Keys.Space:
                    if (!p.IsDestroy)
                    {
                        p.Action();
                    }
                    break;
                case Keys.S:
                    if (!p.IsDestroy)
                    {
                        p.Crouching();
                    }
                    break;
                case Keys.Escape:
                    if(!isEndGame)
                    {
                        PauseGame();
                        ShowPauseMenu();
                    }
                    break;
            }
        }

        private void PauseGame()
        {
            if (timer.Enabled == true)
                timer.Stop();
            else
                timer.Start();
        }

        private void ShowPauseMenu()
        {
            if (panelPause.Enabled)
                HidePanel();
            else
            {
                panelPause.Visible = true;
                panelPause.Enabled = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
            GreenDino p = SearchPlayer();
            if (p == null)
                return;
            switch (e.KeyCode)
            {
                case Keys.S:
                    if (!p.IsDestroy)
                    {
                        p.StopCrouching();
                    }
                    break;
            }
        }

        private void HidePanel()
        {
            panelPause.Visible = false;
            panelPause.Enabled = false;
        }

        private void playAgainButton_Click(object sender, EventArgs e)
        {
            if(!isEndGame)
                EndGame();
            HidePanel();
            ClearAllGameScreen();
            init();
        }

        public void ClearAllGameScreen()
        {
            mainGameScreen.ClearAll();
        }

        private void buttonMainMenu_Click(object sender, EventArgs e)
        {
            ClearAllGameScreen();
            PauseGame();
            EndGame();
            HidePanel();
            ShowMainMenu();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            HideMainMenu();
            init();
        }

        private void HideMainMenu()
        {
            buttonPlay.Visible = false;
            buttonPlay.Enabled = false;
            textBoxGuide.Visible = false;
        }

        private void ShowMainMenu()
        {
            buttonPlay.Visible = true;
            buttonPlay.Enabled = true;
            textBoxGuide.Visible = true;
            picture.Enabled = false;
            picture.Visible = false;
        }
    }
}