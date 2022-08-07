using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhungLongChayBo
{
    class ArmorItem : Item
    {
        private static Image armorImage = Image.FromFile(Application.StartupPath +
            @"\Dino Run\Items\Armor.png");
        private static Image armorSymbol = Image.FromFile(Application.StartupPath +
            @"\Dino Run\Items\Armor symbol.png");
        private static int numberOfDinoArmor = 0;
        public ArmorItem(Rectangle objectShape, int gravityForce, GameScreen gameScreen) : 
            base(objectShape, gravityForce, gameScreen)
        {
        }

        public ArmorItem(int x, int y, int width, int height, int gravityForce, GameScreen gameScreen) : 
            base(x, y, width, height, gravityForce, gameScreen)
        {
        }

        public static Image ArmorImage { get => armorImage; set => armorImage = value; }
        public static Image ArmorSymbol { get => armorSymbol; set => armorSymbol = value; }
        public static int NumberOfDinoArmor { get => numberOfDinoArmor; set => numberOfDinoArmor = value; }

        public override void Effect(GreenDino dino)
        {
            ++numberOfDinoArmor;
        }
    }
}
