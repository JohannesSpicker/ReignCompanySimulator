using System.Collections.Generic;
using System.Linq;
using TeppichsDice;

namespace Reign.Contests
{
    public abstract class Contest
    {
        public enum WinCondition
        {
            Height,
            Width
        }

        public IContestant activeContestant;
        public DicePool activeDicePool;

        public bool outcome;

        public PassingCondition passingCondition;
        public int penalties;

        public abstract bool DetermineOutcome();

        private static RolledDice RollDice(DicePool dicePool, PassingCondition passingCondition,
            WinCondition winCondition, int penalties)
        {
            for (; 0 < penalties && 0 < dicePool.masterDice; penalties--)
                dicePool.masterDice--;

            if (1 < dicePool.masterDice)
            {
                dicePool.expertDice += dicePool.masterDice - 1;
                dicePool.masterDice = 1;
            }

            for (; 0 < penalties && 0 < dicePool.expertDice; penalties--)
                dicePool.expertDice--;

            for (; 0 < penalties && 0 < dicePool.dice; penalties--)
                dicePool.dice--;

            List<int> rolled = new();

            for (int i = 10; 0 < dicePool.expertDice && 0 < i; i--)
            {
                rolled.Add(i);
                dicePool.expertDice--;
            }

            rolled.AddRange(Dice.D10(dicePool.dice + dicePool.expertDice));

            RolledDice rolledDice = new RolledDice(rolled);

            if (0 < dicePool.masterDice)
                rolledDice.AddDie(FindBestMasterDieValue());

            return rolledDice;

            int FindBestMasterDieValue()
            {
                PassingCondition alteredPassingCondition =
                    new PassingCondition(passingCondition.minHeight, passingCondition.minWidth - 1);

                if (winCondition == WinCondition.Height)
                {
                    List<int> candidates = new();

                    if (alteredPassingCondition.minWidth < 2 && rolledDice.TryGetHighestWaste(out int highestWaste))
                        candidates.Add(highestWaste);

                    if (rolledDice.TryGetHighestPassingSet(out Set highestSet, alteredPassingCondition))
                        candidates.Add(highestSet.height);

                    if (candidates.Any())
                        return candidates.Max();
                }
                else
                {
                    if (rolledDice.TryGetWidestPassingSet(out Set widestSet, alteredPassingCondition))
                        return widestSet.height;

                    if (alteredPassingCondition.minWidth < 2 && rolledDice.TryGetHighestWaste(out int highestWaste))
                        return highestWaste;
                }

                return 10;
            }
        }

        public struct DicePool
        {
            public int dice;
            public int expertDice;
            public int masterDice;
        }
    }
}