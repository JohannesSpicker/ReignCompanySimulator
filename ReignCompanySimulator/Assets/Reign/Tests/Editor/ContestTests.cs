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
}