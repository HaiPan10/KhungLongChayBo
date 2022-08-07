﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhungLongChayBo
{
    class TalkingTreeItem : Item
    {
        private static Image talkingTreeImage = Image.FromFile(Application.StartupPath +
            @"\Dino Run\Items\Talking Tree.png");
        public TalkingTreeItem(Rectangle objectShape, int gravityForce, GameScreen gameScreen) : 
            base(objectShape, gravityForce, gameScreen)
        {
            ObjectImage = talkingTreeImage;
        }

        public TalkingTreeItem(int x, int y, int width, int height, int gravityForce, GameScreen gameScreen) :
            base(x, y, width, height, gravityForce, gameScreen)
        {
            ObjectImage = talkingTreeImage;
        }

        public override void Effect(Player dino)
        {
            GameScreen.DeletedItemCollector.Add(dino);
            ChangeToTalkingTree(dino);
        }
        private void ChangeToTalkingTree(Player dino)
        {
            GameScreen.AddedItemCollector.Add(CreateTalkingTreeDino(dino));
        }
        private TalkingTreeDino CreateTalkingTreeDino(Player dino)
        {
            int width = dino.ObjectShape.Width;
            int height = dino.ObjectShape.Height;
            int x = dino.ObjectShape.X;
            int y = dino.ObjectShape.Y;
            TalkingTreeDino talkingTreeDino = new TalkingTreeDino(new Rectangle(x,y,width,height),
                dino.ObjectGravity.Force, GameScreen);
            talkingTreeDino.InitAnimation();
            return talkingTreeDino;
        }
    }
}
