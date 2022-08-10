﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace KhungLongChayBo
{
    class TalkingTreeDino : GreenDino
    {
        private static List<Image> talkingTreeAnimationStand = InitAnimationStand();
        private static List<Image> talkingTreeAnimationCrouch = InitAnimaionCrouch();
        private int ammo = 20; //Number of bullets used
        private int baseAmmo = 20; //Max number of bullets
        private TextBox textBoxAmmo;

        public static List<Image> TalkingTreeAnimationStand
        {
            get => talkingTreeAnimationStand;
            set => talkingTreeAnimationStand = value;
        }
        public static List<Image> TalkingTreeAnimationCrouch
        {
            get => talkingTreeAnimationCrouch;
            set => talkingTreeAnimationCrouch = value;
        }

        internal TextBox TextBoxAmmo { get => textBoxAmmo; set => textBoxAmmo = value; }
        public int BaseAmmo { get => baseAmmo; set => baseAmmo = value; }
        public int Ammo { get => ammo; set => ammo = value; }

        public TalkingTreeDino(Rectangle playerShape, int gravityFoce, GameScreen screen) : 
            base(playerShape, gravityFoce, screen)
        {
            ObjectImage = TalkingTreeAnimationStand[0];
            InitTextBoxAmmo();
        }

        public TalkingTreeDino(int x, int y, int width, int height, int gravityFoce, GameScreen screen)
            : base(x, y, width, height, gravityFoce, screen)
        {
            ObjectImage = TalkingTreeAnimationStand[0];
            InitTextBoxAmmo();
        }

        public void InitTextBoxAmmo()
        {
            int width = 100;
            int height = 50;
            int x = 0;
            int y = 0;

            TextBoxAmmo = new TextBox(x, y, width, height, 0, GameScreen);
            TextBoxAmmo.Text = String.Format("{0}/{1}", Ammo, BaseAmmo);
            TextBoxAmmo.Hittable = false;
            TextBoxAmmo.StringFormat.Alignment = StringAlignment.Center;
            TextBoxAmmo.StringFormat.LineAlignment = StringAlignment.Center;
            GameScreen.AddedItemCollector.Add(TextBoxAmmo);
        }

        private static List<Image> InitAnimationStand()
        {
            try
            {
                List<Image> animationStand = new List<Image>();
                string[] filesStand = Directory.GetFiles(Application.StartupPath +
                    @"\Dino Run\Dinos\Talking tree Dino\stand");
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

        private static List<Image> InitAnimaionCrouch()
        {
            try
            {
                List<Image> animationCrouch = new List<Image>();
                string[] filesCrouch = Directory.GetFiles(Application.StartupPath +
                    @"\Dino Run\Dinos\Talking tree Dino\crouch");
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

        public override void Action()
        {
            if(Ammo > 0)
            {
                Shooting();
            }
        }
        public void Shooting()
        {
            if(Ammo > 0)
            {
                int distance = 3;
                Bullet b = new Bullet(ObjectShape.X + ObjectShape.Width + distance,
                    ObjectShape.Y + ObjectShape.Height / 2,
                    10, 5, 0, GameScreen);
                GameScreen.AddGameObjects(b);
                textBoxAmmo.Text = String.Format("{0}/{1}", --Ammo, BaseAmmo);
            }
        }

        public override void DoAnimation()
        {
            if (OnGround() == null)
                return;
            ++Counter;
            //Do animation by changing image in list
            if (Crouch > 0 && TalkingTreeAnimationCrouch.Count > 0)
            {
                if (Counter >= TalkingTreeAnimationCrouch.Count)
                    Counter = 0;
                ObjectImage = TalkingTreeAnimationCrouch[Counter];
            }
            else
            {
                if (TalkingTreeAnimationStand.Count <= 0)
                    return;
                if (Counter >= TalkingTreeAnimationStand.Count)
                    Counter = 0;
                ObjectImage = TalkingTreeAnimationStand[Counter];
            }
        }

        public override void Display()
        {
            base.Display();
            if(Ammo <= 0)
            {
                //Delete all the thing here
                ChangeToGreenDino();
                ClearUp();
            }
        }

        public override void ClearUp()
        {
            base.ClearUp();
            GameScreen.DeletedItemCollector.Add(TextBoxAmmo);
        }
    }
}