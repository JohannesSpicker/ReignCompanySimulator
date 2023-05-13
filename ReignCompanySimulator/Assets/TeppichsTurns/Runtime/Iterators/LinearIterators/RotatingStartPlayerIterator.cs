using System.Collections.Generic;
using TeppichsTurns.Actors;

namespace TeppichsTurns.Iterators
{
    public sealed class RotatingStartPlayerIterator : StartPlayerIterator
    {
        public RotatingStartPlayerIterator(List<IActor> actors) : base(actors) { }

        protected override void StartTurn()
        {
            startingPlayerIndex++;
            startingPlayerIndex %= actors.Count;

            base.StartTurn();
        }
    }
}