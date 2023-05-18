using Reign.Contests.Dice;
using TeppichsAttributes.Attributes;

namespace Reign.Companies.CompanyActions
{
    public class CompanyActionTrainAndLevyTroops : CompanyActionRaiseQuality
    {
        protected override string ActionName => "Train And Levy Troops";

        public override bool IsViable(Company company) => base.IsViable(company)
                                                          && 2 <= company.RemainingSovereignty.Value
                                                          + company.RemainingTerritory.Value;

        protected override DicePool GetActivePool(Company company) =>
            new(company.UseSovereignty() + company.UseTerritory());

        protected override Stat StatToImprove(Company company) => company.Might;
    }
}