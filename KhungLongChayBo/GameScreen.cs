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
        private List<GameObjects> addedItemCollector;
        private int distance = 0;
        private int speed = 1;
        public GameScreen(Bitmap target, Bitmap backGround)
        {
            Screen = target;
            Background = backGround;
            Pen = Graphics.FromImage(target);
            Pen.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            ListOfGameObjects = new List<GameObjects>();
            DeletedItemCollector = new List<GameObjects>();
            AddedItemCollector = new List<GameObjects>();
        }

        public Bitmap Screen { get => screen; set => screen = value; }
        public Graphics Pen { get => pen; set => pen = value; }
        internal List<GameObjects> ListOfGameObjects { get => listOfGameObjects; set => listOfGameObjects = value; }
        internal List<GameObjects> DeletedItemCollector { get => deletedItemCollector; set => deletedItemCollector = value; }
        public Bitmap Background { get => background; set => background = value; }
        public int Distance { get => distance; set => distance = value; }
        public int Speed { get => speed; set => speed = value; }
        internal List<GameObjects> AddedItemCollector { get => addedItemCollector; set => addedItemCollector = value; }

        public void ClearScreen()
        {
            //Draw the background for the image
            Distance -= Speed;
            if (Distance < -Screen.Width)
                Distance = -Speed;
            //Make the background move 
            pen.DrawImage(Background, new Point(Distance, 0));
            pen.DrawImage(Background, new Point(Screen.Width + Distance - Speed, 0));
        }
        public bool UpdateFrame()
        {
            bool isEndGame = false;
            //Console.WriteLine(this.ListOfGameObjects.Count);
            ClearScreen();
            //Draw the objects on the background
            foreach (GameObjects item in ListOfGameObjects)
            {
                item.Display();
                if ((item.GetType().BaseType == typeof(GreenDino) ||
                    item.GetType() == typeof(GreenDino)) && item.IsDestroy)
                {
                    EndGame();
                    isEndGame = true;
                }
            }
            Adding();
            ClearUp();
            return isEndGame;
        }
        public void CollectOutOfBorder()
        {
            foreach (GameObjects item in ListOfGameObjects)
            {
                if (item.IsOutOfBorder())
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
            foreach (GameObjects gameItem in ListOfGameObjects)
            {
                if (gameItem.Equals(item))
                {
                    ListOfGameObjects.Remove(gameItem);
                    return true;
                }
            }
            return false;
        }
        private void ClearUp()
        {
            CollectOutOfBorder();
            while (DeletedItemCollector.Count > 0)
            {
                ListOfGameObjects.Remove(DeletedItemCollector[0]);
                DeletedItemCollector.RemoveAt(0);
            }
        }
        private void Adding()
        {
            //Console.WriteLine(this.AddedItemCollector.Count);
            if (AddedItemCollector.Count > 0)
            {
                foreach (GameObjects ob in AddedItemCollector)
                {
                    AddGameObjects(ob);
                }
                AddedItemCollector.Clear();
            }
        }
        public void ClearAll()
        {
            ListOfGameObjects.Clear();
            AddedItemCollector.Clear();
            DeletedItemCollector.Clear();
        }
        public void EndGame()
        {
            //Adding the textbox game over message
            int endGameWidth = 200;
            int endGameHeight = 50;
            int x = this.Screen.Width / 2 - endGameWidth / 2;
            int y = this.Screen.Height / 3 - endGameHeight;
            Rectangle r = new Rectangle(x, y, endGameWidth, endGameHeight);
            TextBox textBoxEndGame = new TextBox(r, 0, this);
            textBoxEndGame.Text = "GAME OVER";
            textBoxEndGame.StringFormat.Alignment = StringAlignment.Center;
            textBoxEndGame.StringFormat.LineAlignment = StringAlignment.Center;
            textBoxEndGame.Brush = Brushes.Black;
            textBoxEndGame.Display();
        }
    }
}