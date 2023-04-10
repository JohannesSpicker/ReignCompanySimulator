using Reign.Contests.Dice;

namespace Reign.Contests.Contests
{
    public class DynamicContest : SharedContest
    {
        public DynamicContest(DicePool activeDicePool, DicePool opposingDicePool, PassingCondition passingCondition,
            WinCondition winCondition, int penalties) : base(activeDicePool, opposingDicePool, passingCondition,
            winCondition, penalties)
        {
        }

        protected override bool DetermineOutcomeInternal()
        {
            RolledDice activeDice = new RolledDice(activeContestant.rolledDice);
            RolledDice opposingDice = new RolledDice(opposingContestant.rolledDice);

            bool isHeight = winCondition is WinCondition.Height;

            while (TryGetBestActiveSet(out var activeSet, passingCondition) &&
                   TryGetBestOpposingSet(out var opposingSet, passingCondition))
            {
                if (IsPrimaryConditionEqual(activeSet, opposingSet))
                {
                    if (IsSecondaryConditionEqual(activeSet, opposingSet))
                    {
                        activeDice.sets.Remove(activeSet);
                        opposingDice.sets.Remove(opposingSet);

                        continue;
                    }

                    return CheckSecondaryCondition(activeSet, opposingSet);
                }

                return CheckPrimaryCondition(activeSet, opposingSet);
            }

            PassingCondition easyPassingCondition = new PassingCondition(1, 2);
            
            while (TryGetBestActiveSet(out var activeSet, easyPassingCondition) &&
                   TryGetBestOpposingSet(out var opposingSet, easyPassingCondition))
            {
                if (IsPrimaryConditionEqual(activeSet, opposingSet))
                {
                    if (IsSecondaryConditionEqual(activeSet, opposingSet))
                    {
                        activeDice.sets.Remove(activeSet);
                        opposingDice.sets.Remove(opposingSet);

                        continue;
                    }

                    return CheckSecondaryCondition(activeSet, opposingSet);
                }

                return CheckPrimaryCondition(activeSet, opposingSet);
            }

            while (activeDice.TryGetHighestWaste(out var activeWaste) &&
                   opposingDice.TryGetHighestWaste(out var opposingWaste))
            {
                if (activeWaste == opposingWaste)
                {
                    activeDice.waste.Remove(activeWaste);
                    opposingDice.waste.Remove(opposingWaste);

                    continue;
                }

                return opposingWaste < activeWaste;
            }

            return activeDice.TryGetHighestWaste(out var _);

            bool TryGetBestActiveSet(out Set activeSet, PassingCondition passCon)
            {
                return isHeight
                    ? activeDice.TryGetHighestPassingSet(out activeSet, passCon)
                    : activeDice.TryGetWidestPassingSet(out activeSet, passCon);
            }

            bool TryGetBestOpposingSet(out Set opposingSet, PassingCondition passCon)
            {
                return isHeight
                    ? opposingDice.TryGetHighestPassingSet(out opposingSet, passCon)
                    : opposingDice.TryGetWidestPassingSet(out opposingSet, passCon);
            }

            bool IsPrimaryConditionEqual(Set activeSet, Set opposingSet)
            {
                return isHeight
                    ? IsHeightEqual(activeSet, opposingSet)
                    : IsWidthEqual(activeSet, opposingSet);
            }

            bool IsSecondaryConditionEqual(Set activeSet, Set opposingSet)
            {
                return isHeight
                    ? IsWidthEqual(activeSet, opposingSet)
                    : IsHeightEqual(activeSet, opposingSet);
            }

            bool IsHeightEqual(Set activeSet, Set opposingSet)
            {
                return activeSet.height == opposingSet.height;
            }

            bool IsWidthEqual(Set activeSet, Set opposingSet)
            {
                return activeSet.width == opposingSet.width;
            }

            bool CheckPrimaryCondition(Set activeSet, Set opposingSet)
            {
                return isHeight ? CheckHeight(activeSet, opposingSet) : CheckWidth(activeSet, opposingSet);
            }

            bool CheckSecondaryCondition(Set activeSet, Set opposingSet)
            {
                return isHeight ? CheckWidth(activeSet, opposingSet) : CheckHeight(activeSet, opposingSet);
            }

            bool CheckHeight(Set activeSet, Set opposingSet)
            {
                return opposingSet.height < activeSet.height;
            }

            bool CheckWidth(Set activeSet, Set opposingSet)
            {
                return opposingSet.width < activeSet.width;
            }
        }
    }
}