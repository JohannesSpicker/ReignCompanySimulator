using System.Linq;
using Sirenix.Utilities;
using TeppichsTurns.Actors;
using UnityEngine;

namespace TeppichsTurns.Iterators.InitiativeIterators
{
    public sealed class ShadowrunInitiativeIterator : InitiativeIterator
    {
        private int costPerTurn = 10;

        public override IActor GetNextActor()
        {
            if (currentTurn.IsNullOrEmpty())
                StartTurn();

            currentTurn.Sort();
            IInitiativeActor nextActor = currentTurn.First();
            
            nextActor.CurrentInitiative = Mathf.Max(0, nextActor.CurrentInitiative - costPerTurn);

            if (nextActor.CurrentInitiative == 0)
                currentTurn.Remove(nextActor);

            return nextActor;
        }
    }
}