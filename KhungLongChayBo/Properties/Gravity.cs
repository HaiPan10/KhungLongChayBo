using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KhungLongChayBo.Properties
{
    class Gravity
    {
        private int gravitySpeed;
        private int gravityWeight;

        public int GravitySpeed { get => gravitySpeed; set => gravitySpeed = value; }
        public int GravityWeight { get => gravityWeight; set => gravityWeight = value; }

        public Gravity(int gravityWeight)
        {
            GravityWeight = gravityWeight;
            gravitySpeed = 0;
        }
        public void increasingSpeed()
        {
            GravitySpeed += GravityWeight;
        }
        public void fallDown(ref Rectangle target)
        {
            increasingSpeed();
            int newPosY = target.Y + gravitySpeed;
            target.Y = newPosY;
        }
    }
}
