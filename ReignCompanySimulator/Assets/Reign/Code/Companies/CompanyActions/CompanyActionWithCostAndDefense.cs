namespace Reign.Companies.CompanyActions
{
    public abstract class CompanyActionWithCostAndDefense : CompanyActionWithCost
    {
        protected abstract int GetDefendingPool(Company company);
    }
}