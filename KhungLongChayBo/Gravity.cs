using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KhungLongChayBo
{
    class Gravity
    {
        private int gravitySpeed;
        private int gravityForce;
        private GameObjects target;
        public int GravitySpeed { get => gravitySpeed; set => gravitySpeed = value; }
        public int GravityForce { get => gravityForce; set => gravityForce = value; }
        internal GameObjects Target { get => target; set => target = value; }

        public Gravity(GameObjects target, int gravityForce)
        {
            Target = target;
            GravityForce = gravityForce;
            gravitySpeed = 0;
        }
        public void IncreasingSpeed()
        {
            GravitySpeed += GravityForce;
        }
        public void FallDown()
        {
            IncreasingSpeed();
            int newPosY = Target.ObjectShape.Y + gravitySpeed;
            Point newPos = new Point(Target.ObjectShape.X, newPosY);
            Rectangle newShape = new Rectangle(newPos, Target.ObjectShape.Size);
            Target.ObjectShape = newShape;
        }
    }
}