using Reign.Contests.Contests;
using Reign.Contests.Dice;
using TeppichsTools.Creation;

namespace Reign.Contests.Builders
{
    public abstract class ContestBuilder<T> : Builder<T> where T : Contest
    {
        protected DicePool dice;
        protected PassingCondition passingCon;
        protected int penalty;
        protected Contest.WinCondition winCon;

        public ContestBuilder<T> WithDicePool(DicePool dicePool)
        {
            dice = dicePool;
            return this;
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
    }
}