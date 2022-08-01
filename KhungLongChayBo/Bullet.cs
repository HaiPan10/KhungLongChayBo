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
        public Bullet(Rectangle playerShape, int gravityFoce, GameScreen screen)
            : base(playerShape, gravityFoce, screen)
        {
            Speed = 20;
        }

        public Bullet(int x, int y, int width, int height, int gravityFoce, GameScreen screen)
            : base(x, y, width, height, gravityFoce, screen)
        {
            Speed = 20;
        }
        public override void Display()
        {
            //Display the behaviors of a bullet
            base.Display();
            GameObjects ob = HittingObject();
            if (ob != null && ob.GetType() != this.GetType())
            {
                GameScreen.DeletedItemCollector.Add(ob);
                GameScreen.DeletedItemCollector.Add(this);
            }
            MoveForward(Speed);
        }
    }
}
