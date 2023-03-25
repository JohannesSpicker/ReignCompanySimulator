using Reign.Contests.Dice;

namespace Reign.Contests.Contests
{
    public abstract class SharedContest : Contest
    {
        public IContestant opposingContestant;
        public DicePool opposingDicePool;
        public RolledDice opposingRolledDice;

        protected SharedContest(DicePool activeDicePool, PassingCondition passingCondition, WinCondition winCondition, int penalties) : base(activeDicePool, passingCondition, winCondition, penalties)
        {
        }
    }
}