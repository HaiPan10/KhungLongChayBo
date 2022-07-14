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
        public Player(Rectangle playerShape, int gravityFoce, GameScreen screen) 
            : base(playerShape, gravityFoce, screen)
        {
            
        }
        public Player(int x,int y, int width, int height, int gravityFoce, GameScreen screen) 
            : base(x, y, width, height, gravityFoce, screen)
        {
            
        }
    }
}
