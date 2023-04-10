using Reign.Contests.Contests;

namespace Reign.Contests.Builders
{
    public class DynamicContestBuilder : SharedContestBuilder<DynamicContest>
    {
        protected override DynamicContest Build()
        {
            return new DynamicContest(activeDice, opposedDice, passingCon, winCon, penalty)
            {
                activeContestant = { rolledDice = activeRolledDice },
                opposingContestant = { rolledDice = opposedRolledDice }
            };
        }
    }
}