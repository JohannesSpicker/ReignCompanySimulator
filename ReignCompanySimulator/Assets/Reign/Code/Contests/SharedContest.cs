namespace Reign.Contests
{
    public abstract class SharedContest : Contest
    {
        public IContestant opposingContestant;
        public DicePool opposingDicePool;
    }
}