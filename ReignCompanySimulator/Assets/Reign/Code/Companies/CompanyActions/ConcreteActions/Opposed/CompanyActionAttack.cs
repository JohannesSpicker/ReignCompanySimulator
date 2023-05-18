using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Reign.Contests;
using Reign.Contests.Contests;
using Reign.Contests.Dice;
using TeppichsAttributes.Attributes;
using TeppichsTools.Data;

namespace Reign.Companies.CompanyActions
{
    public sealed class CompanyActionAttack : CompanyActionWithCostAndDefense
    {
        private AttackType attackType;

        protected override string ActionName => $"{attackType} Attack";

        public override bool IsViable(Company company) =>
            2 <= company.RemainingMight.Value + company.RemainingTreasure.Value;

        public override IEnumerator ProcessAction(Company activeCompany, Company defendingCompany)
        {
            attackType = DetermineAttackType(activeCompany, defendingCompany);

            OpposedContest contest = new(GetActivePool(activeCompany), GetDefendingPool(defendingCompany),
                                         PassingCondition.Default, Contest.WinCondition.Height, 0);

            contest.MakeRolls();

            bool outcome = contest.DetermineOutcome();

            if (outcome)
                AttackerWins(attackType, activeCompany, defendingCompany);

            LogAction(activeCompany, $"Defender: {defendingCompany.name}", outcome);

            yield return null;
        }

        protected override DicePool GetActivePool(Company company) => new(company.UseMight() + company.UseTreasure());

        protected override DicePool GetDefendingPool(Company company) =>
            new(company.UseMight() + company.UseTerritory());

        private static AttackType DetermineAttackType(Company attacker, Company defender)
        {
            List<AttackType> candidates = new();

            if (0 < defender.Treasure.Value)
                candidates.Add(AttackType.Raid);

            if (0 < defender.Territory.Value)
                candidates.Add(AttackType.Annexation);

            if (0 < defender.Sovereignty.Value)
                candidates.Add(AttackType.Symbolic);

            if (0 < defender.Might.Value)
                candidates.Add(AttackType.PreemptiveDefense);

            if (0 == candidates.Count)
                candidates.Add(AttackType.Symbolic);

            return candidates.Shuffle().First();
        }

        private static void AttackerWins(AttackType attackType, Company attacker, Company defender)
        {
            switch (attackType)
            {
                case AttackType.Raid:
                    if (attacker.Treasure.Value < defender.Treasure.Value)
                        ImproveStat(attackType, attacker);

                    ReduceStat(attackType, defender);

                    return;
                case AttackType.Annexation:
                    if (attacker.Territory.Value < defender.Territory.Value)
                        ImproveStat(attackType, attacker);

                    ReduceStat(attackType, defender);

                    return;
                case AttackType.Symbolic:

                    ReduceStat(attackType, defender);

                    return;
                case AttackType.PreemptiveDefense:

                    ReduceStat(attackType, defender);

                    return;
            }
        }

        private static void ReduceStat(AttackType attackType, Company company) =>
            GetRelevantStat(attackType, company).AddToBaseValue(-1);

        private static void ImproveStat(AttackType attackType, Company company) =>
            GetRelevantStat(attackType, company).AddToBaseValue(1);

        private static Stat GetRelevantStat(AttackType attackType, Company company) => attackType switch
        {
            AttackType.Raid              => company.Treasure,
            AttackType.Annexation        => company.Territory,
            AttackType.Symbolic          => company.Sovereignty,
            AttackType.PreemptiveDefense => company.Might,
            _                            => company.Sovereignty
        };

        private enum AttackType
        {
            Raid,
            Annexation,
            Symbolic,
            PreemptiveDefense
        }
    }
}