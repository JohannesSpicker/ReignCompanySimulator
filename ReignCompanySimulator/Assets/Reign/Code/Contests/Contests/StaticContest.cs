using Reign.Contests.Dice;

namespace Reign.Contests.Contests
{
    public class StaticContest : Contest
    {
        public StaticContest(DicePool activeDicePool, PassingCondition passingCondition, WinCondition winCondition,
            int penalties) : base(activeDicePool, passingCondition, winCondition, penalties)
        {
        }

        public override bool DetermineOutcome()
        {
            outcome = activeContestant.rolledDice.HasPassingSet(passingCondition);

            return outcome;
        }
    }
}