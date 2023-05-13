using System;

namespace TeppichsTurns.Actors
{
    public interface IComparableActor<in T> : IActor, IComparable<T> { }
}