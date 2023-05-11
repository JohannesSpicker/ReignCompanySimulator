using System;

namespace TeppichsTurns.Actors
{
    public interface IActor
    {
        /// <summary>
        ///     Process an actor turn.
        /// </summary>
        /// <param name="callback">Action to invoke to give control back to the <see cref="TurnOrder" /></param>
        void DoTurn(Action callback);
    }
}