using System;
using System.IO;
using System.Windows.Forms;
using System.Media;

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

            IFileReading fileReading = new FileReading();
            const string FILE_PATH = "C:\\Users\\Ethan\\source\\repos\\GHLikeGame\\GHLikeGame\\Resources\\SongNotes.txt";
            StreamReader reader = new StreamReader(FILE_PATH);
            SoundPlayer song = new SoundPlayer(@"C:\Users\Ethan\source\repos\GHTest\GHTest\Resources\tomp3.cc - Goukisan  Betrayal of Fear.wav");

            Application.Run(new Main(fileReading, reader, song));
        }
    }
}
