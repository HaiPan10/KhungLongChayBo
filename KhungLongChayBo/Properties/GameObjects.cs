using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace KhungLongChayBo.Properties
{
    abstract class GameObjects
    {
        private Rectangle objectShape;
        private Image objectImage;
        private GameScreen gameScreen;
        private Gravity objectGravity;
        public GameObjects(Rectangle objectShape, int gravityForce, GameScreen gameScreen)
        {
            ObjectShape = objectShape;
            GameScreen = gameScreen;
            objectGravity = new Gravity(this, gravityForce);
        }
        public GameObjects(int x,int y, int width, int height, int gravityForce, GameScreen gameScreen)
        {
            ObjectShape = new Rectangle(x, y, width, height);
            GameScreen = gameScreen;
            objectGravity = new Gravity(this, gravityForce);
        }
        
        public Image ObjectImage { get => objectImage; set => objectImage = value; }
        public GameScreen GameScreen { get => gameScreen; set => gameScreen = value; }
        public Rectangle ObjectShape { get => objectShape; set => objectShape = value; }
        internal Gravity ObjectGravity { get => objectGravity; set => objectGravity = value; }
        public void ObjectFallDown()
        {
            objectGravity.FallDown();
        }
        public void Display()
        {
            if(objectImage == null)
            {
                gameScreen.Pen.FillRectangle(Brushes.White, ObjectShape);
            }
            else
            {
                gameScreen.Pen.DrawImage(objectImage, ObjectShape);
            }
        }
    }
}