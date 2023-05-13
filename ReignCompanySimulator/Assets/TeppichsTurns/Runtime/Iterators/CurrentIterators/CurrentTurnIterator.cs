using System.Collections.Generic;
using System.Linq;
using TeppichsTurns.Actors;

namespace TeppichsTurns.Iterators
{
    public abstract class CurrentTurnIterator<T> : TurnIterator<T> where T : IActor
    {
        protected List<T> currentTurn;
        protected CurrentTurnIterator(List<T> actors) : base(actors) { }

        protected virtual void StartTurn() => currentTurn = actors.ToList();
    }
}