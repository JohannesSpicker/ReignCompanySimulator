using System.Collections.Generic;
using TeppichsTurns.Actors;

namespace TeppichsTurns.Iterators.InitiativeIterators
{
    public abstract class InitiativeIterator<T> : TurnIterator<T> where T : IInitiativeActor
    {
        protected readonly List<IInitiativeActor> currentTurn = new();

        protected InitiativeIterator(List<T> actors) : base(actors) { }

        protected void StartTurn()
        {
            currentTurn.Clear();

            foreach (T actor in actors)
            {
                if (actor is not IInitiativeActor initiativeActor)
                    continue;

                initiativeActor.RollInitiative();
                currentTurn.Add(initiativeActor);
            }
        }
    }
}