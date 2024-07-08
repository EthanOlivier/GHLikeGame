using System.IO;

namespace GHLikeGame
{
    public class FileRead : IFileRead
    {
        public string[] ReadFileContents()
        {
            const string FILE_PATH = "C:\\Users\\Ethan\\source\\repos\\GHLikeGame\\SongNotes.txt";
            return File.ReadAllLines(FILE_PATH);
        }
    }
}
