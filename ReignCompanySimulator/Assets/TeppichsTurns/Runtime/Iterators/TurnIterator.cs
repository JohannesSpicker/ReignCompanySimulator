using System.Collections.Generic;
using TeppichsTurns.Actors;

namespace TeppichsTurns.Iterators
{
    public abstract class TurnIterator<T> where T : IActor
    {
        protected readonly List<T> actors;

        protected TurnIterator(List<T> actors) { this.actors = actors; }

        public virtual void AddActor(T    actor) => actors.Add(actor);
        public virtual void RemoveActor(T actor) => actors.Remove(actor);

        public abstract T GetNextActor();
    }
}