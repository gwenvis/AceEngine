using System.Collections.Generic;

namespace objection
{
    public class Animation
    {
        public List<Sprite> Sprites { get; private set; }
        private int frameTime;
        public int CurrentFrame { get; private set; }
        public int CurrentFrameTime { get; private set; }

        public Animation(IEnumerable<Sprite> sprites, int frameTime = 2)
        {
            Sprites = new List<Sprite>();
            
            foreach (var sprite in sprites)
            {
                Sprites.Add(sprite);
            }

            this.frameTime = frameTime;
            CurrentFrame = 0;
            CurrentFrameTime = 0;
        }

        public void Advance()
        {
            CurrentFrameTime++;
            if (CurrentFrameTime != frameTime) return;
            
            CurrentFrameTime = 0;
            CurrentFrame++;
            if (CurrentFrame >= Sprites.Count)
                CurrentFrame = 0;
        }
    }
}