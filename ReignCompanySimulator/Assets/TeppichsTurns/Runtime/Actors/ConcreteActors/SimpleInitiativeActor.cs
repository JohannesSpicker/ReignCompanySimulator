using System;

namespace TeppichsTurns.Actors.ConcreteActors
{
    public class SimpleInitiativeActor : IInitiativeActor
    {
        public void DoTurn(Action              callback) => throw new NotImplementedException();
        public int  CompareTo(IInitiativeActor other)    => throw new NotImplementedException();
        public int  CurrentInitiative                    { get; set; }
        public int  RollInitiative()                     => throw new NotImplementedException();
    }
}