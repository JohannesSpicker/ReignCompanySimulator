using System.Collections.Generic;
using System.Linq;
using Reign.Contests.Dice;

namespace Reign.Contests.Contests
{
    public abstract class Contest
    {
        public enum WinCondition
        {
            Height,
            Width
        }

        public List<Contestant> contestants = new();

        public bool outcome;

        public PassingCondition passingCondition;
        public int              penalties;
        public WinCondition     winCondition;

        protected Contest(DicePool activeDicePool, PassingCondition passingCondition, WinCondition winCondition,
                          int      penalties)
        {
            this.passingCondition = passingCondition;
            this.penalties        = penalties;
            this.winCondition     = winCondition;

            contestants.Add(new Contestant(activeDicePool));
        }

        public Contestant ActiveContestant => contestants[0];

        /// <summary>
        ///     Rolls all the dice.
        /// </summary>
        public void MakeRolls()
        {
            foreach (Contestant contestant in contestants)
                RollDice(contestant, passingCondition, winCondition, penalties);
        }

        /// <summary>
        ///     Calculates the outcome from the rolled dice.
        /// </summary>
        /// <returns>Whether the active contestant won</returns>
        public bool DetermineOutcome()
        {
            outcome = DetermineOutcomeInternal();

            return outcome;
        }

        protected abstract bool DetermineOutcomeInternal();

        private static void RollDice(Contestant   contestant,   PassingCondition passingCondition,
                                     WinCondition winCondition, int              penalties)
        {
            DicePool dicePool = contestant.dicePool;

            for (; 0 < penalties && 0 < dicePool.masterDice; penalties--)
                dicePool.masterDice--;

            if (1 < dicePool.masterDice)
            {
                dicePool.expertDice += dicePool.masterDice - 1;
                dicePool.masterDice =  1;
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

            rolled.AddRange(TeppichsDice.Dice.D10(dicePool.dice + dicePool.expertDice));

            RolledDice rolledDice = new(rolled);

            if (0 < dicePool.masterDice)
                rolledDice.AddDie(FindBestMasterDieValue());

            contestant.rolledDice = rolledDice;

            int FindBestMasterDieValue()
            {
                PassingCondition alteredPassingCondition =
                    new(passingCondition.minHeight, passingCondition.minWidth - 1);

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
    }
}