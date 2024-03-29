using Reign.Contests.Contests;

namespace Reign.Contests.Builders
{
    public class OpposedContestBuilder : SharedContestBuilder<OpposedContest>
    {
        protected override OpposedContest Build()
        {
            return new OpposedContest(activeDice, opposedDice, passingCon, winCon, penalty)
            {
                ActiveContestant = { rolledDice = activeRolledDice },
                opposingContestant = { rolledDice = opposedRolledDice }
            };
        }
    }
}