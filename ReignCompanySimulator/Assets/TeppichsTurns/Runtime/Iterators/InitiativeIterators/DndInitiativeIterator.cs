using System.Linq;
using Sirenix.Utilities;
using TeppichsTurns.Actors;

namespace TeppichsTurns.Iterators.InitiativeIterators
{
    public sealed class DndInitiativeIterator : InitiativeIterator
    {
        public override IActor GetNextActor()
        {
            if (currentTurn.IsNullOrEmpty())
                StartTurn();
            
            currentTurn.Sort();
            IInitiativeActor nextActor = currentTurn.First();
            currentTurn.Remove(nextActor);

            return nextActor;
        }
    }
}