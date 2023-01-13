using System.Collections.Generic;
using System.Linq;
using TeppichsDice;

namespace Reign
{
    // public class StaticContest : Contest
    // {
    //     //put in  dice number, difficulty, penalties
    //     //outcome: did win?
    // }
    //
    // public class DynamicContest : Contest { }
    //
    // public class OpposedContest : Contest { }

    public interface IContestant { }

    public abstract class Contest
    {
        public enum WinCondition
        {
            Height,
            Width
        }

        public IContestant activeContestant;
        public DicePool    activeDicePool;

        public bool outcome;

        public PassingCondition passingCondition;
        public int              penalties;

        public abstract bool DetermineOutcome();

        public static List<int> RollDice(DicePool     dicePool,     PassingCondition passingCondition,
                                         WinCondition winCondition, int              penalties)
        {
            for (; 0 < penalties && 0 < dicePool.masterDice; penalties--)
                dicePool.masterDice--;

            for (; 0 < penalties && 0 < dicePool.expertDice; penalties--)
                dicePool.expertDice--;

            for (; 0 < penalties && 0 < dicePool.dice; penalties--)
                dicePool.dice--;

            List<int> rolled = new();

            for (int i = 10; 0 < dicePool.expertDice; i--)
            {
                rolled.Add(i);
                dicePool.expertDice--;
            }

            rolled.AddRange(Dice.D10(dicePool.dice));

            //add the master die to the highest/widest set

            return rolled;
        }

        public struct DicePool
        {
            public int dice;
            public int expertDice;
            public int masterDice;
        }

        public struct PassingCondition
        {
            public int minHeight;
            public int minWidth;
        }
    }

    public abstract class SharedContest : Contest
    {
        public IContestant opposingContestant;
        public DicePool    opposingDicePool;
    }

    public struct RolledDice
    {
        public RolledDice(List<int> rolled) : this()
        {
            rolled.Sort();

            sets = rolled.GroupBy(x => x).Select(g => new Set(g.Key, g.Count())).ToList();

            waste = rolled;//TODO: put not-set dice into waste.
        }

        public List<Set> sets;
        public List<int> waste;

        public Set HighestSet(int minimalWidth = 2) => null;
    }

    public struct Set
    {
        public Set(int height, int width)
        {
            this.height = height;
            this.width  = width;
        }

        public int height;
        public int width;
    }
}