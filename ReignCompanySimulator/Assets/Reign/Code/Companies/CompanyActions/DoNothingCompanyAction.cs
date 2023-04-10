using System.Collections;
using UnityEngine;

namespace Reign.Companies.CompanyActions
{
    public class DoNothingCompanyAction : CompanyAction
    {
        public override IEnumerator ProcessAction()
        {
            Debug.Log("Company action did nothing.");

            yield return null;
        }
    }
}