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
            Speed = 20;
        }

        public Item(int x, int y, int width, int height, int gravityForce, GameScreen gameScreen) : 
            base(x, y, width, height, gravityForce, gameScreen)
        {
            Speed = 20;
        }

        public override void Display()
        {
            base.Display();
            if(!IsDestroy)
            {
                ObjectFallDown();
                MoveForward(-Speed);
                KeepInBorder();
                List<GameObjects> list = HittingObjects();
                foreach (GameObjects ob in list)
                {
                    //Keep Item on any top but except player top
                    if (ob.GetType() != this.GetType() && 
                        IsOnTop(ob) && 
                        ob.GetType() != typeof(GreenDino) &&
                        ob.GetType().BaseType != typeof(GreenDino))
                    {
                        KeepOnOtherTop(ob);
                    }
                }
                
            }
        }

        public abstract void Effect(GreenDino dino);
    }
}
