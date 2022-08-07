using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace KhungLongChayBo
{
    class Player : GameObjects
    {
        private int jumpingHeight = 45;
        private List<Image> animationStand = new List<Image>();
        private List<Image> animationCrouch = new List<Image>();
        private int crouch = 0; //Player alway stand
        private int counter; //use for change animation
        private Timer time;

        public int JumpingHeight { get => jumpingHeight; set => jumpingHeight = value; }
        public List<Image> AnimationStand { get => animationStand; set => animationStand = value; }
        public List<Image> AnimationCrouch { get => animationCrouch; set => animationCrouch = value; }
        public int Crouch { get => crouch; set => crouch = value; }
        public int Counter { get => counter; set => counter = value; }
        private Timer Time { get => time; set => time = value; }

        public Player(Rectangle playerShape, int gravityFoce, GameScreen screen)
            : base(playerShape, gravityFoce, screen)
        {
            InitClock();
        }
        public Player(int x, int y, int width, int height, int gravityFoce, GameScreen screen)
            : base(x, y, width, height, gravityFoce, screen)
        {
            InitClock();
        }
        private void InitClock()
        {
            Time = new Timer();
            Time.Interval = 100; //Do the animtion per 0.5s
            Time.Tick += Time_Tick;
            Time.Enabled = true;
        }

        private void Time_Tick(object sender, EventArgs e)
        {
            DoAnimation();
        }

        public void Jumping()
        {
            if (ObjectGravity.Speed > 0 && Crouch <= 0)
            {
                ObjectGravity.Speed = -JumpingHeight;
            }
        }
        public void Crouching()
        {
            if (Crouch > 0 || OnGround() == null)
                return; //Already crouching or is jumping
            Crouch = 30;
            Point p = new Point(ObjectShape.X, ObjectShape.Y + Crouch);
            Size s = new Size(ObjectShape.Width, ObjectShape.Height - Crouch);
            Rectangle r = new Rectangle(p, s);
            ObjectShape = r;
        }
        public void StopCrouching()
        {
            Point p = new Point(ObjectShape.X, ObjectShape.Y - Crouch);
            Size s = new Size(ObjectShape.Width, ObjectShape.Height + Crouch);
            Crouch = 0;
            Rectangle r = new Rectangle(p, s);
            ObjectShape = r;
        }
        public void StopAnimation()
        {
            Time.Enabled = false;
        }
        public void StartAnimation()
        {
            Time.Enabled = true;
        }
        public virtual void InitAnimation()
        {
            string[] filesStand = Directory.GetFiles(Application.StartupPath +
                @"\Dino Run\Dinos\Green Dino\stand");
            string[] filesCrouch = Directory.GetFiles(Application.StartupPath +
                @"\Dino Run\Dinos\Green Dino\crouch");
            foreach (string fileName in filesStand)
            {
                AnimationStand.Add(Image.FromFile(fileName));
            }
            foreach(string fileName in filesCrouch)
            {
                AnimationCrouch.Add(Image.FromFile(fileName));
            }
            ObjectImage = AnimationStand[0];
            Counter = 0;
        }
        public void DoAnimation()
        {
            if (OnGround() == null)
                return;
            ++Counter;
            //Do animation by changing image in list
            if (Crouch > 0)
            {
                if (Counter >= AnimationCrouch.Count)
                    Counter = 0;
                ObjectImage = AnimationCrouch[Counter];
            }
            else
            {
                if (Counter >= AnimationStand.Count)
                    Counter = 0;
                ObjectImage = AnimationStand[Counter];
            }
        }
        public virtual void Action()
        {

        }
        public Ground OnGround()
        {
            List<GameObjects> objects = HittingObjects();
            foreach (GameObjects ob in objects)
            {
                if (ob.GetType() == typeof(Ground) && IsOnTop(ob))
                {
                    return (Ground)ob;
                }
            }
            return null;
        }
        public override void Display()
        {
            base.Display();
            if(!IsDestroy)
            {
                ObjectFallDown();
                KeepInBorder();
                if (Hittable)
                {
                    List<GameObjects> objects = HittingObjects();
                    foreach (GameObjects ob in objects)
                    {
                        if (ob.GetType() == typeof(Ground) && IsOnTop(ob))
                        {
                            KeepOnOtherTop(ob);
                        }
                        else if (ob.GetType() == typeof(Obstacle))
                        {
                            //The player hit the obstacle
                            ob.Speed = 0;
                            this.ObjectGravity.Force = 0;
                            this.ObjectGravity.Speed = 0;
                            this.IsDestroy = true;
                            break;
                        }
                    }
                }
            }
        }
    }
}