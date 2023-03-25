namespace Reign.Contests
{
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
}