using System.Collections.Generic;
using TeppichsTurns.Actors;

namespace TeppichsTurns.Iterators
{
    public abstract class TurnIterator
    {
        protected readonly List<IActor> actors = new();

        public virtual void AddActor(IActor    actor) => actors.Add(actor);
        public virtual void RemoveActor(IActor actor) => actors.Remove(actor);

        public abstract IActor GetNextActor();
    }
}