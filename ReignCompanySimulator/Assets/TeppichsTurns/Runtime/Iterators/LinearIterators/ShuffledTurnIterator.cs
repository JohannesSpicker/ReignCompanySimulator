using System.Linq;
using TeppichsTools.Data;

namespace TeppichsTurns.Iterators
{
    public sealed class ShuffledTurnIterator : SimpleTurnIterator
    {
        protected override void StartTurn()
        {
            currentTurn = actors.ToList();
            currentTurn.Shuffle();
        }
    }
}