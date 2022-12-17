using TeppichsAttributes.Data;

namespace TeppichsAttributes.Builders
{
    public class ResourceDataBuilder : BaseAttributeDataBuilder<ResourceData>
    {
        private bool decreaseValueOnMaxAttributeChange;

        private bool          increaseValueOnMaxAttributeChange;
        private AttributeData maxAttribute;

        public ResourceDataBuilder WithMaxAttribute(AttributeData attributeData)
        {
            maxAttribute = attributeData;

            return this;
        }

        public ResourceDataBuilder DoesIncreaseValueOnMaxAttributeChange()
        {
            increaseValueOnMaxAttributeChange = true;

            return this;
        }

        public ResourceDataBuilder DoesDecreaseValueOnMaxAttributeChange()
        {
            decreaseValueOnMaxAttributeChange = true;

            return this;
        }

        protected override ResourceData Build()
        {
            ResourceData resourceData = base.Build();

            resourceData.maxAttribute                      = maxAttribute;
            resourceData.increaseValueOnMaxAttributeChange = increaseValueOnMaxAttributeChange;
            resourceData.decreaseValueOnMaxAttributeChange = decreaseValueOnMaxAttributeChange;

            return resourceData;
        }
    }
}