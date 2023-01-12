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

        private AttributeContainer FillContainer(AttributeContainer container)
        {
            int might       = 1;
            int treasure    = 2;
            int influence   = 3;
            int territory   = 4;
            int sovereignty = 5;

            //TODO: set qualities to values
            //TODO: build accessors to the qualities
            
            return container;
        }

        public Company CreateCompany() => new(FillContainer(CreateAttributeContainer()));
    }
}