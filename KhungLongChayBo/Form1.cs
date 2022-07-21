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
        public Form1()
        {
            InitializeComponent();
            mainGameScreen = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            init();
        }

        private void init()
        {
            PictureBox gameScreen = new PictureBox();
            gameScreen.BackColor = Color.White;
            gameScreen.Dock = DockStyle.Fill;
            Controls.Add(gameScreen);
            mainGameScreen = new GameScreen(gameScreen);
            //Create player
            Rectangle playerShape = new Rectangle(50, 100, 80,80);
            Player dino = new Player(playerShape, 2, mainGameScreen);
            dino.ObjectImage = gameImageList.Images[0];
            mainGameScreen.AddGameObjects(dino);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            mainGameScreen.UpdateFrame(mainGameScreen.Screen.BackColor);
        }
    }
}
