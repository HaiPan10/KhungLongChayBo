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
        private bool isDestroy = false;
        private int speed;
        private bool hittable = true;
        private int hitBoxPadding = 5;
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
        public bool IsDestroy { get => isDestroy; set => isDestroy = value; }
        public int HitBoxPadding { get => hitBoxPadding; set => hitBoxPadding = value; }

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
            return ObjectShape.X + ObjectShape.Width < 0 ||
                ObjectShape.Y + ObjectShape.Height < 0 ||
                ObjectShape.X > GameScreen.Screen.Width ||
                ObjectShape.Y > GameScreen.Screen.Height;
        }
        public void MoveForward(int speed)
        {
            int newPosX = ObjectShape.X + speed;
            Point p = new Point(newPosX, ObjectShape.Y);
            ObjectShape = new Rectangle(p, ObjectShape.Size);
        }
        public virtual List<GameObjects> HittingObjects()
        {
            List<GameObjects> collector = new List<GameObjects>();
            //Return the GameObjects which is being hit by this object
            foreach (GameObjects ob in GameScreen.ListOfGameObjects)
            {
                //The object in list is hittable and not this object
                if (ob.Hittable && !ob.Equals(this) && IsHittingObject(ob))
                {
                    collector.Add(ob);
                }
            }
            return collector;
        }
        public bool IsHittingObject(GameObjects ob)
        {
            //This location
            int thisTop = this.ObjectShape.Y + HitBoxPadding;
            int thisBottom = this.ObjectShape.Y + this.ObjectShape.Height - HitBoxPadding;
            int thisLeft = this.ObjectShape.X + HitBoxPadding;
            int thisRight = this.ObjectShape.X + this.ObjectShape.Width - HitBoxPadding;

            //other location
            int otherTop = ob.ObjectShape.Y + HitBoxPadding;
            int otherBottom = ob.ObjectShape.Y + ob.ObjectShape.Height - HitBoxPadding;
            int otherLeft = ob.ObjectShape.X + HitBoxPadding;
            int otherRight = ob.ObjectShape.X + ob.ObjectShape.Width - HitBoxPadding;

            bool isHit = true;
            if (thisTop > otherBottom || 
                thisBottom < otherTop ||
                thisLeft > otherRight ||
                thisRight < otherLeft)
            {
                isHit = false;
            }
            return isHit;
        }
        public void KeepInBorder()
        {
            int newPosY = ObjectShape.Location.Y;
            int newPosX = ObjectShape.Location.X;
            if (ObjectShape.Location.X < 0)
            {
                newPosX = 0;
            }
            else if (ObjectShape.Location.Y < 0)
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
        public bool IsOnTop(GameObjects ob) //Check if this is on the other game object
        {
            bool isOn = true;
            int thisBottom = ObjectShape.Y + ObjectShape.Height;
            int thisLeft = ObjectShape.X;
            int thisRight = ObjectShape.X + ObjectShape.Width;

            int otherTop = ob.ObjectShape.Y;
            int otherLeft = ob.ObjectShape.X;
            int otherRight = ob.ObjectShape.X + ob.ObjectShape.Width;

            if(thisBottom <= otherTop || otherRight < thisLeft || otherLeft > thisRight)
            {
                isOn = false;
            }
            //Console.WriteLine(isOn);
            return isOn;
        }
        public void KeepOnOtherTop(GameObjects ob)
        {
            //Help to keep the player on which ground
            if (ob == null)
                return;
            int groundTop = ob.ObjectShape.Y + HitBoxPadding;
            int playerBottom = ObjectShape.Y + ObjectShape.Height - HitBoxPadding;
            int newPosY = ObjectShape.Y;
            if (playerBottom >= groundTop)
            {
                newPosY = groundTop - ObjectShape.Height + HitBoxPadding;
            }
            Point p = new Point(ObjectShape.X, newPosY);
            Size s = new Size(ObjectShape.Width, ObjectShape.Height);
            ObjectShape = new Rectangle(p, s);
        }
    }
}