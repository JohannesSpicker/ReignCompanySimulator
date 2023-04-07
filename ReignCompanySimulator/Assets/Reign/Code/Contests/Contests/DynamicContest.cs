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

        public override bool DetermineOutcome()
        {
            outcome = GetOutcome();
            return outcome;

            bool GetOutcome()
            {
                RolledDice activeDice = new RolledDice(activeContestant.rolledDice);
                RolledDice opposingDice = new RolledDice(opposingContestant.rolledDice);

                bool isHeight = winCondition is WinCondition.Height;

                while (TryGetBestActiveSet(out var activeSet) && TryGetBestOpposingSet(out var opposingSet))
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
                
                //TODO: check both for non-passing sets

                if (activeDice.HasSet)
                    return true;
                if (opposingDice.HasSet)
                    return false;

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

                bool TryGetBestActiveSet(out Set activeSet)
                {
                    return isHeight
                        ? activeDice.TryGetHighestPassingSet(out activeSet, passingCondition)
                        : activeDice.TryGetWidestPassingSet(out activeSet, passingCondition);
                }

                bool TryGetBestOpposingSet(out Set opposingSet)
                {
                    return isHeight
                        ? opposingDice.TryGetHighestPassingSet(out opposingSet, passingCondition)
                        : opposingDice.TryGetWidestPassingSet(out opposingSet, passingCondition);
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
}