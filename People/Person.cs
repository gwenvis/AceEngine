using System.Collections.Generic;
using System.Linq;

namespace objection
{
    public class Person
    {
        public string Name { get; internal set; }
        public List<objection.Emote> PossibleActions { get; private set; }
        public Emote CurrentEmote { get; private set; }
        
        public Person()
        {
        }

        public void SetActions(IEnumerable<Emote> actions)
        {
            PossibleActions = actions.ToList();
        }

        public void AddAction(objection.Emote emote)
        {
            PossibleActions?.Add(emote);
        }

        public void PlayAction(Emote emote)
        {
            CurrentEmote = emote;
        }

        public void Advance()
        {
            CurrentEmote.Advance();
        }
    }

    public enum Emotes
    {
        NORMAL,
        SLAM,
        SLAMMED,
        POINT,
        POINTED,
        NOD,
        SHAKE,
        PROUD,
        PAPER,
        SHOCK,
        BLUFF,
        THINK,
        SWEAT
    }
}