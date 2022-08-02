using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhungLongChayBo
{
    class Obstacle : GameObjects
    {
        public Obstacle(Rectangle objectShape, int gravityForce, GameScreen gameScreen) : 
            base(objectShape, gravityForce, gameScreen)
        {
        }

        public Obstacle(int x, int y, int width, int height, int gravityForce, GameScreen gameScreen) : 
            base(x, y, width, height, gravityForce, gameScreen)
        {
        }

        public override void Display()
        {
            base.Display();
            MoveForward(-Speed);
            List<GameObjects> objects = HittingObjects();
            if(objects.Count > 0)
            {
                foreach(GameObjects ob in objects)
                {
                    if (ob.GetType() != typeof(Bullet) && ob.GetType() != this.GetType())
                    {
                        this.Speed = 0;
                        ob.ObjectGravity.Force = 0;
                        ob.ObjectGravity.Speed = 0;
                    }
                }

            }
        }
    }
}
