using System.Collections.Generic;
using TeppichsTurns.Actors;
using TeppichsTurns.Iterators;

namespace TeppichsTurns
{
    public class TurnOrder
    {
        private List<IActor> actors = new();
        private TurnIterator turnIterator;

        public void ProcessTurn() => turnIterator.GetNextActor().DoTurn(ProcessTurn);
    }
}