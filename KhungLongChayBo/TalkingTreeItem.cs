using System;
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

        public override void Effect(GreenDino dino)
        {
            ChangeToTalkingTree(dino);
        }
        private void ChangeToTalkingTree(GreenDino dino)
        {
            GameScreen.AddedItemCollector.Add(CreateTalkingTreeDino(dino));
        }
        private TalkingTreeDino CreateTalkingTreeDino(GreenDino dino)
        {
            if (dino.Crouch > 0)
            {
                dino.ObjectShape = new Rectangle(dino.ObjectShape.X, dino.ObjectShape.Y,
                   dino.ObjectShape.Width, dino.ObjectShape.Height + dino.Crouch);
            }
            TalkingTreeDino talkingTreeDino = new TalkingTreeDino(dino.ObjectShape,
                dino.ObjectGravity.Force, GameScreen);
            return talkingTreeDino;
        }
    }
}
