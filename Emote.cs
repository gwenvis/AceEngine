namespace objection
{
    public class Emote
    {

        public Animation TalkAnim { get; private set; }
        public Animation BlinkAnim { get; private set; }
        public Sprite IdleAnim { get; private set; }
        public Emotes CurrentEmote { get; private set; }

        public Emote(Emotes currentEmote, Animation talkAnim)
        {
            CurrentEmote = currentEmote;
            TalkAnim = talkAnim;
        }

        public Sprite GetCurrentSprite()
        {
            return TalkAnim.Sprites[TalkAnim.CurrentFrame];
        }

        public void Advance()
        {
            TalkAnim.Advance();
        }
    }
}