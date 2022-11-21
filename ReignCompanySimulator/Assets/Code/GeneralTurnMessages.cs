using System;

namespace Code
{
    public static class GeneralTurnMessages
    {
        public static event Action OnStartOfTurn;
        public static event Action OnEndOfTurn;
        public static event Action OnStartOfPhase;
        public static event Action OnEndOfPhase;
        public static event Action OnStartOfStep;
        public static event Action OnEndOfStep;
        public static event Action OnStartOfInterval;
        public static event Action OnEndOfInterval;

        public static void InvokeOnStartOfTurn() => OnStartOfTurn?.Invoke();
        public static void InvokeOnEndOfTurn() => OnEndOfTurn?.Invoke();
        public static void InvokeOnStartOfPhase() => OnStartOfPhase?.Invoke();
        public static void InvokeOnEndOfPhase() => OnEndOfPhase?.Invoke();
        public static void InvokeOnStartOfStep() => OnStartOfStep?.Invoke();
        public static void InvokeOnEndOfStep() => OnEndOfStep?.Invoke();
        public static void InvokeOnStartOfInterval() => OnStartOfInterval?.Invoke();
        public static void InvokeOnEndOfInterval() => OnEndOfInterval?.Invoke();
    }
}