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
        private static int maxNumberOfArmor = 3;
        private static List<ArmorItem> armors = new List<ArmorItem>();
        private static int armorSymbolWidth = 50;
        private static int armorSymbolHeight = 50;
        public ArmorItem(Rectangle objectShape, int gravityForce, GameScreen gameScreen) : 
            base(objectShape, gravityForce, gameScreen)
        {
            ObjectImage = ArmorImage;
        }

        public ArmorItem(int x, int y, int width, int height, int gravityForce, GameScreen gameScreen) : 
            base(x, y, width, height, gravityForce, gameScreen)
        {
            ObjectImage = ArmorImage;
        }

        public static Image ArmorImage { get => armorImage; set => armorImage = value; }
        public static Image ArmorSymbol { get => armorSymbol; set => armorSymbol = value; }
        public static int NumberOfDinoArmor { get => numberOfDinoArmor; set => numberOfDinoArmor = value; }
        public static int MaxNumberOfArmor { get => maxNumberOfArmor; set => maxNumberOfArmor = value; }
        internal static List<ArmorItem> Armors { get => armors; set => armors = value; }
        public static int ArmorSymbolWidth { get => armorSymbolWidth; set => armorSymbolWidth = value; }
        public static int ArmorSymbolHeight { get => armorSymbolHeight; set => armorSymbolHeight = value; }

        public override void Effect(GreenDino dino)
        {
            IncreaseArmor(dino);
        }
        public static void IncreaseArmor(GreenDino dino)
        {
            if(NumberOfDinoArmor < MaxNumberOfArmor && Armors.Count < MaxNumberOfArmor)
            {
                ArmorItem temp = CreateArmor(dino);
                Armors.Add(temp);
                dino.GameScreen.AddedItemCollector.Add(temp);
                ++NumberOfDinoArmor;
            }
        }
        public static void DecreaseArmor(GreenDino dino)
        {
            if(NumberOfDinoArmor > 0)
            {
                int index = --NumberOfDinoArmor;
                dino.GameScreen.DeletedItemCollector.Add(Armors[index]);
                Armors.RemoveAt(index);
            }
        }
        public static ArmorItem CreateArmor(GreenDino dino)
        {
            int paddingTop = 10;
            int paddingLeft = 10;
            Rectangle r;
            if(NumberOfDinoArmor == 0)
            {
                int x = SoldierItem.TextBoxAmmoX + SoldierItem.TextBoxAmmoWidth + paddingLeft;
                int y = SoldierItem.TextBoxAmmoY + paddingTop;
                r = new Rectangle(x, y, ArmorSymbolWidth, ArmorSymbolHeight);
            }
            else
            {
                int x = Armors[NumberOfDinoArmor - 1].ObjectShape.X + ArmorSymbolWidth + paddingLeft;
                int y = SoldierItem.TextBoxAmmoY + paddingTop;
                r = new Rectangle(x, y, ArmorSymbolWidth, ArmorSymbolHeight);
            }
            ArmorItem item = new ArmorItem(r, 0, dino.GameScreen);
            item.ObjectImage = ArmorItem.ArmorSymbol;
            item.IsDestroy = true;
            item.Hittable = false;
            return item;
        }
    }
}