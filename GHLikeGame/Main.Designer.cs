using WMPLib;
using System.Windows.Forms;
using System.Drawing;

namespace GHLikeGame
{
    partial class Main
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
            this.GPBackground = new System.Windows.Forms.Panel();
            this.KKey = new System.Windows.Forms.PictureBox();
            this.DKey = new System.Windows.Forms.PictureBox();
            this.JKey = new System.Windows.Forms.PictureBox();
            this.FKey = new System.Windows.Forms.PictureBox();
            this.Background = new System.Windows.Forms.PictureBox();
            this.lblPercent = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.tmrDropNotes = new System.Windows.Forms.Timer(this.components);
            this.tmrBetweenNotes = new System.Windows.Forms.Timer(this.components);
            this.GPBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Background)).BeginInit();
            this.SuspendLayout();
            // 
            // GPBackground
            // 
            this.GPBackground.Controls.Add(this.KKey);
            this.GPBackground.Controls.Add(this.DKey);
            this.GPBackground.Controls.Add(this.JKey);
            this.GPBackground.Controls.Add(this.FKey);
            this.GPBackground.Location = new System.Drawing.Point(150, 0);
            this.GPBackground.Name = "GPBackground";
            this.GPBackground.Size = new System.Drawing.Size(500, 450);
            this.GPBackground.TabIndex = 1;
            this.GPBackground.Paint += new System.Windows.Forms.PaintEventHandler(this.GPBackground_Paint);
            // 
            // KKey
            // 
            this.KKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(112)))), ((int)(((byte)(133)))));
            this.KKey.Image = global::GHLikeGame.Properties.Resources.K;
            this.KKey.Location = new System.Drawing.Point(325, 375);
            this.KKey.Name = "KKey";
            this.KKey.Size = new System.Drawing.Size(75, 75);
            this.KKey.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.KKey.TabIndex = 5;
            this.KKey.TabStop = false;
            this.KKey.Paint += new System.Windows.Forms.PaintEventHandler(this.Keys_Paint);
            // 
            // DKey
            // 
            this.DKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(91)))), ((int)(((byte)(116)))));
            this.DKey.Image = global::GHLikeGame.Properties.Resources.D;
            this.DKey.Location = new System.Drawing.Point(100, 375);
            this.DKey.Name = "DKey";
            this.DKey.Size = new System.Drawing.Size(75, 75);
            this.DKey.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.DKey.TabIndex = 4;
            this.DKey.TabStop = false;
            this.DKey.Paint += new System.Windows.Forms.PaintEventHandler(this.Keys_Paint);
            // 
            // JKey
            // 
            this.JKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(71)))), ((int)(((byte)(98)))));
            this.JKey.Image = global::GHLikeGame.Properties.Resources.J;
            this.JKey.Location = new System.Drawing.Point(250, 375);
            this.JKey.Name = "JKey";
            this.JKey.Size = new System.Drawing.Size(75, 75);
            this.JKey.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.JKey.TabIndex = 1;
            this.JKey.TabStop = false;
            this.JKey.Paint += new System.Windows.Forms.PaintEventHandler(this.Keys_Paint);
            // 
            // FKey
            // 
            this.FKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(50)))), ((int)(((byte)(81)))));
            this.FKey.Image = global::GHLikeGame.Properties.Resources.F;
            this.FKey.Location = new System.Drawing.Point(175, 375);
            this.FKey.Name = "FKey";
            this.FKey.Size = new System.Drawing.Size(75, 75);
            this.FKey.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.FKey.TabIndex = 0;
            this.FKey.TabStop = false;
            this.FKey.Paint += new System.Windows.Forms.PaintEventHandler(this.Keys_Paint);
            // 
            // Background
            // 
            this.Background.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Background.Image = global::GHLikeGame.Properties.Resources.youtube_video_gif;
            this.Background.Location = new System.Drawing.Point(0, 0);
            this.Background.Name = "Background";
            this.Background.Size = new System.Drawing.Size(800, 450);
            this.Background.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Background.TabIndex = 0;
            this.Background.TabStop = false;
            // 
            // lblPercent
            // 
            this.lblPercent.BackColor = System.Drawing.Color.Transparent;
            this.lblPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPercent.Location = new System.Drawing.Point(650, 175);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(150, 100);
            this.lblPercent.TabIndex = 2;
            this.lblPercent.Text = "100.00%";
            this.lblPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblScore
            // 
            this.lblScore.BackColor = System.Drawing.Color.Transparent;
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.Location = new System.Drawing.Point(668, 280);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(120, 80);
            this.lblScore.TabIndex = 6;
            this.lblScore.Text = "0 / 0";
            this.lblScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrDropNotes
            // 
            this.tmrDropNotes.Interval = 10;
            // 
            // tmrBetweenNotes
            // 
            this.tmrBetweenNotes.Interval = 500;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.GPBackground);
            this.Controls.Add(this.Background);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Form_Load);
            this.GPBackground.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.KKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Background)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox Background;
        private Panel GPBackground;
        private PictureBox JKey;
        private PictureBox FKey;
        private Timer tmrDropNotes;
        private Timer tmrBetweenNotes;
        private Label lblPercent;
        private PictureBox DKey;
        private PictureBox KKey;
        private Label lblScore;
    }
}

