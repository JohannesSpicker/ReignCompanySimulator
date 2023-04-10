using System.Collections;

namespace Reign.Companies.CompanyActions
{
    public abstract class CompanyAction
    {
        public abstract IEnumerator ProcessAction();
    }
}