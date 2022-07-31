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
        private int speed;
        private int force;
        private GameObjects target;
        public int Speed { get => speed; set => speed = value; }
        public int Force { get => force; set => force = value; }
        internal GameObjects Target { get => target; set => target = value; }

        public Gravity(GameObjects target, int gravityForce)
        {
            Target = target;
            Force = gravityForce;
            speed = 0;
        }
        public void IncreasingSpeed()
        {
            Speed += Force;
        }
        public void FallDown()
        {
            IncreasingSpeed();
            int newPosY = Target.ObjectShape.Y + speed;
            Point newPos = new Point(Target.ObjectShape.X, newPosY);
            Rectangle newShape = new Rectangle(newPos, Target.ObjectShape.Size);
            Target.ObjectShape = newShape;
        }
    }
}