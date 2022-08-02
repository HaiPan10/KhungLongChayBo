using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KhungLongChayBo
{
    class Player : GameObjects
    {
        private int jumpingHeight = 45;

        public int JumpingHeight { get => jumpingHeight; set => jumpingHeight = value; }

        public Player(Rectangle playerShape, int gravityFoce, GameScreen screen)
            : base(playerShape, gravityFoce, screen)
        {

        }
        public Player(int x, int y, int width, int height, int gravityFoce, GameScreen screen)
            : base(x, y, width, height, gravityFoce, screen)
        {

        }

        

        public void Jumping()
        {
            if (ObjectGravity.Speed > 0)
            {
                ObjectGravity.Speed = -JumpingHeight;
            }
        }

        public Ground OnGround()
        {
            List<GameObjects> objects = HittingObjects();
            Ground g = null;
            foreach(GameObjects ob in objects)
            {
                if(ob.GetType() != this.GetType() && ob.GetType() == typeof(Ground))
                {
                    g = (Ground)ob;
                    break;
                }
            }
            if (g == null)
                return null;
            int temp = GameScreen.Screen.Height - g.ObjectShape.Y;
            if (ObjectShape.Y >= temp)
                return g;
            else
                return null;
        }
        public virtual void Action()
        {

        }
        public void KeepOnGround(Ground ground)
        {
            //Help to keep the player on which ground
            if (ground == null)
                return;
            int groundTop = ground.ObjectShape.Y;
            int playerBottom = ObjectShape.Y + ObjectShape.Height;
            int newPosY = ObjectShape.Y;
            if (playerBottom >= groundTop)
            {
                newPosY = groundTop - ObjectShape.Height;
            }
            Point p = new Point(ObjectShape.X, newPosY);
            Size s = new Size(ObjectShape.Width, ObjectShape.Height);
            ObjectShape = new Rectangle(p, s);
        }
        public override void Display()
        {
            base.Display();
            ObjectFallDown();
            KeepInBorder();
            KeepOnGround(OnGround());
        }
    }
}
