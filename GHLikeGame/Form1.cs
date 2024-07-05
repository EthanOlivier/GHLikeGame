using System;
using System.Drawing;
using System.Windows.Forms;

namespace GHLikeGame
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, GPBackground, new object[] { true });

        }

        private void FormLoad(object sender, EventArgs e)
        {
            GPBackground.Parent = Background;
            GPBackground.BackColor = Color.FromArgb(100, 13, 50, 81);

        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {

        }

        private void GPBackground_Paint(object sender, PaintEventArgs e)
        {
            // Loop is used to increase the size and thickness of the border
            for (int i = 0; i < 25; i++)
            {
                Rectangle rect = new Rectangle(i, i, GPBackground.Width - (2 * i), GPBackground.Height - (2 * i));
                ControlPaint.DrawBorder(e.Graphics, rect, Color.Gray, 25, ButtonBorderStyle.Solid, // Left border
                                                          Color.Gray, 0, ButtonBorderStyle.None,   // Top border
                                                          Color.Gray, 25, ButtonBorderStyle.Solid, // Right border
                                                          Color.Gray, 0, ButtonBorderStyle.None);  // Bottom border
            }
        }

        private void Keys_Paint(object sender, PaintEventArgs e)
        {
            PictureBox key = sender as PictureBox;

            Pen pen = new Pen(Color.White, 1);

            e.Graphics.DrawLine(pen, 0, 0, key.Width, 0);
            e.Graphics.DrawLine(pen, 0, 0, 0, key.Height);
            e.Graphics.DrawLine(pen, key.Width - 2, 0, key.Width - 2, key.Height);

            pen.Dispose();
        }

    }
}
