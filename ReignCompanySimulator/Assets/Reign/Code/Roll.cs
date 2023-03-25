using System.Collections.Generic;
using System.Linq;
using TeppichsDice;
using UnityEngine;

namespace Reign
{
    public class StaticContest : Contest
    {
        //put in  dice number, difficulty, penalties

        //outcome: did win?
        public override bool DetermineOutcome()
        {
            throw new System.NotImplementedException();
        }
    }

    //

    // public class DynamicContest : Contest { }

    //
    // public class OpposedContest : Contest { }

    public abstract class SharedContest : Contest
    {
        public IContestant opposingContestant;
        public DicePool opposingDicePool;
    }

    public interface IContestant
    {
    }

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

    public struct PassingCondition
    {
        public int minHeight;
        public int minWidth;

        public PassingCondition(int minHeight, int minWidth)
        {
            this.minHeight = minHeight;
            this.minWidth = minWidth;
        }
    }

    public class RolledDice
    {
        public RolledDice(List<int> rolled)
        {
            rolled.Sort();

            List<Set> dirtySets = rolled.GroupBy(x => x).Select(g => new Set(g.Key, g.Count())).ToList();
            sets = dirtySets.Where(s => 1 < s.width).ToList();
            waste = dirtySets.Where(s => 1 == s.width).Select(s => s.height).ToList();
        }

        public List<Set> sets;
        public List<int> waste;

        public bool HasSet => sets.Any();

        public bool HasPassingSet(PassingCondition passingCondition) =>
            sets.Any(s => s.PassesCondition(passingCondition));

        public bool TryGetHighestPassingSet(out Set highestSet, PassingCondition passingCondition)
        {
            highestSet = sets.Where(s => s.PassesCondition(passingCondition)).OrderByDescending(s => s.height).First();
            return highestSet != null;
        }

        public bool TryGetWidestPassingSet(out Set widestSet, PassingCondition passingCondition)
        {
            widestSet = sets.Where(s => s.PassesCondition(passingCondition)).OrderByDescending(s => s.width).First();
            return widestSet != null;
        }

        public bool TryGetHighestWaste(out int highestWaste)
        {
            highestWaste = waste.Max();
            return waste.Any();
        }

        public void AddDie(int value)
        {
            Set set = sets.Find(s => s.height == value);

            if (set != null)
                set.width++;
            else if (waste.Remove(value))
                sets.Add(new Set(value, 2));
            else
                waste.Add(value);
        }

        public void BreakSetInTwo(Set setToBreakUp)
        {
            if (!sets.Remove(setToBreakUp))
                return;

            sets.Add(new Set(setToBreakUp.height, Mathf.CeilToInt((setToBreakUp.width / 2f))));
            sets.Add(new Set(setToBreakUp.height, Mathf.FloorToInt((setToBreakUp.width / 2f))));
        }
    }

    public class Set
    {
        public Set(int height, int width)
        {
            this.height = height;
            this.width = width;
        }

        public readonly int height;
        public int width;

        public bool PassesCondition(PassingCondition passingCondition) =>
            passingCondition.minHeight <= height && passingCondition.minWidth <= width;
    }
}