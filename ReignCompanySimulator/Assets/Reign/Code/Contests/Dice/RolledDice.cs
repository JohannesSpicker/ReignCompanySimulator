using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Reign.Contests.Dice
{
    public class RolledDice
    {
        public RolledDice(List<int> rolled)
        {
            rolled.Sort();

            List<Set> dirtySets = rolled.GroupBy(x => x).Select(g => new Set(g.Key, g.Count())).ToList();
            sets = dirtySets.Where(s => 1 < s.width).ToList();
            waste = dirtySets.Where(s => 1 == s.width).Select(s => s.height).ToList();
        }

        public RolledDice(RolledDice rolledDice)
        {
            sets = rolledDice.sets.ToList();
            waste = rolledDice.waste.ToList();
        }

        public List<Set> sets;
        public List<int> waste;

        public bool HasSet => sets.Any();

        public bool HasPassingSet(PassingCondition passingCondition) =>
            sets.Any(s => s.PassesCondition(passingCondition));

        public bool TryGetHighestPassingSet(out Set highestSet, PassingCondition passingCondition)
        {
            highestSet = sets.Where(s => s.PassesCondition(passingCondition)).OrderByDescending(s => s.height)
                .FirstOrDefault();
            return highestSet != null;
        }

        public bool TryGetWidestPassingSet(out Set widestSet, PassingCondition passingCondition)
        {
            widestSet = sets.Where(s => s.PassesCondition(passingCondition)).OrderByDescending(s => s.width).First();
            return widestSet != null;
        }

        public bool TryGetHighestWaste(out int highestWaste)
        {
            highestWaste = waste.OrderByDescending(x => x).FirstOrDefault();
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
}