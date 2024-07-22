using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.Linq;
using System.IO;

namespace GHLikeGame;

public partial class Main : Form
{
    private Dictionary<Keys, (PictureBox, Color, Point)> ActionKeys; // Should be removed after the file is allowed
                                                                     // to be able to determine the amount
                                                                     // and which keys it wants whenever it wants

    private IFileReading _fileReading;
    private StreamReader reader;
    private SoundPlayer song;

    public Main(IFileReading fileReading, StreamReader reader, SoundPlayer song)
    {
        _fileReading = fileReading;
        this.reader = reader;
        this.song = song;

        InitializeComponent();


        Dictionary<Keys, List<PictureBox>> activeNotes = new Dictionary<Keys, List<PictureBox>>()
        {
            { Keys.D, new List<PictureBox>() },
            { Keys.F, new List<PictureBox>() },
            { Keys.J, new List<PictureBox>() },
            { Keys.K, new List<PictureBox>() }
        };
        // This is done to prevent OnKeyDown events happening
        // multiple times while the key is being held
        var keysBeingHeld = new HashSet<Keys>();

        this.tmrDropNotes.Tick += (sender, e) => NotesDropper(sender, e, activeNotes);
        this.tmrBetweenNotes.Tick += (sender, e) => HandleFileCommands(sender, e, activeNotes);
        this.KeyDown += (sender, e) => KeysDown(sender, e, keysBeingHeld, activeNotes);
        this.KeyUp += (sender, e) => KeysUp(sender, e, keysBeingHeld);
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

        ActionKeys = new Dictionary<Keys, (PictureBox, Color, Point)>
        {
            { Keys.D, (DKey, Color.FromArgb(61, 91, 116), new Point(255, 0)) },
            { Keys.F, (FKey, Color.FromArgb(13, 50, 81), new Point(330, 0)) },
            { Keys.J, (JKey, Color.FromArgb(37, 71, 98), new Point(405, 0)) },
            { Keys.K, (KKey, Color.FromArgb(86, 112, 133), new Point(480, 0)) },
        };

        song.Play();

        tmrBetweenNotes.Start();
        tmrDropNotes.Start();
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



    private void HandleFileCommands(object sender, EventArgs e, Dictionary<Keys, List<PictureBox>> activeNotes)
    {
        if (!reader.EndOfStream)
        {
            int interval, noteSpeed;
            List<Keys> notes = new List<Keys>();

            (interval, noteSpeed, notes) = _fileReading.ReadFileContents(reader);

            if (interval != 0)
            {
                tmrBetweenNotes.Interval = interval;
            }

            foreach (var note in notes)
            {
                CreateNote(activeNotes[note], ActionKeys[note].Item2, ActionKeys[note].Item3, noteSpeed);
            }
        }
        else
        {
            foreach (var key in activeNotes)
            {
                if (key.Value.Count > 0)
                {
                    return;
                }
            }

            tmrBetweenNotes.Stop();
            tmrDropNotes.Stop();
            GPBackground.Visible = false;
            lblPercent.BackColor = Color.FromArgb(100, 13, 50, 81);
            lblPercent.Dock = DockStyle.Fill;
            lblPercent.Text = lblPercent.Text + "\n" + lblScore.Text;
        }
    }

    private void CreateNote(List<PictureBox> notesGroup, Color bgColor, Point location, int noteSpeed)
    {
        PictureBox newNote = new PictureBox();

        newNote.Size = new Size(65, 65);
        newNote.TabStop = false;
        newNote.Visible = true;
        newNote.BackColor = bgColor;
        newNote.Location = location;
        newNote.Tag = noteSpeed;

        notesGroup.Add(newNote);

        this.Controls.Add(newNote);
        newNote.BringToFront();
    }

    private void NotesDropper(object sender, EventArgs e, Dictionary<Keys, List<PictureBox>> activeNotes)
    {
        List<Keys> keys = new List<Keys>(activeNotes.Keys);
        for (int i = 0; i < activeNotes.Keys.Count; i++)
        {
            Keys key = keys[i];
            List<PictureBox> notes = activeNotes[key];
            for (int j = 0; j < notes.Count; j++)
            {
                var note = notes[j];
                if (note.Bottom < this.ClientSize.Height + note.Size.Height)
                {
                    note.Location = new Point(note.Location.X, note.Location.Y + Convert.ToInt32(note.Tag));
                }
                else
                {
                    CalculateScore(0, 1);
                    note.Visible = false;
                    activeNotes[key].Remove(note);
                }
            }
        }
    }
    


    private void CalculateScore(int amountAddedToHits, int amountAddedToPossibleHits)
    {
        string scoreText = lblScore.Text;
        int dividerLocation = scoreText.IndexOf("/");

        int hits = Convert.ToInt32(scoreText.Substring(0, dividerLocation - 1));
        int possibleHits = Convert.ToInt32(lblScore.Text.Substring(dividerLocation + 1));

        hits += amountAddedToHits;
        possibleHits += amountAddedToPossibleHits;

        lblPercent.Text = (Convert.ToDouble(hits) / possibleHits * 100).ToString("N2") + "%";
        lblScore.Text = hits.ToString() + " / " + possibleHits.ToString();
    }



    private void KeysDown(object sender, KeyEventArgs e, HashSet<Keys> keysBeingHeld, Dictionary<Keys, List<PictureBox>> activeNotes)
    {
        if (ActionKeys.ContainsKey(e.KeyCode))
        {
            if (tmrDropNotes.Enabled && !keysBeingHeld.Contains(e.KeyCode))
            {
                keysBeingHeld.Add(e.KeyCode);

                var notes = activeNotes[e.KeyCode];
                var key = ActionKeys[e.KeyCode].Item1;

                key.BackColor = Color.FromArgb(
                    Convert.ToInt32(key.BackColor.R - (key.BackColor.R * 0.3)),
                    Convert.ToInt32(key.BackColor.G - (key.BackColor.G * 0.3)),
                    Convert.ToInt32(key.BackColor.B - (key.BackColor.B * 0.3))
                );

                if (notes.Any() && notes[0].Bottom > key.Top)
                {
                    notes[0].Visible = false;
                    activeNotes[e.KeyCode].Remove(notes[0]);
                    CalculateScore(1, 1);
                }
                else
                {
                    CalculateScore(0, 1);
                }
            }
        }
    }

    private void KeysUp(object sender, KeyEventArgs e, HashSet<Keys> keysBeingHeld)
    {
        if (ActionKeys.ContainsKey(e.KeyCode))
        {
            keysBeingHeld.Remove(e.KeyCode);
            var key = ActionKeys[e.KeyCode].Item1;
            key.BackColor = Color.FromArgb(
                    Convert.ToInt32(key.BackColor.R / 0.7),
                    Convert.ToInt32(key.BackColor.G / 0.7),
                    Convert.ToInt32(key.BackColor.B / 0.7)
            );
        }
    }
}
