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
        private int distance = 0;
        private int speed = 20;
        public Ground(Rectangle objectShape, int gravityForce, GameScreen gameScreen) : 
            base(objectShape, gravityForce, gameScreen)
        {
        }

        public Ground(int x, int y, int width, int height, int gravityForce, GameScreen gameScreen) : 
            base(x, y, width, height, gravityForce, gameScreen)
        {
        }

        public int Distance { get => distance; set => distance = value; }
        public int Speed1 { get => speed; set => speed = value; }

        public override void Display()
        {
            //Make the road move
            if (ObjectImage != null)
            {
                distance -= speed;
                if (-distance >= ObjectShape.Width)
                    distance = 0;
                Size s = new Size(ObjectShape.Width, ObjectShape.Height);
                Rectangle r1 = new Rectangle(new Point(Distance, ObjectShape.Y),s);
                Rectangle r2 = new Rectangle(new Point(GameScreen.Screen.Width +
                    Distance - Speed, ObjectShape.Y), s);
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
