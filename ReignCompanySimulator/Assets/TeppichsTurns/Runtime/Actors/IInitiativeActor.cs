namespace TeppichsTurns.Actors
{
    public interface IInitiativeActor : IComparableActor<IInitiativeActor>
    {
        int CurrentInitiative { get; set; }
        int RollInitiative();
    }
}