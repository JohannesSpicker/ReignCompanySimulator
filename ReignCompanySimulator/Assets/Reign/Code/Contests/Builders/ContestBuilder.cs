using System.Linq;
using Reign.Contests.Contests;
using Reign.Contests.Dice;
using TeppichsTools.Creation;

namespace Reign.Contests.Builders
{
    public abstract class ContestBuilder<T> : Builder<T> where T : Contest
    {
        protected PassingCondition passingCon;
        protected int penalty;
        protected Contest.WinCondition winCon;
        
        protected DicePool activeDice;
        protected RolledDice activeRolledDice;

        public ContestBuilder<T> WithDicePool(DicePool dicePool)
        {
            activeDice = dicePool;
            return this;
        }

        public ContestBuilder<T> WithPassingCondition(int minHeight, int minWidth)
        {
            return WithPassingCondition(new PassingCondition(minHeight, minWidth));
        }

        public ContestBuilder<T> WithPassingCondition(PassingCondition passingCondition)
        {
            passingCon = passingCondition;
            return this;
        }

        public ContestBuilder<T> WithWinCondition(Contest.WinCondition winCondition)
        {
            winCon = winCondition;
            return this;
        }

        public ContestBuilder<T> WithPenalties(int penalties)
        {
            penalty = penalties;
            return this;
        }

        public ContestBuilder<T> WithRolledDice(params int[] diceRolled)
        {
            activeRolledDice = new RolledDice(diceRolled.ToList());
            return this;
        }

        public ContestBuilder<T> WithRolledDice(RolledDice diceRolled)
        {
            activeRolledDice = diceRolled;
            return this;
        }
    }
}