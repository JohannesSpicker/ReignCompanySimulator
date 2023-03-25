using Reign.Contests.Contests;

namespace Reign.Contests.Builders
{
    public class StaticContestBuilder : ContestBuilder<StaticContest>
    {
        protected override StaticContest Build() => new StaticContest(dice, passingCon, winCon, penalty);
    }
}