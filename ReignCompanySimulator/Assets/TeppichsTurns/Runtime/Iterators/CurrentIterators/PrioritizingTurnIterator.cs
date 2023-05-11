using Sirenix.Utilities;
using TeppichsTurns.Actors;

namespace TeppichsTurns.Iterators
{
    public abstract class PrioritizingTurnIterator : CurrentTurnIterator
    {
        public override IActor GetNextActor()
        {
            if (currentTurn.IsNullOrEmpty())
                StartTurn();

            return null;
        }

        private void Bla()
        {
            
        }
    }
}