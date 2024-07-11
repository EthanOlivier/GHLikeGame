﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Media;
using System.Linq;

namespace GHLikeGame;

public partial class FormSetup : Form
{
    Dictionary<Keys, (List<PictureBox>, PictureBox)> ActionKeys;
    List<PictureBox> DNotes = new List<PictureBox>();
    List<PictureBox> FNotes = new List<PictureBox>();
    List<PictureBox> JNotes = new List<PictureBox>();
    List<PictureBox> KNotes = new List<PictureBox>();
    List<PictureBox> ActiveNotes = new List<PictureBox>();
    string[] FileContents;
    int FileLine = 0;
    int Hits = 0;
    int PossibleHits = 0;
    int NoteSpeed = 0;
    bool keyDown = false;
    HashSet<Keys> KeysDown = new HashSet<Keys>();


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





    public void ReadFileContents()
    {
        const string FILE_PATH = "C:\\Users\\Ethan\\source\\repos\\GHLikeGame\\GHLikeGame\\Resources\\SongNotes.txt";
        FileContents = File.ReadAllLines(FILE_PATH);
    }



    private void ReadFileLine(object sender, EventArgs e)
    {
        if (FileLine < FileContents.Length)
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
            if (FileContents[FileLine].ToUpper().Contains("D"))
            {
                StartNotesFalling("D");
            }
            if (FileContents[FileLine].ToUpper().Contains("F"))
            {
                StartNotesFalling("F");
            }
            if (FileContents[FileLine].ToUpper().Contains("J"))
            {
                StartNotesFalling("J");
            }
            if (FileContents[FileLine].ToUpper().Contains("K"))
            {
                StartNotesFalling("K");
            }
            FileLine++;
        }
        else
        {
            if (!ActiveNotes.Any())
            {
                tmrBetweenNotes.Stop();
                tmrDropDown.Stop();
                GPBackground.Visible = false;
                lblPercent.BackColor = Color.FromArgb(100, 13, 50, 81);
                lblPercent.Dock = DockStyle.Fill;
                lblPercent.Text += "\n" + lblScore.Text;
            }
        }
    }



    public void StartNotesFalling(string note)
    {
        PictureBox newNote = new PictureBox();
        newNote.Size = new Size(65, 65);
        newNote.TabStop = false;
        newNote.Visible = true;
        if (note == "D")
        {
            newNote.BackColor = Color.FromArgb(61, 91, 116);
            newNote.Location = new Point(255, 0);

            DNotes.Add(newNote);
        }
        else if (note == "F")
        {
            newNote.BackColor = Color.FromArgb(13, 50, 81);
            newNote.Location = new Point(330, 0);

            FNotes.Add(newNote);
        }
        else if (note == "J")
        {
            newNote.BackColor = Color.FromArgb(37, 71, 98);
            newNote.Location = new Point(405, 0);
            
            JNotes.Add(newNote);
        }
        else if (note == "K")
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
    

    private void KeysPressed(object sender, KeyEventArgs e)
    {
        if (tmrDropDown.Enabled && !KeysDown.Contains(e.KeyCode))
        {
            KeysDown.Add(e.KeyCode);
            if (ActionKeys.ContainsKey(e.KeyCode))
            {
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

    private void KeysReleased(object sender, KeyEventArgs e)
    {
        if (ActionKeys.ContainsKey(e.KeyCode))
        {
            KeysDown.Remove(e.KeyCode);
            var key = ActionKeys[e.KeyCode].Item2;
            key.BackColor = Color.FromArgb(
                    Convert.ToInt32(key.BackColor.R / 0.7),
                    Convert.ToInt32(key.BackColor.G / 0.7),
                    Convert.ToInt32(key.BackColor.B / 0.7)
            );
        }
    }
}
