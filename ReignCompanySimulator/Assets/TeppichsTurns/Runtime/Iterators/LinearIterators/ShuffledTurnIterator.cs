using System.Collections.Generic;
using System.Linq;
using TeppichsTools.Data;
using TeppichsTurns.Actors;

namespace TeppichsTurns.Iterators
{
    public sealed class ShuffledTurnIterator : SimpleTurnIterator<IActor>
    {
        public ShuffledTurnIterator(List<IActor> actors) : base(actors) { }

        protected override void StartTurn()
        {
            currentTurn = actors.ToList();
            currentTurn.Shuffle();
        }
    }
}