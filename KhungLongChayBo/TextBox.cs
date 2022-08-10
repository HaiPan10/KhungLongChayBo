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
        string text = String.Empty;
        Brush brush = Brushes.Black;
        Font font = new Font("Arial",16F); //Default font
        StringFormat stringFormat = new StringFormat();
        public TextBox(Rectangle objectShape, int gravityForce, GameScreen gameScreen) : 
            base(objectShape, gravityForce, gameScreen)
        {
            Hittable = false;
        }

        public TextBox(int x, int y, int width, int height, int gravityForce, GameScreen gameScreen) : 
            base(x, y, width, height, gravityForce, gameScreen)
        {
            Hittable = false;
        }

        public string Text { get => text; set => text = value; }
        public Font Font 
        { 
            get => font;
            set
            {
                font = value;
            }
        }
        public StringFormat StringFormat { get => stringFormat; set => stringFormat = value; }
        public Brush Brush { get => brush; set => brush = value; }

        public override void Display()
        {
            if (text == String.Empty)
                return; //Don't draw anything
            GameScreen.Pen.DrawString(Text, Font, Brush, ObjectShape, StringFormat);
        }
    }
}
