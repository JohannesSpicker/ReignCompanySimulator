namespace Reign.Contests
{
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