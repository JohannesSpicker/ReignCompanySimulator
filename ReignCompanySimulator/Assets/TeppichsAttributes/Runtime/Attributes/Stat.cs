using System.Collections.Generic;
using System.Linq;
using TeppichsAttributes.Data;
using TeppichsAttributes.Modifiers;

namespace TeppichsAttributes.Attributes
{
    public class Stat : Attribute
    {
        private readonly List<Modifier> modifiers = new();

        public Stat(AttributeData data, float baseValue) : base(data, baseValue) { }

        public IEnumerable<Modifier> Modifiers => modifiers;

        public override void AddToBaseValue(float amount)
        {
            BaseValue += amount;
            RecalculateValue();
        }

        #region Modifiers

        public override void AddModifier(Modifier modifier)
        {
            modifiers.Add(modifier);
            RecalculateValue();
        }

        public void RemoveModifier(Modifier modifier)
        {
            modifiers.Remove(modifier);
            RecalculateValue();
        }

        public void RemoveAllModifiersFromSource(object source)
        {
            modifiers.RemoveAll(mod => mod.source == source);
            RecalculateValue();
        }

        #region Calculate Value

        protected virtual float BaseValueUsedForCalculations => BaseValue;

        protected void RecalculateValue()
        {
            float before = Value;

            Value = CalculateFinalValue();

            InvokeOnAttributeValueChanged();
            InvokeOnAttributeValueChangedByAmount(before);

            float CalculateFinalValue()
            {
                float finalValue = BaseValueUsedForCalculations;

                finalValue += modifiers.Where(mod => mod.type == ModifierType.Flat).Sum(mod => mod.value);
                finalValue *= 1 + modifiers.Where(mod => mod.type == ModifierType.PercentAdd).Sum(mod => mod.value);

                foreach (Modifier mod in modifiers.Where(s => s.type == ModifierType.PercentMult))
                    finalValue *= 1 + mod.value;

                return finalValue;
            }
        }

        #endregion

        #endregion
    }
}