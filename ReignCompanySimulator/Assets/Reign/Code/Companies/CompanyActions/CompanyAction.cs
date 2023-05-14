using System.Collections;

namespace Reign.Companies.CompanyActions
{
    public abstract class CompanyAction
    {
        public abstract bool IsViable(Company company);

        public abstract IEnumerator ProcessAction(Company activeCompany, Company defendingCompany);
    }
}