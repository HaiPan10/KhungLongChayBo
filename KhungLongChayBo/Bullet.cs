using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KhungLongChayBo
{
    class Bullet : GameObjects
    {
        private int speed = 40;
        public Bullet(Rectangle playerShape, int gravityFoce, GameScreen screen)
            : base(playerShape, gravityFoce, screen)
        {

        }

        public Bullet(int x, int y, int width, int height, int gravityFoce, GameScreen screen)
            : base(x, y, width, height, gravityFoce, screen)
        {

        }
        public int Speed { get => speed; set => speed = value; }
        public void Moving()
        {
            int newPosX = ObjectShape.X + speed;
            Point p = new Point(newPosX, ObjectShape.Y);
            ObjectShape = new Rectangle(p, ObjectShape.Size);
        }

        public override void Display()
        {
            Moving();
            base.Display();
        }
    }
}
