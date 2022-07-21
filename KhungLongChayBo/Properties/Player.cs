using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KhungLongChayBo.Properties
{
    class Player : GameObjects
    {
        public Player(Rectangle playerShape, int gravityFoce, GameScreen screen) 
            : base(playerShape, gravityFoce, screen)
        {
            
        }
        public Player(int x,int y, int width, int height, int gravityFoce, GameScreen screen) 
            : base(x, y, width, height, gravityFoce, screen)
        {
            
        }

        public void KeepInBorder()
        {
            int newPosY = ObjectShape.Location.Y;
            int newPosX = ObjectShape.Location.X;
            if (ObjectShape.Location.X + ObjectShape.Width <= 0 ||
                ObjectShape.Location.X + ObjectShape.Width >= GameScreen.Screen.Width)
            {
                newPosX = GameScreen.Screen.Width - ObjectShape.Width - 10;
            }
            else if(ObjectShape.Location.Y + ObjectShape.Height <= 0 || 
                ObjectShape.Location.Y + ObjectShape.Height >= GameScreen.Screen.Height)
            {
                newPosY = GameScreen.Screen.Height - ObjectShape.Height - 10;
            }
            Point p = new Point(newPosX, newPosY);
            Size s = new Size(ObjectShape.Width, ObjectShape.Height);
            Rectangle r = new Rectangle(p, s);
            ObjectShape = r;
        }
    }
}
