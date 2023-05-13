using TeppichsAttributes.Attributes;
using TeppichsAttributes.Data;

namespace Reign.Companies
{
    public static class CompanyCreator
    {
        private static int companyIndex;

        public static Company CreateCompany(AttributeConfig qualityConfig, QualityDataHolder qualityDataHolder)
        {
            Company company =
                new(CreateAttributeContainer(qualityConfig), qualityDataHolder) { name = $"Company {companyIndex++}" };

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

        private static AttributeContainer CreateAttributeContainer(AttributeConfig baseQualityConfig)
        {
            AttributeContainer container = new();
            baseQualityConfig.ApplyConfig(container);

            return container;
        }
    }
}