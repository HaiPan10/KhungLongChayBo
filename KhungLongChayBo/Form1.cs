using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KhungLongChayBo.Properties;
using System.IO;

namespace KhungLongChayBo
{
    public partial class Form1 : Form
    {
        private GameScreen mainGameScreen;
        private DateTime spawnObstacleTime;
        private DateTime speedUpTime;
        private DateTime spawnItemTime;
        private Random rand = new Random();
        private Graphics frame;
        private Obstacle roadObstacle;
        private int highestScore = 0;
        private TextBox highScore;
        private bool isEndGame = false;
        private Panel picture;
        private GreenDino dino;
        private int baseSpeed = 12;
        private int maxSpeed = 25;
        private Ground road;
        private int roadSpeedBonus = 3;
        private int flySpeedBonus = 0;

        //
        private int obstacleTime;
        private int itemTime;
        private int obstacleMaxTime = 5;
        private int obstacleMinTime = 2;
        private int itemMaxTime = 30;
        private int itemMinTime = 10;

        public Form1()
        {
            InitializeComponent();
            mainGameScreen = null;
            picture = new Panel();
            picture.Width = ClientRectangle.Width;
            picture.Height = ClientRectangle.Height;
            picture.Visible = false;
            picture.Enabled = false;
            picture.Paint += Picture_Paint;
            this.Controls.Add(picture);
            frame = picture.CreateGraphics();
        }

        private void Picture_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(mainGameScreen.Screen, new Point(0, 0));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void init()
        {
            ////init highest score
            //try
            //{
            //    highestScore = Convert.ToInt32(File.ReadAllText(Application.StartupPath +
            //        @"\highestScore.txt"));
            //}
            //catch
            //{
            //    File.GetAccessControl((Application.StartupPath +
            //            @"\highestScore.txt"));
            //    highestScore = 0;
            //    using (File.Create(Application.StartupPath +
            //            @"\highestScore.txt"))
            //    {

            //    }
            //}

            //init speed
            baseSpeed = 12;

            //Clear Armor
            ArmorItem.NumberOfDinoArmor = 0;
            ArmorItem.Armors.Clear();

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
            road = new Ground(0,ClientSize.Height-roadHeight,roadWidth,roadHeight,0,mainGameScreen);
            road.ObjectImage = Image.FromFile(Application.StartupPath +
                @"\Dino Run\Maps\ground.png");
            mainGameScreen.AddGameObjects(road);
            road.SpeedMove = baseSpeed + roadSpeedBonus;

            //Create player
            int playerHeight = 80;
            int playerWidth = 80;
            Rectangle playerShape = new Rectangle(35, ClientSize.Height - roadHeight - playerHeight, 
                playerWidth, playerHeight);
            dino = new GreenDino(playerShape, 4, mainGameScreen);
            //dino.Hittable = false;
            mainGameScreen.AddGameObjects(dino);

            //Create a tree obstacle for later use
            int treeWidth = 80;
            int treeHeight = 100;
            roadObstacle = new Obstacle(mainGameScreen.Screen.Width - 50, dino.ObjectShape.Y,
                    treeWidth, treeHeight, 0, mainGameScreen);

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
            spawnObstacleTime = DateTime.Now;
            speedUpTime = DateTime.Now;
            spawnItemTime = DateTime.Now;

            //Init time to spawn obstacle
            obstacleTime = rand.Next(obstacleMinTime, obstacleMaxTime);
            itemTime = rand.Next(itemMinTime, itemMaxTime);

            //Create first item for dino
            mainGameScreen.AddGameObjects(CreateItem(baseSpeed));
        }
        private void EndGame()
        {
            isEndGame = true;
            //Console.WriteLine(mainGameScreen.ListOfGameObjects.Count);
            PauseGame();
            GC.GetTotalMemory(true); //Make the gabarage collector collect weak reference
            int score = Convert.ToInt32(highScore.Text);
            if (score > highestScore)
            {
                highestScore = score;
                //try
                //{
                //    File.GetAccessControl(Application.StartupPath +
                //        @"\highestScore.txt");
                //    using (StreamWriter writer = new StreamWriter(Application.StartupPath +
                //        @"\highestScore.txt"))
                //    {
                //        writer.Write(highestScore);
                //    }
                //}
                //catch(NullReferenceException)
                //{
                    
                //}
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            frame.DrawImage(mainGameScreen.Screen, new Point(0, 0));
            isEndGame = mainGameScreen.UpdateFrame();
            if (isEndGame)
            {
                //Draw a final picture
                picture.Invalidate();
                EndGame();
                ShowPauseMenu();
            }
            DateTime now = DateTime.Now;
            if ((Convert.ToInt32((now - spawnObstacleTime).TotalSeconds) + 1) % obstacleTime == 0)
            {
                spawnObstacleTime = now;
                mainGameScreen.AddGameObjects(CreateObstalce(baseSpeed));
                obstacleTime = rand.Next(obstacleMinTime, obstacleMaxTime);
                //Console.WriteLine(baseSpeed);
            }
            if((Convert.ToInt32((now - spawnItemTime).TotalSeconds) + 1) % itemTime == 0)
            {
                spawnItemTime = now;
                mainGameScreen.AddGameObjects(CreateItem(baseSpeed));
                itemTime = rand.Next(itemMinTime, itemMaxTime);
            }
            //Speed up the game
            if((Convert.ToInt32((now - speedUpTime).TotalSeconds + 1) % 5 == 0))
            {
                speedUpTime = now;
                //Console.WriteLine(road.SpeedMove);
                if(baseSpeed < maxSpeed)
                {
                    baseSpeed++;
                    road.SpeedMove = baseSpeed + roadSpeedBonus;
                }
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
            if (timer.Enabled)
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
            try
            {
                init();
            }
            catch(NullReferenceException)
            {
                Close();
            }
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
            try
            {
                init();
            }
            catch(NullReferenceException)
            {
                Close();
            }
        }

        private void HideMainMenu()
        {
            buttonPlay.Visible = false;
            buttonPlay.Enabled = false;
            textBoxGuide.Visible = false;
            textBoxAuthor.Visible = false;
        }

        private void ShowMainMenu()
        {
            buttonPlay.Visible = true;
            buttonPlay.Enabled = true;
            textBoxGuide.Visible = true;
            picture.Enabled = false;
            picture.Visible = false;
            textBoxAuthor.Visible = true;
        }
        private Obstacle CreateObstalce(int speed)
        {
            int flyHeight = 120;
            int flyWidth = 60;
            Obstacle obstacle = null;
            int i = rand.Next(1, 3);
            switch(i)
            {
                case 1: //tree
                    obstacle = new Obstacle(roadObstacle.ObjectShape, 0, mainGameScreen);
                    int j = rand.Next(1, 3);
                    switch(j)
                    {
                        case 1:
                            obstacle.ObjectImage = Obstacle.Tree1;
                            break;
                        case 2:
                            obstacle.ObjectImage = Obstacle.Tree2;
                            break;
                    }
                    obstacle.Speed = speed;
                    break;
                case 2: //fly
                    int x = ClientRectangle.Width - flyWidth / 2;
                    int y = rand.Next(10, ClientRectangle.Height/2);
                    Rectangle r = new Rectangle(x, y, flyWidth, flyHeight);
                    obstacle = new Obstacle(r, 0, mainGameScreen);
                    obstacle.ObjectImage = Obstacle.FlyObstacle;
                    obstacle.Speed = speed + flySpeedBonus;
                    break;
            }
            return obstacle;
        }

        private Item CreateItem(int speed)
        {
            int gravityForce = 10;
            Item item = null;
            int itemWidth = 50;
            int itemHeight = 50;
            int x = ClientRectangle.Width - itemWidth / 2;
            int y = 0;
            int i = rand.Next(1, 3);
            Rectangle r = new Rectangle(x, y, itemWidth, itemHeight);
            switch(i)
            {
                case 1:
                    item = new ArmorItem(r, gravityForce, mainGameScreen);
                    item.Speed = speed;
                    break;
                case 2:
                    item = new SoldierItem(r, gravityForce, mainGameScreen);
                    item.Speed = speed;
                    break;
            }
            return item;
        }
    }
}