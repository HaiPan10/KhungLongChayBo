using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhungLongChayBo
{
    class TextBox : GameObjects
    {
        public TextBox(Rectangle objectShape, int gravityForce, GameScreen gameScreen) : 
            base(objectShape, gravityForce, gameScreen)
        {
        }

        public TextBox(int x, int y, int width, int height, int gravityForce, GameScreen gameScreen) : 
            base(x, y, width, height, gravityForce, gameScreen)
        {
        }
    }
}
