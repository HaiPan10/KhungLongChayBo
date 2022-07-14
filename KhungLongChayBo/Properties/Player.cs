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
        private Gravity playerGravity;
        public Player(Rectangle playerShape) : base(playerShape)
        {
            playerGravity = new Gravity(1);
        }
        internal Gravity PlayerGravity { get => playerGravity; set => playerGravity = value; }
        public void playerFallDown()
        {
            playerGravity.fallDown(ref objectShape);
        }
    }
}
