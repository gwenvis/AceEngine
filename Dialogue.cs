using System.Drawing;
using MagickSharp.Core;

namespace objection
{
    public class Dialogue
    {
        public string[] Lines { get; private set; }
        public int CurrentTextIndex { get; private set; }
        public readonly int TextWidth;
        public Person Pers { get; private set; }
        public Emote Ac { get; private set; }

        private int currentLine = 0;

        public bool endReached = false;
        
        public Dialogue(string lines, Person person, Emote emote, MagickImage img, Drawable drawable)
        {
            TextWidth = (int)new TypeMetrics(img, drawable, lines).Width;
            
            Ac = emote;
            Lines = new string[]
            {
                lines
            };
            Pers = person;
        }

        public string GetText()
        {
            int width = CurrentTextIndex < Lines[currentLine].Length ? CurrentTextIndex : Lines[currentLine].Length;
            return Lines[currentLine].Substring(0, width);
        }

        public void Advance()
        {
            CurrentTextIndex++;
            if (CurrentTextIndex == Lines[currentLine].Length + 2) endReached = true;
            Ac.Advance();
        }
    }
}