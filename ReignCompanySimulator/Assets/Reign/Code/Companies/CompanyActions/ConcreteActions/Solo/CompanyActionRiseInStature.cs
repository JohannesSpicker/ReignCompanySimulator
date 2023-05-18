using Reign.Contests.Dice;
using TeppichsAttributes.Attributes;

namespace Reign.Companies.CompanyActions
{
    public class CompanyActionRiseInStature : CompanyActionRaiseQuality
    {
        protected override string ActionName => "Rise In Stature";

        public override bool IsViable(Company company) => base.IsViable(company)
                                                          && 2 <= company.RemainingSovereignty.Value
                                                          + company.RemainingTreasure.Value;

        protected override DicePool GetActivePool(Company company) =>
            new(company.UseSovereignty() + company.UseTreasure());

        protected override Stat StatToImprove(Company company) => company.Influence;
    }
}