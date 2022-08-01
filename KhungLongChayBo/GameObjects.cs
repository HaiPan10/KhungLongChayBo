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
        private Rectangle objectShape;
        private Image objectImage;
        private GameScreen gameScreen;
        private Gravity objectGravity;
        private int speed;
        private bool hittable = true;
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
        public bool Hittable { get => hittable; set => hittable = value; }
        public int Speed { get => speed; set => speed = value; }

        public void ObjectFallDown()
        {
            objectGravity.FallDown();
        }
        public virtual void Display()
        {
            //Display the behaviors of a game object
            if(objectImage == null)
            {
                gameScreen.Pen.FillRectangle(Brushes.Red, ObjectShape);
                
            }
            else
            {
                gameScreen.Pen.DrawImage(objectImage, ObjectShape);
                //gameScreen.Pen.DrawRectangle(Pens.Red, ObjectShape);
            }
        }
        public bool IsOutOfBorder()
        {
            //Check if the object is completely out of the game screen
            return ObjectShape.X <= 0 ||
                ObjectShape.Y <= 0 ||
                ObjectShape.X >= GameScreen.Screen.Width ||
                ObjectShape.Y >= GameScreen.Screen.Height;
        }
        public void MoveForward(int speed)
        {
            int newPosX = ObjectShape.X + speed;
            Point p = new Point(newPosX, ObjectShape.Y);
            ObjectShape = new Rectangle(p, ObjectShape.Size);
        }
        public virtual GameObjects HittingObject()
        {
            //Return the GameObjects which is being hit by this object
            foreach (GameObjects ob in GameScreen.ListOfGameObjects)
            {
                //The object in list is hittable and not this object
                if (ob.Hittable && !ob.Equals(this))
                {
                    if (IsHittingLeft(ob) || IsHittingRight(ob) ||
                        IsHittedBottom(ob) || IsHittedTop(ob))
                    {
                        return ob;
                    }
                }
            }
            return null;
        }
        public bool IsHittingLeft(GameObjects ob)
        {
            int rightSide = this.ObjectShape.X + this.ObjectShape.Width + Speed; //Right side of this object
            if (rightSide >= ob.ObjectShape.X &&
                rightSide <= ob.ObjectShape.X + ob.Speed + ob.ObjectShape.Width &&
                IsInRangeVertical(ob))
            {
                return true;
            }
            return false;
        }
        public bool IsHittingRight(GameObjects ob)
        {
            int rightSide = ob.ObjectShape.X + ob.ObjectShape.Width;
            if(IsInRangeVertical(ob) && 
                this.ObjectShape.X <= rightSide &&
                this.ObjectShape.X + this.ObjectShape.Width + this.Speed >= rightSide)
            {
                return true;
            }
            return false;
        }
        public bool IsHittedBottom(GameObjects ob)
        {
            return false;
        }
        public bool IsHittedTop(GameObjects ob)
        {
            
            return false;
        }
        public bool IsInRangeVertical(GameObjects ob)
        {
            //Check if the range of the object is in game of this object in vertically
            if((this.ObjectShape.Y >= ob.ObjectShape.Y && 
                this.ObjectShape.Y <= ob.ObjectShape.Y + ob.ObjectShape.Height) || 
                (this.ObjectShape.Y + this.ObjectShape.Height >= ob.ObjectShape.Y &&
                this.ObjectShape.Y + this.ObjectShape.Height <= ob.ObjectShape.Y + ob.ObjectShape.Height))
            {
                return true;
            }
            return false;
        }
        public bool IsInRangeHorizontal(GameObjects ob)
        {
            if ((this.ObjectShape.X >= ob.ObjectShape.X &&
                this.ObjectShape.X <= ob.ObjectShape.X + ob.ObjectShape.Width) ||
                (this.ObjectShape.X + this.ObjectShape.Width >= ob.ObjectShape.X &&
                this.ObjectShape.X + this.ObjectShape.Width <= ob.ObjectShape.X + ob.ObjectShape.Width))
            {
                return true;
            }
            return false;
        }
        public void KeepInBorder()
        {
            int newPosY = ObjectShape.Location.Y;
            int newPosX = ObjectShape.Location.X;
            if (ObjectShape.Location.X <= 0)
            {
                newPosX = 0;
            }
            else if (ObjectShape.Location.Y <= 0)
            {
                newPosY = 0;
            }
            else if(ObjectShape.X + ObjectShape.Width >= GameScreen.Screen.Width)
            {
                newPosX = GameScreen.Screen.Width - ObjectShape.Width;
            }
            else if(ObjectShape.Y + ObjectShape.Height >= GameScreen.Screen.Height)
            {
                newPosY = GameScreen.Screen.Height - ObjectShape.Height;
            }

            Point p = new Point(newPosX, newPosY);
            Rectangle r = new Rectangle(p, ObjectShape.Size);
            ObjectShape = r;
        }
    }
}