using System.Linq;
using Sirenix.Utilities;
using TeppichsTurns.Actors;

namespace TeppichsTurns.Iterators
{
    public abstract class SimpleTurnIterator : CurrentTurnIterator
    {
        public override IActor GetNextActor()
        {
            if (currentTurn.IsNullOrEmpty())
                StartTurn();

            IActor nextActor = currentTurn.First();
            currentTurn.Remove(nextActor);

            return nextActor;
        }
    }
}