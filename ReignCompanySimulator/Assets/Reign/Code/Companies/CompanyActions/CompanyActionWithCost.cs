using UnityEngine;

namespace Reign.Companies.CompanyActions
{
    public abstract class CompanyActionWithCost : CompanyAction
    {
        protected abstract string ActionName { get; }

        protected abstract int GetActivePool(Company company);

        protected void LogAction(Company activeCompany, string extraInfo, bool success) =>
            Debug.Log($"Company {activeCompany.name} did {ActionName} {extraInfo} with result {(success ? "success" : "failure")}");
    }
}