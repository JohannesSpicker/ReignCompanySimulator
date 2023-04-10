using TeppichsAttributes.Attributes;
using TeppichsAttributes.Data;

namespace Reign.Companies
{
    public static class CompanyCreator
    {
        public static Company CreateCompany(AttributeConfig qualityConfig, QualityDataHolder qualityDataHolder)
        {
            Company company = new(CreateAttributeContainer(qualityConfig), qualityDataHolder);

            var might = 1;
            var treasure = 2;
            var influence = 3;
            var territory = 4;
            var sovereignty = 5;

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