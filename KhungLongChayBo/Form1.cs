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
            gameScreen.BackColor = Color.Red;
            gameScreen.Dock = DockStyle.Fill;
            Controls.Add(gameScreen);
            mainGameScreen = new GameScreen(gameScreen);
            Player box1 = new Player(10, 10, 100, 100, 2, mainGameScreen);
            Player box2 = new Player(50, 50, 100, 50, 1, mainGameScreen);
            mainGameScreen.AddGameObjects(box1);
            mainGameScreen.AddGameObjects(box2);
            timer.Interval = 100;
            timer.Enabled = true;
            timer.Tick += timer_Tick;
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                mainGameScreen.UpdateFrame(mainGameScreen.Screen.BackColor);
            }
            catch(NullReferenceException)
            {
                return;
            }
        }
    }
}
