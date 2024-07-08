using System;
using System.Windows.Forms;

namespace GHLikeGame
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //IFileRead _fileRead = new FileRead();
            //string[] fileContents = _fileRead.ReadFileContents();
            //INotesDisplay _notesDisplay = new NotesDisplay(fileContents);
            //_notesDisplay.StartDisplayingNotes();

            Application.Run(new FormSetup());
        }
    }
}
