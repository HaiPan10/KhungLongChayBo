using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KhungLongChayBo
{
    class VietCongDino : Player
    {
        public VietCongDino(Rectangle playerShape, int gravityFoce, GameScreen screen) : 
            base(playerShape, gravityFoce, screen)
        {

        }

        public VietCongDino(int x, int y, int width, int height, int gravityFoce, GameScreen screen)
            : base(x, y, width, height, gravityFoce, screen)
        {

        }
        
        public override void Action()
        {
            Shooting();
        }
        public void Shooting()
        {
            int distance = 3;
            Bullet b = new Bullet(ObjectShape.X + ObjectShape.Width + distance,
                ObjectShape.Y + ObjectShape.Height / 2,
                10, 5, 0, GameScreen);
            GameScreen.AddGameObjects(b);
        }
    }
}
