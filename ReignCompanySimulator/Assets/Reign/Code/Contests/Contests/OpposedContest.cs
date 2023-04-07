using Reign.Contests.Dice;

namespace Reign.Contests.Contests
{
    public class OpposedContest : Contest
    {
        public override bool DetermineOutcome()
        {
            //TODO: implement class
            throw new System.NotImplementedException();
        }

        public OpposedContest(DicePool activeDicePool, PassingCondition passingCondition, WinCondition winCondition, int penalties) : base(activeDicePool, passingCondition, winCondition, penalties)
        {
        }
    }
}