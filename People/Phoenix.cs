using System.Collections.Generic;

namespace objection
{
    public class Phoenix : Person
    {
        public Phoenix() : base()
        {
            Name = "phoenix";
            
            SetActions(
                new List<Emote>()
                {
                    new Emote(Emotes.NORMAL, GetNormalSprites())
                });
        }

        public Animation GetNormalSprites()
        {
            string filename = "Images/phoenix-sheet.png";
            int w = 125, h = 142, y = 204;
            
            var sprites = new[]
            {
                new Sprite(filename, 171, y, w, h),
                new Sprite(filename, 326, y, w, h)
            };

            return new Animation(sprites);
        }
    }
}