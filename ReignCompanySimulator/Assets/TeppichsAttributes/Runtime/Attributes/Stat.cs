using System.Collections.Generic;
using System.Linq;
using TeppichsAttributes.Data;
using TeppichsAttributes.Modifiers;

namespace TeppichsAttributes.Attributes
{
    public class Stat : Attribute
    {
        private readonly List<Modifier> modifiers = new();

        public IEnumerable<Modifier> Modifiers => modifiers;

        public Stat(AttributeData data, float baseValue) : base(data, baseValue) { }

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

        protected virtual float BaseValue => baseValue;

        protected void RecalculateValue()
        {
            float before = Value;

            Value = CalculateFinalValue();

            InvokeOnAttributeValueChanged();
            InvokeOnAttributeValueChangedByAmount(before);

            float CalculateFinalValue()
            {
                float finalValue = BaseValue;

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