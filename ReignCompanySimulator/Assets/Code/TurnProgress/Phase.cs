using System;
using Code;

public abstract class Phase : Interval
{
    public override Action StartAction => GeneralTurnMessages.InvokeOnStartOfPhase;
    public override Action EndAction => GeneralTurnMessages.InvokeOnEndOfPhase;
}

public abstract class Step : Interval
{
    public override Action StartAction => GeneralTurnMessages.InvokeOnStartOfStep;
    public override Action EndAction => GeneralTurnMessages.InvokeOnEndOfStep;
}

public abstract class Interval
{
    public abstract Action StartAction { get; }
    public abstract Action EndAction { get; }

    public void Execute()
    {
        Begin();
        During();
        End();
    }

    protected virtual void Begin()
    {
        GeneralTurnMessages.InvokeOnStartOfInterval();
        StartAction?.Invoke();
    }

    protected abstract void During();

    protected virtual void End()
    {
        GeneralTurnMessages.InvokeOnEndOfInterval();
        EndAction?.Invoke();
    }
}

public abstract class Turn : Interval
{
}

public class IntervalRunner
{
    //runs the turns
}