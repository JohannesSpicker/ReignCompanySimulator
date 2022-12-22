using System.Collections.Generic;
using TeppichsAttributes.Attributes;

namespace TeppichsAttributes.Visualizers.Primitives
{
    public abstract class DerivedStatVisualizer : BaseStatVisualizer<DerivedStat>
    {
        protected override void DisplayAttribute(DerivedStat derivedStat)
        {
            base.DisplayAttribute(derivedStat);
            DisplayFactors(derivedStat.Factors);
        }

        protected override void Refresh(float value)
        {
            base.Refresh(value);
            DisplayFactors(attribute.Factors);
        }

        protected abstract void DisplayFactors(IEnumerable<Stat> factors);
    }
}