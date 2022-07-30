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
        public Form1()
        {
            InitializeComponent();
            mainGameScreen = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void init()
        {
            //Init game screen
            PictureBox gameScreen = new PictureBox();
            gameScreen.BackColor = Color.White;
            gameScreen.Dock = DockStyle.Fill;
            Controls.Add(gameScreen);
            mainGameScreen = new GameScreen(gameScreen);
            //Create player
            Rectangle playerShape = new Rectangle(35, 100, 80,80);
            dino = new Player(playerShape, 10, mainGameScreen);
            dino.ObjectImage = gameImageList.Images[0];
            mainGameScreen.AddGameObjects(dino);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            mainGameScreen.UpdateFrame(mainGameScreen.Screen.BackColor);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
            if (dino == null)
                return;
            if (e.KeyCode == Keys.Up)
            {
                if (dino.IsGrounded())
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
                dino.ObjectGravity.GravityForce,
                mainGameScreen);
            dino.ObjectImage = gameImageList.Images[7];
            mainGameScreen.AddGameObjects(dino);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            init();
            button1.Hide();
            button1.Enabled = false;
            timer.Enabled = true;
            ChangeToVietCongDino();
        }
    }
}