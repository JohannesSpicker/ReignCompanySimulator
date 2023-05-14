using TeppichsAttributes.Attributes;

namespace Reign.Companies.CompanyActions
{
    public class CompanyActionImproveTheCulture : CompanyActionRaiseQuality
    {
        protected override string ActionName => "Improve The Culture";

        public override bool IsViable(Company company) =>
            2 <= company.RemainingTerritory.Value + company.RemainingTreasure.Value;

        protected override int  GetActivePool(Company company) => company.UseTerritory() + company.UseTreasure();
        protected override Stat StatToImprove(Company company) => company.Sovereignty;
    }
}