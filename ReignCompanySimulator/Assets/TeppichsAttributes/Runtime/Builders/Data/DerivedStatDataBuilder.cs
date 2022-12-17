using System.Collections.Generic;
using TeppichsAttributes.Data;

namespace TeppichsAttributes.Builders
{
    public class DerivedStatDataBuilder : BaseAttributeDataBuilder<DerivedStatData>
    {
        private readonly List<AttributeData> factors = new();

        public DerivedStatDataBuilder WithFactor(AttributeData factor)
        {
            factors.Add(factor);

            return this;
        }

        public DerivedStatDataBuilder WithFactors(List<AttributeData> facs)
        {
            factors.AddRange(facs);

            return this;
        }

        protected override DerivedStatData Build()
        {
            DerivedStatData derivedStatData = base.Build();

            derivedStatData.factors = factors;

            return derivedStatData;
        }
    }
}