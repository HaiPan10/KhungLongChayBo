using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KhungLongChayBo.Properties
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

        public void KeepInBorder()
        {
            int newPosY = ObjectShape.Location.Y;
            int newPosX = ObjectShape.Location.X;
            if (ObjectShape.Location.X + ObjectShape.Width <= 0 ||
                ObjectShape.Location.X + ObjectShape.Width >= GameScreen.Screen.Width)
            {
                newPosX = GameScreen.Screen.Width - ObjectShape.Width;
            }
            else if (/*ObjectShape.Location.Y + ObjectShape.Height <= 0 ||*/
                ObjectShape.Location.Y + ObjectShape.Height >= GameScreen.Screen.Height)
            {
                newPosY = GameScreen.Screen.Height - ObjectShape.Height;
            }
            Point p = new Point(newPosX, newPosY);
            Rectangle r = new Rectangle(p, ObjectShape.Size);
            ObjectShape = r;
        }

        public void Jumping()
        {
            if (ObjectGravity.GravitySpeed > 0)
            {
                ObjectGravity.GravitySpeed = -JumpingHeight;
            }
        }

        public bool isGrounded()
        {
            int temp = GameScreen.Screen.Height - ObjectShape.Height;
            if (ObjectShape.Y >= temp)
                return true;
            else
                return false;
        }
    }
}
