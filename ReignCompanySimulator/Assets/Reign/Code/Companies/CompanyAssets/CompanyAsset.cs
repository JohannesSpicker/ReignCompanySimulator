namespace Reign.Companies.CompanyAssets
{
    public abstract class CompanyAsset
    {
        public abstract void Apply();
        public abstract void Remove();
    }

    public abstract class CircumstancialCompanyAsset : CompanyAsset
    {
        public abstract bool CheckCircumstance();
        
        //some need to check circumstance when using
        //others might need to have circumstance already set
    }

    public abstract class OneUseCircumstancialCompanyAsset : CircumstancialCompanyAsset
    {   
        //remove after use
    }
}