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
        private Control screen;
        private Graphics pen;
        private List<GameObjects> listOfGameObjects;
        public GameScreen(Control target)
        {
            Screen = target;
            Pen = target.CreateGraphics();
            ListOfGameObjects = new List<GameObjects>();
        }

        public Control Screen { get => screen; set => screen = value; }
        public Graphics Pen { get => pen; set => pen = value; }
        internal List<GameObjects> ListOfGameObjects { get => listOfGameObjects; set => listOfGameObjects = value; }

        public void clearScreen()
        {
            pen.Clear(Color.White);
        }
        public void updateFrame()
        {
            clearScreen();
            foreach (GameObjects item in ListOfGameObjects)
            {
                item.display();
            }
        }
        public void addGameObjects(GameObjects item)
        {
            listOfGameObjects.Add(item);
        }
        public bool removeGameObjects(GameObjects item)
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
            //Pen.back
        }
    }
}