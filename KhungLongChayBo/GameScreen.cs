using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace KhungLongChayBo
{
    class GameScreen
    {
        private Bitmap screen;
        private Bitmap background;
        private Graphics pen;
        private List<GameObjects> listOfGameObjects;
        private List<GameObjects> deletedItemCollector;
        private int distance = 0;
        private int speed = 5;
        public GameScreen(Bitmap target, Bitmap backGround)
        {
            Screen = target;
            Background = backGround;
            Pen = Graphics.FromImage(target);
            Pen.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            ListOfGameObjects = new List<GameObjects>();
            DeletedItemCollector = new List<GameObjects>();
        }

        public Bitmap Screen { get => screen; set => screen = value; }
        public Graphics Pen { get => pen; set => pen = value; }
        internal List<GameObjects> ListOfGameObjects { get => listOfGameObjects; set => listOfGameObjects = value; }
        internal List<GameObjects> DeletedItemCollector { get => deletedItemCollector; set => deletedItemCollector = value; }
        public Bitmap Background { get => background; set => background = value; }

        public void ClearScreen()
        {
            //Draw the background for the image
            distance -= speed;
            if (distance < -Screen.Width)
                distance = -speed;
            //Make the background move 
            pen.DrawImage(Background, new Point(distance, 0));
            pen.DrawImage(Background, new Point(Screen.Width + distance - speed, 0));
        }
        public void UpdateFrame()
        {
            ClearScreen();
            //Draw the objects on the background
            foreach (GameObjects item in ListOfGameObjects)
            {
                item.Display();
            }
            ClearUp();
        }
        public void CollectOutOfBorder()
        {
            foreach (GameObjects item in ListOfGameObjects)
            {
                if(item.IsOutOfBorder())
                {
                    DeletedItemCollector.Add(item);
                }
            }
        }
        public void AddGameObjects(GameObjects item)
        {
            listOfGameObjects.Add(item);
        }
        public bool RemoveGameObjects(GameObjects item)
        {
            foreach(GameObjects gameItem in ListOfGameObjects)
            {
                if(gameItem.Equals(item))
                {
                    ListOfGameObjects.Remove(gameItem);
                    return true;
                }
            }
            return false;
        }
        public void ClearUp()
        {
            CollectOutOfBorder();
            while(DeletedItemCollector.Count > 0)
            {
                ListOfGameObjects.Remove(DeletedItemCollector[0]);
                DeletedItemCollector.RemoveAt(0);
            }
        }
    }
}