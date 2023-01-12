using System.Collections.Generic;
using System.Linq;
using TeppichsAttributes.Data;
using TeppichsTools.Math;

namespace TeppichsAttributes.Attributes
{
    /// <summary>
    ///     Refers to attributes it derives from
    ///     Gets baseValue via the related attributes, then adds its own modifiers.
    /// </summary>
    public sealed class DerivedStat : Stat
    {
        private readonly List<Stat> factors;
        public IEnumerable<Stat> Factors => factors;

        public DerivedStat(DerivedStatData data, IEnumerable<Stat> factors) : base(data, 0f)
        {
            this.factors = factors.ToList();

            foreach (Stat factor in this.factors)
                factor.OnAttributeValueChanged += ReactToFactorValueChange;
            
            RecalculateValue();
        }

        protected override float BaseValueUsedForCalculations => factors.Select(factor => factor.Value).Product();

        ~DerivedStat()
        {
            foreach (Stat factor in factors)
                factor.OnAttributeValueChanged -= ReactToFactorValueChange;
        }

        private void ReactToFactorValueChange(float _) => RecalculateValue();
    }
}