using TeppichsTools.Math.Randomness;
using TeppichsTurns.Actors;

namespace TeppichsTurns.Iterators
{
    public sealed class RandomTurnIterator : TurnIterator
    {
        public override IActor GetNextActor() => actors[ThreadSafeRandom.ThisThreadsRandom.Next(actors.Count)];
    }
}