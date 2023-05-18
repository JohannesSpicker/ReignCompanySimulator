using Reign.Contests.Dice;

namespace Reign.Companies.CompanyActions
{
    public abstract class CompanyActionWithCostAndDefense : CompanyActionWithCost
    {
        protected abstract DicePool GetDefendingPool(Company company);
    }
}