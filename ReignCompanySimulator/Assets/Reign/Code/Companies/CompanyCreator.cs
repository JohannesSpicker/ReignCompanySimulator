using TeppichsAttributes.Attributes;
using TeppichsAttributes.Data;

namespace Reign.Companies
{
    public class CompanyCreator
    {
        private AttributeConfig   baseQualityConfig; //TODO: get this reference
        private QualityDataHolder qualityDataHolder; //TODO: get this reference

        private AttributeContainer CreateAttributeContainer()
        {
            AttributeContainer container = new();
            baseQualityConfig.ApplyConfig(container);

            return container;
        }

        private Company CreateCompany(AttributeContainer container)
        {
            Company company = new(CreateAttributeContainer(), qualityDataHolder);

            int might       = 1;
            int treasure    = 2;
            int influence   = 3;
            int territory   = 4;
            int sovereignty = 5;

            company.Might.AddToBaseValue(might);
            company.Treasure.AddToBaseValue(treasure);
            company.Influence.AddToBaseValue(influence);
            company.Territory.AddToBaseValue(territory);
            company.Sovereignty.AddToBaseValue(sovereignty);

            return company;
        }
    }
}