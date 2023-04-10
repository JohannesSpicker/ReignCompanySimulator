namespace Reign.Contests.Builders
{
    public static class A
    {
        public static StaticContestBuilder StaticContest => new();
        public static DynamicContestBuilder DynamicContest => new();
        public static OpposedContestBuilder OpposedContest => new();
    }
}