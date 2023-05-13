using System.Collections.Generic;
using System.Linq;
using TeppichsTurns.Actors;

namespace TeppichsTurns.Iterators
{
    public class ClockwiseTurnIterator<T> : SimpleTurnIterator<T> where T : IActor
    {
        public ClockwiseTurnIterator(List<T> actors) : base(actors) { }
        protected override void StartTurn() => currentTurn = actors.ToList();
    }
}