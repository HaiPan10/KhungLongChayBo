
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
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 20;
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
            this.gameImageList.Images.SetKeyName(8, "Background.png");
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(368, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 71);
            this.button1.TabIndex = 0;
            this.button1.Text = "Play";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 362);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Dino Run";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ImageList gameImageList;
        private System.Windows.Forms.Button button1;
    }
}

