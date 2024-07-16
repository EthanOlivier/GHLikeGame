using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace GHLikeGame;
public interface IFileReading
{
    (int, int, List<Keys>) ReadFileContents(StreamReader reader);
}
