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

namespace KhungLongChayBo
{
    public partial class Form1 : Form
    {
        private GameScreen mainGameScreen;
        private Player dino;
        private static DateTime previousTime;
        private static Random rand = new Random();
        private static Graphics frame;
        public Form1()
        {
            InitializeComponent();
            mainGameScreen = null;
            frame = this.CreateGraphics();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void init()
        {
            //Init game screen
            Bitmap gameScreen = new Bitmap(ClientSize.Width, ClientSize.Height);
            Image temp = Image.FromFile(Application.StartupPath +
                @"\Dino Run\Maps\Background.png");
            Bitmap backGround = new Bitmap(temp, ClientSize.Width, ClientSize.Height);
            mainGameScreen = new GameScreen(gameScreen, backGround);
            //Create player
            Rectangle playerShape = new Rectangle(35, 100, 80,80);
            dino = new Player(playerShape, 5, mainGameScreen);
            dino.ObjectImage = gameImageList.Images[0];
            mainGameScreen.AddGameObjects(dino);
            Ground road = new Ground(10,ClientSize.Height-50,200,10,0,mainGameScreen);
            mainGameScreen.AddGameObjects(road);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            frame.DrawImage(mainGameScreen.Screen, new Point(0, 0));
            mainGameScreen.UpdateFrame();
            DateTime now = DateTime.Now;
            int time = rand.Next(3, 10);
            if (Convert.ToInt32((now - previousTime).TotalSeconds) == time)
            {
                previousTime = now;
                Obstacle ob = new Obstacle(mainGameScreen.Screen.Width - 50, dino.ObjectShape.Y + 20,
                    50, 80, 0, mainGameScreen);
                ob.Speed = 15;
                ob.ObjectImage = gameImageList.Images[9];
                mainGameScreen.AddGameObjects(ob);
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
            if (dino == null)
                return;
            if (e.KeyCode == Keys.W)
            {
                if (dino.OnGround() != null)
                {
                    dino.Jumping();
                }
            }
            else if(e.KeyCode == Keys.Space)
            {
                dino.Action();
            }
        }
        private void ChangeToVietCongDino()
        {
            mainGameScreen.RemoveGameObjects(dino);
            dino = new VietCongDino(dino.ObjectShape,
                dino.ObjectGravity.Force,
                mainGameScreen);
            dino.ObjectImage = Image.FromFile(Application.StartupPath +
                @"\Dino Run\Dinos\Talking tree Dino\Vietcong Dino (Idle).png");
            mainGameScreen.AddGameObjects(dino);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            init();
            button1.Hide();
            button1.Enabled = false;
            timer.Enabled = true;
            previousTime = DateTime.Now;
            ChangeToVietCongDino();
        }
    }
}