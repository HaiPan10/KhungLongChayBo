using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhungLongChayBo
{
    abstract class Item : GameObjects
    {
        public Item(Rectangle objectShape, int gravityForce, GameScreen gameScreen) : 
            base(objectShape, gravityForce, gameScreen)
        {
        }

        public Item(int x, int y, int width, int height, int gravityForce, GameScreen gameScreen) : 
            base(x, y, width, height, gravityForce, gameScreen)
        {
        }

        public override void Display()
        {
            int speed = 20;
            base.Display();
            if(!IsDestroy)
            {
                ObjectFallDown();
                MoveForward(-speed);
                KeepInBorder();
                List<GameObjects> list = HittingObjects();
                foreach (GameObjects ob in list)
                {
                    if (ob.GetType() != this.GetType() && IsOnTop(ob))
                    {
                        KeepOnOtherTop(ob);
                        break;
                    }
                }
                
            }
        }

        public abstract void Effect(GreenDino dino);
    }
}
