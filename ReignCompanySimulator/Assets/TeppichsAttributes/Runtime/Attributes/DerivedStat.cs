using System.Collections.Generic;
using System.Linq;
using TeppichsAttributes.Data;

namespace TeppichsAttributes.Attributes
{
    /// <summary>
    ///     refers to attributes it derives from
    ///     gets baseValue via the related attributes, then adds its own modifiers
    /// </summary>
    public class DerivedStat : Stat
    {
        private readonly List<Stat> factors;

        //need to get the concrete attributes into here for calculations
        public DerivedStat(DerivedStatData data, float baseValue, IEnumerable<Stat> factors) : base(data, baseValue)
        {
            this.factors = factors.ToList();
            
            foreach (Stat factor in this.factors)
                factor.OnAttributeValueChanged += ReactToFactorValueChange;
        }

        protected override float BaseValue =>
            factors.Select(factor => factor.Value).Aggregate(1f, (acc, val) => acc * val);

        ~DerivedStat()
        {
            foreach (Stat factor in factors)
                factor.OnAttributeValueChanged -= ReactToFactorValueChange;
        }

        private void ReactToFactorValueChange(float _) => RecalculateValue();
    }
}