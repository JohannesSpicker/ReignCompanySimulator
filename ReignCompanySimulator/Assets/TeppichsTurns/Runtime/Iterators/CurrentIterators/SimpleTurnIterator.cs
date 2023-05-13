using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using TeppichsTurns.Actors;

namespace TeppichsTurns.Iterators
{
    public abstract class SimpleTurnIterator<T> : CurrentTurnIterator<T> where T : IActor
    {
        protected SimpleTurnIterator(List<T> actors) : base(actors) { }

        public override T GetNextActor()
        {
            if (currentTurn.IsNullOrEmpty())
                StartTurn();

            T nextActor = currentTurn.First();
            currentTurn.Remove(nextActor);

            return nextActor;
        }
    }
}