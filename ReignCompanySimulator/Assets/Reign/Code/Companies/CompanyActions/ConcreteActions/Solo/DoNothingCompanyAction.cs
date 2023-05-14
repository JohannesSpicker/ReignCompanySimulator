using System.Collections;
using UnityEngine;

namespace Reign.Companies.CompanyActions
{
    public sealed class DoNothingCompanyAction : CompanyAction
    {
        public override bool IsViable(Company _) => true;

        public override IEnumerator ProcessAction(Company activeCompany, Company _)
        {
            Debug.Log($"Company {activeCompany.name} did nothing.");

            yield return null;
        }
    }
}