using GHLikeGame;
using System;
using System.Windows.Forms;

public class NotesDisplay : INotesDisplay
{
    private Timer timerBetweenNotes;
    private readonly INotesDrop _notesDrop;
    private string[] fileContents;
    private int line = 0;

    public NotesDisplay(string[] fileContents)
    {
        this.fileContents = fileContents;
        //_notesDrop = new FormSetup();
    }
    public void StartDisplayingNotes()
    {
        timerBetweenNotes = new Timer();
        timerBetweenNotes.Interval = 500;

        timerBetweenNotes.Tick += DisplayNotes;
        timerBetweenNotes.Start();
    }
    private void DisplayNotes(object sender, EventArgs e)
    {
        if (fileContents[line].Contains("f"))
        {
            _notesDrop.StartNotesFalling("F");
        }
        if (fileContents[line].Contains("j"))
        {
            _notesDrop.StartNotesFalling("J");
        }

        if (line < fileContents.Length - 1)
        {
            line++;
        }
        else
        {
            timerBetweenNotes.Stop();
        }
    }
}