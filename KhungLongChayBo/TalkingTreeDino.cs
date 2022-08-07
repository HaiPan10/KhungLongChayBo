using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace KhungLongChayBo
{
    class TalkingTreeDino : GreenDino
    {
        private int ammo = 20;
        public TalkingTreeDino(Rectangle playerShape, int gravityFoce, GameScreen screen) : 
            base(playerShape, gravityFoce, screen)
        {
            
        }

        public TalkingTreeDino(int x, int y, int width, int height, int gravityFoce, GameScreen screen)
            : base(x, y, width, height, gravityFoce, screen)
        {

        }
        
        public override void Action()
        {
            if(ammo > 0)
            {
                Shooting();
                --ammo;
            }
        }
        public void Shooting()
        {
            int distance = 3;
            Bullet b = new Bullet(ObjectShape.X + ObjectShape.Width + distance,
                ObjectShape.Y + ObjectShape.Height / 2,
                10, 5, 0, GameScreen);
            GameScreen.AddGameObjects(b);
        }
        public override void InitAnimation()
        {
            string[] filesStand = Directory.GetFiles(Application.StartupPath +
                @"\Dino Run\Dinos\Talking tree Dino\stand");
            string[] filesCrouch = Directory.GetFiles(Application.StartupPath +
                @"\Dino Run\Dinos\Talking tree Dino\crouch");
            foreach (string fileName in filesStand)
            {
                AnimationStand.Add(Image.FromFile(fileName));
            }
            foreach (string fileName in filesCrouch)
            {
                AnimationCrouch.Add(Image.FromFile(fileName));
            }
            ObjectImage = AnimationStand[0];
            Counter = 0;
        }

        public override void Display()
        {
            base.Display();
            if(ammo <= 0)
            {
                ChangeToGreenDino();
            }
        }
    }
}