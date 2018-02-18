using MagickSharp.Core;

namespace objection
{
    public class Sprite
    {
        public string FileName { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Sprite(string fileName, int x = 0, int y = 0, int w = 0, int h = 0)
        {
            FileName = fileName;
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }

        /// <summary>
        /// Gets the sprite together with ihtiwht iwhtoa hwioth aiothaoi hoigdhaiofg 
        /// </summary>
        public MagickImage GetSprite()
        {
            // TODO: optimize this so that it doesn't have to open the file everytime.
            MagickImage img = new MagickImage(FileName);
            
            if ((X + Y + Width + Height) == 0) return img;
            
            img.Crop(X, Y, Width, Height);
            return img;
        }
    }
}