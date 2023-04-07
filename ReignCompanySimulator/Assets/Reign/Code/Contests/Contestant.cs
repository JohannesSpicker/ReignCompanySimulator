using Reign.Contests.Dice;

namespace Reign.Contests
{
    public class Contestant
    {
        public Contestant(DicePool dicePool)
        {
            this.dicePool = dicePool;
        }

        public DicePool dicePool;
        public RolledDice rolledDice;
    }
}