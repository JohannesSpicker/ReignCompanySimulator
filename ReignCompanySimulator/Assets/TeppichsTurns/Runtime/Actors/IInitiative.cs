namespace TeppichsTurns.Actors
{
    public interface IInitiative
    {
        int CurrentInitiative { get; set; }
        int RollInitiative();
    }
}