using TeppichsAttributes.Attributes;

namespace Reign.Companies.CompanyActions
{
    public class CompanyActionTrainAndLevyTroops : CompanyActionRaiseQuality
    {
        protected override string ActionName => "Train And Levy Troops";

        public override bool IsViable(Company company) =>
            2 <= company.RemainingSovereignty.Value + company.RemainingTerritory.Value;

        protected override int  GetActivePool(Company company) => company.UseTerritory() + company.UseTreasure();
        protected override Stat StatToImprove(Company company) => company.Might;
    }
}