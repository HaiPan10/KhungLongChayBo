using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhungLongChayBo
{
    class Score : TextBox
    {
        private static float point = 0.4F; //point per interval milisecond
        private float totalPoint = 0F; //The total point 
        private static int highestPoint;

        public static float Point { get => point; set => point = value; }
        public static int HighestPoint { get => highestPoint; set => highestPoint = value; }
        public float TotalPoint { get => totalPoint; set => totalPoint = value; }

        public Score(Rectangle objectShape, int gravityForce, GameScreen gameScreen) : 
            base(objectShape, gravityForce, gameScreen)
        {
            Text = String.Format("{0}", Convert.ToInt16(TotalPoint));
        }

        public Score(int x, int y, int width, int height, int gravityForce, GameScreen gameScreen) : 
            base(x, y, width, height, gravityForce, gameScreen)
        {
            Text = String.Format("{0}", Convert.ToInt16(TotalPoint));
        }

        public void IncreasingPoint()
        {
            TotalPoint += Point;
        }
        public override void Display()
        {
            base.Display();
            IncreasingPoint();
            int newPoint = Convert.ToInt16(TotalPoint);
            if (newPoint > Convert.ToInt16(Text))
            {
                Text = String.Format("{0}", newPoint);
            }
        }
    }
}
