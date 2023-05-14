using System.Collections;

namespace Reign.Companies.CompanyActions
{
    public sealed class CompanyActionAttack : CompanyActionWithCostAndDefense
    {
        public override    bool        IsViable(Company      company)                                 => throw new System.NotImplementedException();
        public override    IEnumerator ProcessAction(Company activeCompany, Company defendingCompany) { yield return null; }
        protected override string ActionName => "Attack";
        protected override int         GetActivePool(Company    company) => throw new System.NotImplementedException();
        protected override int         GetDefendingPool(Company company) => throw new System.NotImplementedException();
    }
}