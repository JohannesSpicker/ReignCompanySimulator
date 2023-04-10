using System.Linq;
using Reign.Contests.Contests;
using Reign.Contests.Dice;

namespace Reign.Contests.Builders
{
    public abstract class SharedContestBuilder<T> : ContestBuilder<T> where T : SharedContest
    {
        protected DicePool opposedDice;
        protected RolledDice opposedRolledDice;
        
        public SharedContestBuilder<T> WithOpposingDicePool(DicePool dicePool)
        {
            opposedDice = dicePool;
            return this;
        }

        public SharedContestBuilder<T> WithOpposingRolledDice(params int[] diceRolled)
        {
            opposedRolledDice = new RolledDice(diceRolled.ToList());
            return this;
        }

        public SharedContestBuilder<T> WithOpposingRolledDice(RolledDice diceRolled)
        {
            opposedRolledDice = diceRolled;
            return this;
        }
    }
}