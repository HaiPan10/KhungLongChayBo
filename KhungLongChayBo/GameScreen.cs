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

        public void ClearScreen(Color back)
        {
            pen.Clear(back);
        }
        public void UpdateFrame(Color color)
        {
            ClearScreen(color);
            foreach (GameObjects item in ListOfGameObjects)
            {
                item.ObjectFallDown();
                item.Display();
            }
            Console.WriteLine(ListOfGameObjects.Count);
            DeleteObjectsOutOfBorder();
        }
        public void DeleteObjectsOutOfBorder()
        {
            bool flag = true;
            while(flag)
            {
                foreach(GameObjects item in ListOfGameObjects)
                {
                    if (item.IsOutOfBorder())
                    {
                        RemoveGameObjects(item);
                        flag = false;
                        break;
                    }
                }
                if (!flag)
                {
                    flag = true;
                }
                else
                    flag = false;
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
        public void SetBackground(Image back)
        {
            Screen.BackgroundImage = back;
        }
    }
}