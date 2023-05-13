using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using TeppichsTurns.Actors;

namespace TeppichsTurns.Iterators.InitiativeIterators
{
    public sealed class DndInitiativeIterator : InitiativeIterator<IInitiativeActor>
    {
        public DndInitiativeIterator(List<IInitiativeActor> actors) : base(actors) { }

        public override IInitiativeActor GetNextActor()
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