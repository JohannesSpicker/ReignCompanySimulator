using System.Linq;
using Reign.Contests.Dice;

namespace Reign.Contests.Contests
{
    public class OpposedContest : SharedContest
    {
        protected override bool DetermineOutcomeInternal()
        {
            RolledDice activeDice = new RolledDice(ActiveContestant.rolledDice);

            if (!activeDice.HasPassingSet(passingCondition))
                return false;

            RolledDice opposingDice = new RolledDice(opposingContestant.rolledDice);

            if (!opposingDice.HasPassingSet(passingCondition))
                return true;

            bool doesTimingMatter =
                winCondition is WinCondition.Width ||
                2 < passingCondition.minWidth; //this might want to become a bool in the contest itself

            foreach (Set gobblingSet in opposingDice.sets.Where(s => s.PassesCondition(passingCondition))
                         .OrderByDescending(s => s.height))
            {
                for (int i = 0; i < gobblingSet.width; i++)
                {
                    if (activeDice.TryGetHighestPassingSet(out Set activeSet, passingCondition) &&
                        activeSet.height <= gobblingSet.height &&
                        (!doesTimingMatter || activeSet.width <= gobblingSet.width))
                    {
                        activeSet.width--;

                        if (activeSet.width < 2)
                            activeDice.sets.Remove(activeSet);
                    }
                }
            }
            
            //TODO:
            //but this doesn't solve, if timing matters:
            // active: 777 66
            // opposed: 99 666 
            
            return activeDice.HasPassingSet(passingCondition);
        }

        public OpposedContest(DicePool activeDicePool, DicePool opposingDicePool, PassingCondition passingCondition,
            WinCondition winCondition, int penalties) : base(activeDicePool, opposingDicePool, passingCondition,
            winCondition, penalties)
        {
        }
    }
}