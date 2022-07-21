
namespace KhungLongChayBo
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.gameImageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // gameImageList
            // 
            this.gameImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("gameImageList.ImageStream")));
            this.gameImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.gameImageList.Images.SetKeyName(0, "greenDino.png");
            this.gameImageList.Images.SetKeyName(1, "bigHeadDino.png");
            this.gameImageList.Images.SetKeyName(2, "communistDino.png");
            this.gameImageList.Images.SetKeyName(3, "disableDino.png");
            this.gameImageList.Images.SetKeyName(4, "futureDino.png");
            this.gameImageList.Images.SetKeyName(5, "T_PoseDino.png");
            this.gameImageList.Images.SetKeyName(6, "TheHoodDino.png");
            this.gameImageList.Images.SetKeyName(7, "VietCongDino.png");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 407);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ImageList gameImageList;
    }
}

