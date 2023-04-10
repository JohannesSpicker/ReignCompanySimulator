using Reign.Contests.Contests;

namespace Reign.Contests.Builders
{
    public class StaticContestBuilder : ContestBuilder<StaticContest>
    {
        protected override StaticContest Build()
        {
            return new StaticContest(activeDice, passingCon, winCon, penalty)
            {
                activeContestant =
                {
                    rolledDice = activeRolledDice
                }
            };
        }
    }
}