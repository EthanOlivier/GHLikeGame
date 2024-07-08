using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace GHLikeGame;

public partial class FormSetup : Form
{
    List<PictureBox> Notes = new List<PictureBox>();
    string[] FileContents;
    int FileLine = 0;
    public FormSetup()
    {
        InitializeComponent();

        // Used to only set the size of the border around the Gameplay Background once
        typeof(Panel).InvokeMember("DoubleBuffered",
            System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.NonPublic,
            null, GPBackground, new object[] { true });

        tmrDropDown.Start();

        ReadFileContents();
        tmrBetweenNotes.Start();
    }

    private void FormLoad(object sender, EventArgs e)
    {
        GPBackground.Parent = Background;
        GPBackground.BackColor = Color.FromArgb(100, 13, 50, 81);
        lblScore.Parent = Background;
        lblScore.BackColor = Color.FromArgb(240, 13, 50, 81);
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

        e.Graphics.DrawLine(pen, 0, 0, key.Width, 0); // Top border
        e.Graphics.DrawLine(pen, 0, 0, 0, key.Height); // Left border
        e.Graphics.DrawLine(pen, key.Width - 2, 0, key.Width - 2, key.Height); // Right border

        pen.Dispose();
    }

    public void StartNotesFalling(string note)
    {
        if (note == "F")
        {
            FNote.Visible = true;
            Notes.Add(FNote);
        }
        else if (note == "J")
        {
            JNote.Visible = true;
            Notes.Add(JNote);
        }
    }

    private void NotesDropper(object sender, EventArgs e)
    {
        for (int i = 0; i < Notes.Count; i++)
        {
            var note = Notes[i];
            if (note.Bottom < this.ClientSize.Height + note.Size.Height)
            {
                note.Location = new Point(note.Location.X, note.Location.Y + 8);
            }
            else
            {
                note.Visible = false;
                note.Location = new Point(note.Location.X, 0);
                Notes.Remove(note);
            }
        }
    }
    public void ReadFileContents()
    {
        const string FILE_PATH = "C:\\Users\\Ethan\\source\\repos\\GHLikeGame\\SongNotes.txt";
        FileContents = File.ReadAllLines(FILE_PATH);
    }
    private void DisplayNotes(object sender, EventArgs e)
    {
        if (FileContents[FileLine].Contains("f"))
        {
            StartNotesFalling("F");
        }
        if (FileContents[FileLine].Contains("j"))
        {
            StartNotesFalling("J");
        }

        if (FileLine < FileContents.Length - 1)
        {
            FileLine++;
        }
        else
        {
            tmrBetweenNotes.Stop();
        }
    }

    private void KeysPressed(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F)
        {
            if (FNote.Bottom > FKey.Top)
            {
                FNote.Visible = false;
                int score = Convert.ToInt32(lblScore.Text);
                score++;
                lblScore.Text = score.ToString();
            }
        }
        else if (e.KeyCode == Keys.J)
        {
            if (JNote.Bottom > JKey.Top)
            {
                JNote.Visible = false;
                int score = Convert.ToInt32(lblScore.Text);
                score++;
                lblScore.Text = score.ToString();
            }
        }
    }
}
