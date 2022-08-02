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

        
        public virtual void Action()
        {

        }
        public Ground OnGround()
        {
            List<GameObjects> objects = HittingObjects();
            Ground g = null;
            foreach (GameObjects ob in objects)
            {
                if (ob.GetType() == typeof(Ground))
                {
                    g = (Ground)ob;
                    break;
                }
            }
            if (g == null)
                return null;
            if (IsOnTop(g))
                return g;
            else
                return null;
        }
        public override void Display()
        {
            base.Display();
            ObjectFallDown();
            KeepInBorder();
            List<GameObjects> objects = HittingObjects();
            foreach(GameObjects ob in objects)
            {
                if(ob.GetType() == typeof(Ground) && IsOnTop(ob))
                {
                    KeepOnOtherTop(ob);
                    break;
                }
            }
        }
    }
}