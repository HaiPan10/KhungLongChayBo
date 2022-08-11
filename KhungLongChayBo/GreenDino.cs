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
    class GreenDino : GameObjects
    {
        private int jumpingHeight = 45;
        private int crouch = 0; //Player alway stand
        private int counter = 0; //use for change animation
        private Timer time;
        private static List<Image> greenDinoAnimationStand = InitAnimationStand();
        private static List<Image> greenDinoAnimationCrouch = InitAnimationCrouch();
        public int JumpingHeight { get => jumpingHeight; set => jumpingHeight = value; }
        public int Crouch { get => crouch; set => crouch = value; }
        public int Counter { get => counter; set => counter = value; }
        private Timer Time { get => time; set => time = value; }
        public static List<Image> GreenDinoAnimationStand 
        { 
            get => greenDinoAnimationStand; 
        }
        public static List<Image> GreenDinoAnimationCrouch 
        { 
            get => greenDinoAnimationCrouch; 
        }

        public GreenDino(Rectangle playerShape, int gravityFoce, GameScreen screen)
            : base(playerShape, gravityFoce, screen)
        {
            InitClock();
            ObjectImage = GreenDinoAnimationStand[0];
        }
        public GreenDino(int x, int y, int width, int height, int gravityFoce, GameScreen screen)
            : base(x, y, width, height, gravityFoce, screen)
        {
            InitClock();
            ObjectImage = GreenDinoAnimationStand[0];
        }
        private void InitClock()
        {
            Time = new Timer();
            Time.Interval = 100;
            Time.Tick += Time_Tick;
            Time.Enabled = true;
        }

        private void Time_Tick(object sender, EventArgs e)
        {
            DoAnimation();
        }

        public void StopClock()
        {
            time.Stop();
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
        private static List<Image> InitAnimationStand()
        {
            try
            {
                List<Image> animationStand = new List<Image>();
                string[] filesStand = Directory.GetFiles(Application.StartupPath +
                    @"\Dino Run\Dinos\Green Dino\stand");
                foreach (string fileName in filesStand)
                {
                    animationStand.Add(Image.FromFile(fileName));
                }
                return animationStand;
            }
            catch
            {

            }
            return null;
        }
        private static List<Image> InitAnimationCrouch()
        {
            try
            {
                List<Image> animationCrouch = new List<Image>();
                string[] filesCrouch = Directory.GetFiles(Application.StartupPath +
                    @"\Dino Run\Dinos\Green Dino\crouch");
                foreach (string fileName in filesCrouch)
                {
                    animationCrouch.Add(Image.FromFile(fileName));
                }
                return animationCrouch;
            }
            catch
            {

            }
            return null;
        }
        public virtual void DoAnimation()
        {
            if (OnGround() == null)
                return;
            ++Counter;
            //Do animation by changing image in list
            if (Crouch > 0 && GreenDinoAnimationCrouch.Count > 0)
            {
                if (Counter >= GreenDinoAnimationCrouch.Count)
                    Counter = 0;
                ObjectImage = GreenDinoAnimationCrouch[Counter];
            }
            else
            {
                if (GreenDinoAnimationStand.Count <= 0)
                    return;
                if (Counter >= GreenDinoAnimationStand.Count)
                    Counter = 0;
                ObjectImage = GreenDinoAnimationStand[Counter];
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
        public void UsingItem(Item item)
        {
            item.Effect(this);
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
                        //Console.WriteLine(ob);
                        if (ob.GetType() == typeof(Ground) && IsOnTop(ob))
                        {
                            KeepOnOtherTop(ob);
                        }
                        else if (ob.GetType() == typeof(Obstacle))
                        {
                            //The player hit the obstacle
                            //The game will end
                            if(ArmorItem.NumberOfDinoArmor > 0)
                            {
                                //Console.WriteLine(ArmorItem.NumberOfDinoArmor);
                                ArmorItem.DecreaseArmor(this);
                                GameScreen.DeletedItemCollector.Add(ob);
                                
                            }
                            else
                            {
                                ob.Speed = 0;
                                this.ObjectGravity.Force = 0;
                                this.ObjectGravity.Speed = 0;
                                this.IsDestroy = true;
                                StopClock();
                            }
                        }
                        else if(ob.GetType().BaseType == typeof(Item))
                        {
                            UsingItem((Item)ob);
                            GameScreen.DeletedItemCollector.Add(ob);
                            if(ob.GetType() == typeof(SoldierItem))
                            {
                                ClearUp();
                            }
                            Console.WriteLine(ArmorItem.NumberOfDinoArmor);
                        }
                    }
                }
            }
        }
        public void ChangeToGreenDino(GreenDino dino)
        {
            GameScreen.AddedItemCollector.Add(CreateGreenDino(dino));
        }
        private static GreenDino CreateGreenDino(GreenDino dino)
        {
            if (dino.Crouch > 0)
            {
                dino.ObjectShape = new Rectangle(dino.ObjectShape.X, dino.ObjectShape.Y, dino.ObjectShape.Width,
                    dino.ObjectShape.Height + dino.Crouch);
            }
            GreenDino greenDino = new GreenDino(dino.ObjectShape, dino.ObjectGravity.Force, dino.GameScreen);
            return greenDino;
           
        }

        public virtual void ClearUp()
        {
            GameScreen.DeletedItemCollector.Add(this);
            StopClock();
        }
    }
}