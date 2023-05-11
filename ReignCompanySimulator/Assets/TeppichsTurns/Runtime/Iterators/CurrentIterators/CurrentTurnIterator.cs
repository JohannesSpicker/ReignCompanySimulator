using System.Collections.Generic;
using System.Linq;
using TeppichsTurns.Actors;

namespace TeppichsTurns.Iterators
{
    public abstract class CurrentTurnIterator : TurnIterator
    {
        protected List<IActor> currentTurn;

        protected virtual void StartTurn() => currentTurn = actors.ToList();
    }
}