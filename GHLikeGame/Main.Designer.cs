using WMPLib;
using System.Windows.Forms;
using System.Drawing;

namespace GHLikeGame
{
    partial class FormSetup
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
            this.JNote = new System.Windows.Forms.PictureBox();
            this.FNote = new System.Windows.Forms.PictureBox();
            this.JKey = new System.Windows.Forms.PictureBox();
            this.FKey = new System.Windows.Forms.PictureBox();
            this.Background = new System.Windows.Forms.PictureBox();
            this.tmrDropDown = new System.Windows.Forms.Timer(this.components);
            this.tmrBetweenNotes = new System.Windows.Forms.Timer(this.components);
            this.lblScore = new System.Windows.Forms.Label();
            this.GPBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.JNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Background)).BeginInit();
            this.SuspendLayout();
            // 
            // GPBackground
            // 
            this.GPBackground.Controls.Add(this.JNote);
            this.GPBackground.Controls.Add(this.FNote);
            this.GPBackground.Controls.Add(this.JKey);
            this.GPBackground.Controls.Add(this.FKey);
            this.GPBackground.Location = new System.Drawing.Point(200, 0);
            this.GPBackground.Name = "GPBackground";
            this.GPBackground.Size = new System.Drawing.Size(400, 450);
            this.GPBackground.TabIndex = 1;
            this.GPBackground.Paint += new System.Windows.Forms.PaintEventHandler(this.GPBackground_Paint);
            // 
            // JNote
            // 
            this.JNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(71)))), ((int)(((byte)(98)))));
            this.JNote.Location = new System.Drawing.Point(200, 0);
            this.JNote.Name = "JNote";
            this.JNote.Size = new System.Drawing.Size(100, 75);
            this.JNote.TabIndex = 3;
            this.JNote.TabStop = false;
            this.JNote.Visible = false;
            // 
            // FNote
            // 
            this.FNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(50)))), ((int)(((byte)(81)))));
            this.FNote.Location = new System.Drawing.Point(100, 0);
            this.FNote.Name = "FNote";
            this.FNote.Size = new System.Drawing.Size(100, 75);
            this.FNote.TabIndex = 2;
            this.FNote.TabStop = false;
            this.FNote.Visible = false;
            // 
            // JKey
            // 
            this.JKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(71)))), ((int)(((byte)(98)))));
            this.JKey.Image = global::GHLikeGame.Properties.Resources.J;
            this.JKey.Location = new System.Drawing.Point(200, 375);
            this.JKey.Name = "JKey";
            this.JKey.Size = new System.Drawing.Size(100, 75);
            this.JKey.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.JKey.TabIndex = 1;
            this.JKey.TabStop = false;
            this.JKey.Paint += new System.Windows.Forms.PaintEventHandler(this.Keys_Paint);
            // 
            // FKey
            // 
            this.FKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(50)))), ((int)(((byte)(81)))));
            this.FKey.Image = global::GHLikeGame.Properties.Resources.F;
            this.FKey.Location = new System.Drawing.Point(100, 375);
            this.FKey.Name = "FKey";
            this.FKey.Size = new System.Drawing.Size(100, 75);
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
            // tmrDropDown
            // 
            this.tmrDropDown.Interval = 10;
            this.tmrDropDown.Tick += new System.EventHandler(this.NotesDropper);
            // 
            // tmrBetweenNotes
            // 
            this.tmrBetweenNotes.Interval = 1500;
            this.tmrBetweenNotes.Tick += new System.EventHandler(this.DisplayNotes);
            // 
            // lblScore
            // 
            this.lblScore.BackColor = System.Drawing.Color.Transparent;
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.Location = new System.Drawing.Point(642, 195);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(100, 91);
            this.lblScore.TabIndex = 2;
            this.lblScore.Text = "0";
            this.lblScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.GPBackground);
            this.Controls.Add(this.Background);
            this.MaximizeBox = false;
            this.Name = "FormSetup";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.FormLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeysPressed);
            this.GPBackground.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.JNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Background)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private WindowsMediaPlayer Song;
        private PictureBox Background;
        private Panel GPBackground;
        private PictureBox JKey;
        private PictureBox FKey;
        private PictureBox JNote;
        private PictureBox FNote;
        private Timer tmrDropDown;
        private Timer tmrBetweenNotes;
        private Label lblScore;
    }
}

