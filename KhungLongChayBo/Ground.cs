using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhungLongChayBo
{
    class Ground : GameObjects
    {
        public Ground(Rectangle objectShape, int gravityForce, GameScreen gameScreen) : 
            base(objectShape, gravityForce, gameScreen)
        {
        }

        public Ground(int x, int y, int width, int height, int gravityForce, GameScreen gameScreen) : 
            base(x, y, width, height, gravityForce, gameScreen)
        {
        }
        public override void Display()
        {
            //Make the road move
            if (ObjectImage != null)
            {
                Size s = new Size(ObjectShape.Width, ObjectShape.Height);
                Rectangle r1 = new Rectangle(new Point(GameScreen.Distance, ObjectShape.Y),s);
                Rectangle r2 = new Rectangle(new Point(GameScreen.Screen.Width +
                    GameScreen.Distance - GameScreen.Speed, ObjectShape.Y), s);
                GameScreen.Pen.DrawImage(ObjectImage, r1);
                GameScreen.Pen.DrawImage(ObjectImage, r2);
            }
            else
            {
                GameScreen.Pen.FillRectangle(Brushes.Red, ObjectShape);
            }
        }
    }
}
