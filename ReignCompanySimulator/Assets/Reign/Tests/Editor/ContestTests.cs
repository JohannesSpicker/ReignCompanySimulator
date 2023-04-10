using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Reign.Contests.Builders;
using Reign.Contests.Contests;
using Reign.Contests.Dice;

namespace Reign.Tests.Editor
{
    public static class ContestTests
    {
        public static class StaticContestTests
        {
            [Test]
            public static void StaticContestIsSuccessful()
            {
                Contest contest = A.StaticContest.WithPassingCondition(1, 2).WithRolledDice(1, 1);

                contest.DetermineOutcome();

                contest.outcome.Should().BeTrue();
            }

            [Test]
            public static void StaticContestFailsDueToHeight()
            {
                Contest contest = A.StaticContest.WithPassingCondition(2, 2).WithRolledDice(1, 1);

                contest.DetermineOutcome();

                contest.outcome.Should().BeFalse();
            }

            [Test]
            public static void StaticContestFailsDueToWidth()
            {
                Contest contest = A.StaticContest.WithPassingCondition(1, 3).WithRolledDice(1, 1);

                contest.DetermineOutcome();

                contest.outcome.Should().BeFalse();
            }

            [Test]
            public static void StaticContestFailsDueToLackOfSet()
            {
                List<int> dice = new();

                for (int i = 1; i <= 10; i++)
                    dice.Add(i);

                Contest contest = A.StaticContest.WithRolledDice(new RolledDice(dice));

                contest.DetermineOutcome();

                contest.outcome.Should().BeFalse();
            }
        }

        public static class DynamicContestTests
        {
            public static class SingleDice
            {
                [Test]
                public static void OneDieWinsOverNoDie()
                {
                    Contest contest = A.DynamicContest.WithOpposingRolledDice(new RolledDice(new List<int>()))
                        .WithRolledDice(1).WithPassingCondition(1, 2)
                        .WithWinCondition(Contest.WinCondition.Height);

                    contest.DetermineOutcome();

                    contest.outcome.Should().BeTrue();
                }

                [Test]
                public static void NoDieLosesToOneDie()
                {
                    Contest contest = A.DynamicContest.WithOpposingRolledDice(1)
                        .WithRolledDice(new RolledDice(new List<int>())).WithPassingCondition(1, 2)
                        .WithWinCondition(Contest.WinCondition.Height);

                    contest.DetermineOutcome();

                    contest.outcome.Should().BeFalse();
                }

                [Test]
                public static void OneHighDiceWinsOverOneLowDice()
                {
                    Contest contest = A.DynamicContest.WithOpposingRolledDice(1).WithRolledDice(10)
                        .WithPassingCondition(1, 2)
                        .WithWinCondition(Contest.WinCondition.Height);

                    contest.DetermineOutcome();

                    contest.outcome.Should().BeTrue();
                }

                [Test]
                public static void OneLowDiceLosesToOneHighDice()
                {
                    Contest contest = A.DynamicContest.WithOpposingRolledDice(10).WithRolledDice(1)
                        .WithPassingCondition(1, 2)
                        .WithWinCondition(Contest.WinCondition.Height);

                    contest.DetermineOutcome();

                    contest.outcome.Should().BeFalse();
                }
            }

            public static class SingleSet
            {
                [Test]
                public static void HighPassingSetWinsAgainstLowPassingSet()
                {
                    Contest contest = A.DynamicContest.WithOpposingRolledDice(1, 1).WithRolledDice(10, 10)
                        .WithPassingCondition(1, 2).WithWinCondition(Contest.WinCondition.Height);

                    contest.DetermineOutcome();

                    contest.outcome.Should().BeTrue();
                }

                [Test]
                public static void LowPassingSetLosesAgainstHighPassingSet()
                {
                    Contest contest = A.DynamicContest.WithOpposingRolledDice(10, 10).WithRolledDice(1, 1)
                        .WithPassingCondition(1, 2).WithWinCondition(Contest.WinCondition.Height);

                    contest.DetermineOutcome();

                    contest.outcome.Should().BeFalse();
                }

                [Test]
                public static void HighestSetsGetEliminatedAllPassing()
                {
                    Contest contest = A.DynamicContest.WithOpposingRolledDice(10, 10, 10, 9, 9, 9, 8, 8, 8, 7, 7, 7, 6,
                            6, 6, 5, 5, 5, 4, 4, 4, 3, 3, 3, 1, 1, 1).WithRolledDice(10, 10, 10, 9, 9, 9, 8, 8, 8, 7, 7,
                            7, 6,
                            6, 6, 5, 5, 5, 4, 4, 4, 3, 3, 3, 2, 2, 2)
                        .WithPassingCondition(1, 3).WithWinCondition(Contest.WinCondition.Height);

                    contest.DetermineOutcome();

                    contest.outcome.Should().BeTrue();
                }

                [Test]
                public static void HighestSetsGetEliminatedNotAllPassing()
                {
                    Contest contest = A.DynamicContest.WithOpposingRolledDice(10, 10, 9, 9, 9, 8, 8, 7, 7, 7, 6,
                            6, 6, 5, 5, 5, 4, 4, 4, 3, 3, 1, 1, 1).WithRolledDice(10, 10, 9, 9, 9, 8, 8, 7, 7, 7, 6,
                            6, 6, 5, 5, 5, 4, 4, 4, 3, 3, 2, 2, 2)
                        .WithPassingCondition(6, 3).WithWinCondition(Contest.WinCondition.Height);

                    contest.DetermineOutcome();

                    contest.outcome.Should().BeTrue();
                }
            }
        }
    }
}