using TeppichsAttributes.Attributes;

namespace Reign.Companies.CompanyActions
{
    public class CompanyActionRiseInStature : CompanyActionRaiseQuality
    {
        protected override string ActionName => "Rise In Stature";

        public override bool IsViable(Company company) =>
            2 <= company.RemainingSovereignty.Value + company.RemainingTreasure.Value;

        protected override int  GetActivePool(Company company) => company.UseTerritory() + company.UseTreasure();
        protected override Stat StatToImprove(Company company) => company.Influence;
    }
}