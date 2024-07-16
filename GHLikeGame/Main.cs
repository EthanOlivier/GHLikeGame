using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.Linq;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace GHLikeGame;

public partial class Main : Form
{
    Dictionary<Keys, (List<PictureBox>, PictureBox)> ActionKeys;

    List<PictureBox> ActiveNotes = new List<PictureBox>();
    List<PictureBox> DNotes = new List<PictureBox>();
    List<PictureBox> FNotes = new List<PictureBox>();
    List<PictureBox> JNotes = new List<PictureBox>();
    List<PictureBox> KNotes = new List<PictureBox>();

    int Hits = 0;
    int PossibleHits = 0;
    int NoteSpeed = 0;

    const string FILE_PATH = "C:\\Users\\Ethan\\source\\repos\\GHLikeGame\\GHLikeGame\\Resources\\SongNotes.txt";
    IFileReading _fileReading = new FileReading();
    StreamReader reader = new StreamReader(FILE_PATH);

    // KeysHeld includes all of the keys
    // that are actively being held down
    // in order to prevent OnKeyDown events happening
    // multiple times while the key is being held
    HashSet<Keys> KeysBeingHeld = new HashSet<Keys>();


    public Main()
    {
        InitializeComponent();
    }

    private void Form_Load(object sender, EventArgs e)
    {
        // Used to only set the size of the border around the Gameplay Background once
        typeof(Panel).InvokeMember("DoubleBuffered",
            System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.NonPublic,
            null, GPBackground, new object[] { true });

        GPBackground.Parent = Background;
        GPBackground.BackColor = Color.FromArgb(100, 13, 50, 81);
        lblPercent.Parent = Background;
        lblPercent.BackColor = Color.FromArgb(240, 13, 50, 81);
        lblScore.Parent = Background;
        lblScore.BackColor = Color.FromArgb(240, 13, 50, 81);

        SoundPlayer song = new SoundPlayer(@"C:\Users\Ethan\source\repos\GHTest\GHTest\Resources\tomp3.cc - Goukisan  Betrayal of Fear.wav");
        song.Play();

        ActionKeys = new Dictionary<Keys, (List<PictureBox>, PictureBox)>
        {
            { Keys.D, (DNotes, DKey) },
            { Keys.F, (FNotes, FKey) },
            { Keys.J, (JNotes, JKey) },
            { Keys.K, (KNotes, KKey) },
        };

        tmrBetweenNotes.Start();
        tmrDropNotes.Start();
    }

    private void FileReader(object sender, EventArgs e)
    {
        int interval = 0;
        int noteSpeed = 0;
        List<Keys> notes = new List<Keys>();

        if (!reader.EndOfStream || ActiveNotes.Any())
        {
            if (!reader.EndOfStream)
            {
                (interval, noteSpeed, notes) = _fileReading.ReadFileContents(reader);

                if (interval != 0)
                {
                    tmrBetweenNotes.Interval = interval;
                }
                if (noteSpeed != 0)
                {
                    NoteSpeed = noteSpeed;
                }
                foreach (var note in notes)
                {
                    CreateNotes(note);
                }
            }
        }
        else
        {
            tmrBetweenNotes.Stop();
            tmrDropNotes.Stop();
            GPBackground.Visible = false;
            lblPercent.BackColor = Color.FromArgb(100, 13, 50, 81);
            lblPercent.Dock = DockStyle.Fill;
            lblPercent.Text = lblPercent.Text + "\n" + lblScore.Text;
        }
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
                                                      Color.Gray, 0, ButtonBorderStyle.None    // Bottom border
            );
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



    

    private void CreateNotes(Keys note)
    {
        PictureBox newNote = new PictureBox();

        newNote.Size = new Size(65, 65);
        newNote.TabStop = false;
        newNote.Visible = true;

        if (note == Keys.D)
        {
            newNote.BackColor = Color.FromArgb(61, 91, 116);
            newNote.Location = new Point(255, 0);

            DNotes.Add(newNote);
        }
        else if (note == Keys.F)
        {
            newNote.BackColor = Color.FromArgb(13, 50, 81);
            newNote.Location = new Point(330, 0);

            FNotes.Add(newNote);
        }
        else if (note == Keys.J)
        {
            newNote.BackColor = Color.FromArgb(37, 71, 98);
            newNote.Location = new Point(405, 0);
            
            JNotes.Add(newNote);
        }
        else if (note == Keys.K)
        {
            newNote.BackColor = Color.FromArgb(86, 112, 133);
            newNote.Location = new Point(480, 0);

            KNotes.Add(newNote);
        }

        ActiveNotes.Add(newNote);

        this.Controls.Add(newNote);
        newNote.BringToFront();
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
    




    private void KeysDown(object sender, KeyEventArgs e)
    {
        if (ActionKeys.ContainsKey(e.KeyCode))
        {
            if (tmrDropNotes.Enabled && !KeysBeingHeld.Contains(e.KeyCode))
            {
                KeysBeingHeld.Add(e.KeyCode);

                var notes = ActionKeys[e.KeyCode].Item1;
                var key = ActionKeys[e.KeyCode].Item2;

                key.BackColor = Color.FromArgb(
                    Convert.ToInt32(key.BackColor.R - (key.BackColor.R * 0.3)),
                    Convert.ToInt32(key.BackColor.G - (key.BackColor.G * 0.3)),
                    Convert.ToInt32(key.BackColor.B - (key.BackColor.B * 0.3))
                );

                if (notes.Any() && notes[0].Bottom > DKey.Top)
                {
                    notes[0].Visible = false;
                    ActiveNotes.Remove(notes[0]);
                    notes.Remove(notes[0]);
                    Hits++;
                }
                PossibleHits++;
                lblPercent.Text = (Convert.ToDouble(Hits) / PossibleHits * 100).ToString("N2") + "%";
                lblScore.Text = Hits.ToString() + " / " + PossibleHits.ToString();
            }
        }
    }

    private void KeysUp(object sender, KeyEventArgs e)
    {
        if (ActionKeys.ContainsKey(e.KeyCode))
        {
            KeysBeingHeld.Remove(e.KeyCode);
            var key = ActionKeys[e.KeyCode].Item2;
            key.BackColor = Color.FromArgb(
                    Convert.ToInt32(key.BackColor.R / 0.7),
                    Convert.ToInt32(key.BackColor.G / 0.7),
                    Convert.ToInt32(key.BackColor.B / 0.7)
            );
        }
    }
}
