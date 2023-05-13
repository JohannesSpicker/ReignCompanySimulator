using System.Linq;

namespace TeppichsTurns.Iterators
{
    public class ClockwiseTurnIterator : SimpleTurnIterator
    {
        protected override void StartTurn() => currentTurn = actors.ToList();
    }
}