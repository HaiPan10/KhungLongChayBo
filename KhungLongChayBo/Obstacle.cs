using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace KhungLongChayBo
{
    class Obstacle : GameObjects
    {
        private static Image flyObstacle = Image.FromFile(Application.StartupPath +
                @"\Dino Run\Maps\Obstacles\Obstacle Fly.png");
        private static Image tree1 = Image.FromFile(Application.StartupPath +
                @"\Dino Run\Maps\Obstacles\Obstacle Tree.png");
        private static Image tree2 = Image.FromFile(Application.StartupPath +
                @"\Dino Run\Maps\Obstacles\Obstacle Tree 2.png");
        public Obstacle(Rectangle objectShape, int gravityForce, GameScreen gameScreen) : 
            base(objectShape, gravityForce, gameScreen)
        {
        }

        public Obstacle(int x, int y, int width, int height, int gravityForce, GameScreen gameScreen) :
            base(x, y, width, height, gravityForce, gameScreen)
        {
        }

        public static Image FlyObstacle { get => flyObstacle; set => flyObstacle = value; }
        public static Image Tree1 { get => tree1; set => tree1 = value; }
        public static Image Tree2 { get => tree2; set => tree2 = value; }

        public override void Display()
        {
            base.Display();
            if(!IsDestroy)
                MoveForward(-Speed);
        }
    }
}
