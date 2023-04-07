using Reign.Contests.Dice;

namespace Reign.Contests.Contests
{
    public abstract class SharedContest : Contest
    {
        public Contestant opposingContestant => contestants[1];

        protected SharedContest(DicePool activeDicePool, DicePool opposingDicePool, PassingCondition passingCondition,
            WinCondition winCondition, int penalties) : base(activeDicePool, passingCondition, winCondition, penalties)
        {
            contestants.Add(new Contestant(opposingDicePool));
        }
    }
}