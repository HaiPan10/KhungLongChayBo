using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace KhungLongChayBo
{
    abstract class GameObjects
    {
        protected Rectangle objectShape;
        protected Image objectImage;
        private GameScreen gameScreen;
        public GameObjects(Rectangle objectShape, GameScreen gameScreen)
        {
            ObjectShape = objectShape;
            GameScreen = gameScreen;
        }

        public Rectangle ObjectShape { get => ObjectShape; set => ObjectShape = value; }
        public Image ObjectImage { get => objectImage; set => objectImage = value; }
        internal GameScreen GameScreen { get => gameScreen; set => gameScreen = value; }

        public void display()
        {
            gameScreen.Pen.DrawImage(objectImage, objectShape);
        }
    }
}
