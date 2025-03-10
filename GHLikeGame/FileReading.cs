﻿using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace GHLikeGame;

public class FileReading : IFileReading
{
    private int CurrentNoteSpeed = 8;
    public (int, int, List<Keys>) ReadFileContents(StreamReader reader)
    {
        string lineContents = reader.ReadLine();

        int interval = 0, noteSpeed;
        List<Keys> notes = new List<Keys>();

        if (lineContents.Contains("("))
        {
            int startIndex = lineContents.IndexOf("(");
            int endIndex = lineContents.IndexOf(")", startIndex);
            interval = Convert.ToInt32(lineContents.Substring(startIndex + 1, endIndex - startIndex - 1));
        }

        if (lineContents.Contains("/"))
        {
            int startIndex = lineContents.IndexOf("/");
            int endIndex = lineContents.IndexOf(@"\", startIndex);
            CurrentNoteSpeed = Convert.ToInt32(lineContents.Substring(startIndex + 1, endIndex - startIndex - 1));
        }
        noteSpeed = CurrentNoteSpeed;

        if (lineContents.ToUpper().Contains("D"))
        {
            notes.Add(Keys.D);
        }
        if (lineContents.ToUpper().Contains("F"))
        {
            notes.Add(Keys.F);
        }
        if (lineContents.ToUpper().Contains("J"))
        {
            notes.Add(Keys.J);
        }
        if (lineContents.ToUpper().Contains("K"))
        {
            notes.Add(Keys.K);
        }

        return (interval, noteSpeed, notes);
    }
}
