using System.Collections.Generic;
using TeppichsTurns.Actors;

namespace TeppichsTurns.Iterators
{
    public class StartPlayerIterator : SimpleTurnIterator
    {
        protected int startingPlayerIndex = -1;

        protected override void StartTurn()
        {
            currentTurn = new List<IActor>();

            for (int i = startingPlayerIndex; i < actors.Count; i++)
                currentTurn.Add(actors[i]);

            for (int i = 0; i < startingPlayerIndex; i++)
                currentTurn.Add(actors[i]);
        }

        public void SetStartPlayer(IActor actor)
        {
            if (actors.Contains(actor))
                startingPlayerIndex = actors.IndexOf(actor);
        }
    }
}