using System;
using System.IO;
using System.Windows.Forms;
using System.Media;
using System.Runtime.CompilerServices;
using GHLikeGame.Properties;
using System.Text;

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

            SoundPlayer song = new SoundPlayer(Resources.BOFSong);

            byte[] byteArray = Encoding.UTF8.GetBytes(Resources.SongNotes);
            StreamReader reader = new StreamReader(new MemoryStream(byteArray));

            Application.Run(new Main(fileReading, reader, song));
        }
    }
}
