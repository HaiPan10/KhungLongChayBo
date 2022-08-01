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
        private int jumpingHeight = 36;

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

        public bool IsGrounded()
        {
            int temp = GameScreen.Screen.Height - ObjectShape.Height;
            if (ObjectShape.Y >= temp)
                return true;
            else
                return false;
        }
        public virtual void Action()
        {

        }
        public override void Display()
        {
            ObjectFallDown();
            KeepInBorder();
            base.Display();
        }
    }
}
