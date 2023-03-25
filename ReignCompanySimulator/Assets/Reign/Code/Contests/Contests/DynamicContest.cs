using Reign.Contests.Dice;

namespace Reign.Contests.Contests
{
    public class DynamicContest : SharedContest
    {
        public override bool DetermineOutcome()
        {
            //TODO: implement class
            throw new System.NotImplementedException();
        }

        public override void MakeRolls()
        {
            throw new System.NotImplementedException();
        }

        public DynamicContest(DicePool activeDicePool, PassingCondition passingCondition, WinCondition winCondition, int penalties) : base(activeDicePool, passingCondition, winCondition, penalties)
        {
        }
    }
}