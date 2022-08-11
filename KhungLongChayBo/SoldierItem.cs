using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhungLongChayBo
{
    class SoldierItem : Item
    {
        private static Image soldierItemImage = Image.FromFile(Application.StartupPath +
            @"\Dino Run\Items\Soldier.png");
        private static int textBoxAmmoHeight = 50;
        private static int textBoxAmmoWidth = 100;
        private static int textBoxAmmoX = 0;
        private static int textBoxAmmoY = 0;
        public SoldierItem(Rectangle objectShape, int gravityForce, GameScreen gameScreen) : 
            base(objectShape, gravityForce, gameScreen)
        {
            ObjectImage = SoldierItemImage;
        }

        public SoldierItem(int x, int y, int width, int height, int gravityForce, GameScreen gameScreen) :
            base(x, y, width, height, gravityForce, gameScreen)
        {
            ObjectImage = SoldierItemImage;
        }

        public static Image SoldierItemImage { get => soldierItemImage; set => soldierItemImage = value; }
        public static int TextBoxAmmoHeight { get => textBoxAmmoHeight; set => textBoxAmmoHeight = value; }
        public static int TextBoxAmmoWidth { get => textBoxAmmoWidth; set => textBoxAmmoWidth = value; }
        public static int TextBoxAmmoX { get => textBoxAmmoX; set => textBoxAmmoX = value; }
        public static int TextBoxAmmoY { get => textBoxAmmoY; set => textBoxAmmoY = value; }

        public override void Effect(GreenDino dino)
        {
            ChangeToSoldier(dino);
        }
        private static void ChangeToSoldier(GreenDino dino)
        {
            dino.StopCrouching();
            dino.GameScreen.AddedItemCollector.Add(CreateSoldierDino(dino));
        }
        private static SoldierDino CreateSoldierDino(GreenDino dino)
        {
            if (dino.Crouch > 0)
            {
                dino.ObjectShape = new Rectangle(dino.ObjectShape.X, dino.ObjectShape.Y,
                   dino.ObjectShape.Width, dino.ObjectShape.Height + dino.Crouch);
            }
            SoldierDino soldierDino = new SoldierDino(dino.ObjectShape,
                dino.ObjectGravity.Force, dino.GameScreen);
            return soldierDino;
        }
    }
}
