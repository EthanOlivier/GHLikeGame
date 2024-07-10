using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Media;
using System.Linq;

namespace GHLikeGame;

public partial class FormSetup : Form
{
    List<PictureBox> ActiveNotes = new List<PictureBox>();
    List<PictureBox> DNotes = new List<PictureBox>();
    List<PictureBox> FNotes = new List<PictureBox>();
    List<PictureBox> JNotes = new List<PictureBox>();
    List<PictureBox> KNotes = new List<PictureBox>();
    string[] FileContents;
    int FileLine = 0;
    int Hits = 0;
    int PossibleHits = 0;
    int NoteSpeed = 0;
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
        lblPercent.Parent = Background;
        lblPercent.BackColor = Color.FromArgb(240, 13, 50, 81);
        lblScore.Parent = Background;
        lblScore.BackColor = Color.FromArgb(240, 13, 50, 81);

        SoundPlayer song = new SoundPlayer(@"C:\Users\Ethan\source\repos\GHTest\GHTest\Resources\tomp3.cc - Goukisan  Betrayal of Fear.wav");
        song.Play();
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
        if (note == "D")
        {
            PictureBox newNote = new PictureBox();

            newNote.BackColor = Color.FromArgb(61, 91, 116);
            newNote.Location = new Point(255, 0);
            newNote.Size = new Size(65, 65);
            newNote.TabIndex = 6;
            newNote.TabStop = false;
            newNote.Visible = true;

            DNotes.Add(newNote);
            ActiveNotes.Add(newNote);

            this.Controls.Add(newNote);
            newNote.BringToFront();
        }
        else if (note == "F")
        {
            PictureBox newNote = new PictureBox();

            newNote.BackColor = Color.FromArgb(13, 50, 81);
            newNote.Location = new Point(330, 0);
            newNote.Size = new Size(65, 65);
            newNote.TabIndex = 2;
            newNote.TabStop = false;
            newNote.Visible = true;

            FNotes.Add(newNote);
            ActiveNotes.Add(newNote);

            this.Controls.Add(newNote);
            newNote.BringToFront();
        }
        else if (note == "J")
        {
            PictureBox newNote = new PictureBox();

            newNote.BackColor = Color.FromArgb(37, 71, 98);
            newNote.Location = new Point(405, 0);
            newNote.Size = new Size(65, 65);
            newNote.TabIndex = 3;
            newNote.TabStop = false;
            newNote.Visible = true;
            
            JNotes.Add(newNote);
            ActiveNotes.Add(newNote);

            this.Controls.Add(newNote);
            newNote.BringToFront();
        }
        else if (note == "K")
        {
            PictureBox newNote = new PictureBox();

            newNote.BackColor = Color.FromArgb(86, 112, 133);
            newNote.Location = new Point(480, 0);
            newNote.Size = new Size(65, 65);
            newNote.TabIndex = 7;
            newNote.TabStop = false;
            newNote.Visible = true;

            KNotes.Add(newNote);
            ActiveNotes.Add(newNote);

            this.Controls.Add(newNote);
            newNote.BringToFront();
        }
    }

    private void NotesDropper(object sender, EventArgs e)
    {
        for (int i = 0; i < ActiveNotes.Count; i++)
        {
            var note = ActiveNotes[i];
            if (note.Bottom < this.ClientSize.Height + note.Size.Height)
            {
                note.Location = new Point(note.Location.X, note.Location.Y + NoteSpeed);
            }
            else
            {
                PossibleHits++;
                lblPercent.Text = (Convert.ToDouble(Hits) / PossibleHits * 100).ToString("N2") + "%";
                lblScore.Text = Hits.ToString() + " / " + PossibleHits.ToString();
                note.Visible = false;
                note.Location = new Point(note.Location.X, 0);
                ActiveNotes.Remove(note);

                if (DNotes.Contains(note))
                {
                    DNotes.Remove(note);
                }
                else if (FNotes.Contains(note))
                {
                    FNotes.Remove(note);
                }
                else if(JNotes.Contains(note))
                {
                    JNotes.Remove(note);
                }
                else if(KNotes.Contains(note))
                {
                    KNotes.Remove(note);
                }
            }
        }
    }
    public void ReadFileContents()
    {
        const string FILE_PATH = "C:\\Users\\Ethan\\source\\repos\\GHLikeGame\\GHLikeGame\\Resources\\SongNotes.txt";
        FileContents = File.ReadAllLines(FILE_PATH);
    }
    private void ReadFileLine(object sender, EventArgs e)
    {
        if (FileContents[FileLine].Contains("("))
        {
            int startIndex = FileContents[FileLine].IndexOf("(");
            int endIndex = FileContents[FileLine].IndexOf(")", startIndex);
            int interval = Convert.ToInt32(FileContents[FileLine].Substring(startIndex + 1, endIndex - startIndex - 1));
            tmrBetweenNotes.Stop();
            tmrBetweenNotes.Interval = interval;
            tmrBetweenNotes.Start();
        }
        if (FileContents[FileLine].Contains("/"))
        {
            int startIndex = FileContents[FileLine].IndexOf("/");
            int endIndex = FileContents[FileLine].IndexOf(@"\", startIndex);
            NoteSpeed = Convert.ToInt32(FileContents[FileLine].Substring(startIndex + 1, endIndex - startIndex - 1));
        }
        if (FileContents[FileLine].Contains("d"))
        {
            StartNotesFalling("D");
        }
        if (FileContents[FileLine].Contains("f"))
        {
            StartNotesFalling("F");
        }
        if (FileContents[FileLine].Contains("j"))
        {
            StartNotesFalling("J");
        }
        if (FileContents[FileLine].Contains("k"))
        {
            StartNotesFalling("K");
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
        if (e.KeyCode == Keys.D)
        {
            DKey.BackColor = Color.FromArgb(43, 64, 81);
            if (DNotes.Any() && DNotes[0].Bottom > DKey.Top)
            {
                DNotes[0].Visible = false;
                ActiveNotes.Remove(DNotes[0]);
                DNotes.Remove(DNotes[0]);
                PossibleHits++;
                Hits++;
                lblPercent.Text = (Convert.ToDouble(Hits) / PossibleHits * 100).ToString("N2") + "%";
                lblScore.Text = Hits.ToString() + " / " + PossibleHits.ToString();
            }
        }
        else if (e.KeyCode == Keys.F)
        {
            FKey.BackColor = Color.FromArgb(26, 39, 60);
            if (FNotes.Any() && FNotes[0].Bottom > FKey.Top)
            {
                FNotes[0].Visible = false;
                ActiveNotes.Remove(FNotes[0]);
                FNotes.Remove(FNotes[0]);
                PossibleHits++;
                Hits++;
                lblPercent.Text = (Convert.ToDouble(Hits) / PossibleHits * 100).ToString("N2") + "%";
                lblScore.Text = Hits.ToString() + " / " + PossibleHits.ToString();
            }
        }
        else if (e.KeyCode == Keys.J)
        {
            JKey.BackColor = Color.FromArgb(30, 57, 78);
            if (JNotes.Any() && JNotes[0].Bottom > JKey.Top)
            {
                JNotes[0].Visible = false;
                ActiveNotes.Remove(JNotes[0]);
                JNotes.Remove(JNotes[0]);
                PossibleHits++;
                Hits++;
                lblPercent.Text = (Convert.ToDouble(Hits) / PossibleHits * 100).ToString("N2") + "%";
                lblScore.Text = Hits.ToString() + " / " + PossibleHits.ToString();
            }
        }
        else if (e.KeyCode == Keys.K)
        {
            KKey.BackColor = Color.FromArgb(60, 78, 93);
            if (KNotes.Any() && KNotes[0].Bottom > KKey.Top)
            {
                KNotes[0].Visible = false;
                ActiveNotes.Remove(KNotes[0]);
                KNotes.Remove(KNotes[0]);
                PossibleHits++;
                Hits++;
                lblPercent.Text = (Convert.ToDouble(Hits) / PossibleHits * 100).ToString("N2") + "%";
                lblScore.Text = Hits.ToString() + " / " + PossibleHits.ToString();
            }
        }
    }

    private void KeysReleased(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.D)
        {
            DKey.BackColor = Color.FromArgb(61, 91, 116);
        }
        else if (e.KeyCode == Keys.F)
        {
            FKey.BackColor = Color.FromArgb(13, 50, 81);
        }
        else if (e.KeyCode == Keys.J)
        {
            JKey.BackColor = Color.FromArgb(37, 71, 98);
        }
        else if (e.KeyCode == Keys.K)
        {
            KKey.BackColor = Color.FromArgb(86, 112, 133);
        }
    }
}
