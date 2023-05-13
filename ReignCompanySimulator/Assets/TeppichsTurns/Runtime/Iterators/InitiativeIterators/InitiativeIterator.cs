using System.Collections.Generic;
using TeppichsTurns.Actors;

namespace TeppichsTurns.Iterators.InitiativeIterators
{
    public abstract class InitiativeIterator : TurnIterator
    {
        protected readonly List<IInitiativeActor> currentTurn = new();

        protected void StartTurn()
        {
            currentTurn.Clear();

            foreach (IActor actor in actors)
            {
                if (actor is not IInitiativeActor initiativeActor)
                    continue;

                initiativeActor.RollInitiative();
                currentTurn.Add(initiativeActor);
            }
        }
    }
}