using System.Collections.Generic;
using TeppichsTools.Math.Randomness;
using TeppichsTurns.Actors;

namespace TeppichsTurns.Iterators
{
    public sealed class RandomTurnIterator : TurnIterator<IActor>
    {
        public RandomTurnIterator(List<IActor> actors) : base(actors) { }
        public override IActor GetNextActor() => actors[ThreadSafeRandom.ThisThreadsRandom.Next(actors.Count)];
    }
}