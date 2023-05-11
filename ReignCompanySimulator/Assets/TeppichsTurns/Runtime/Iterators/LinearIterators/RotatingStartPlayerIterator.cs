namespace TeppichsTurns.Iterators
{
    public sealed class RotatingStartPlayerIterator : StartPlayerIterator
    {
        protected override void StartTurn()
        {
            startingPlayerIndex++;
            startingPlayerIndex %= actors.Count;

            base.StartTurn();
        }
    }
}