namespace Reign.Contests.Dice
{
    public struct DicePool
    {
        public DicePool(int dice) : this() { this.dice = dice; }

        public DicePool(int dice, int expertDice, int masterDice)
        {
            this.dice       = dice;
            this.expertDice = expertDice;
            this.masterDice = masterDice;
        }

        public int dice;
        public int expertDice;
        public int masterDice;
    }
}