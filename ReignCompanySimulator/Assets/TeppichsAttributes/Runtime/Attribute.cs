using System;
using System.Collections.Generic;
using System.Linq;
using TeppichsAttributes.Runtime;

namespace TeppichsAttributes
{
    public abstract class Attribute
    {
        protected float         baseValue;
        private   AttributeData data;

        public Attribute(AttributeData data, float baseValue)
        {
            this.data      = data;
            this.baseValue = baseValue;
            Value          = baseValue;
        }

        #region Value

        public float Value { get; protected set; }

        public event Action<float> OnAttributeValueChanged;
        public event Action<float> OnAttributeValueChangedByAmount;

        protected void InvokeOnAttributeValueChanged() => OnAttributeValueChanged?.Invoke(Value);

        protected void InvokeOnAttributeValueChangedByAmount(float before) =>
            OnAttributeValueChangedByAmount?.Invoke(Value - before);

        #endregion
    }

    public class StatAttribute : Attribute
    {
        protected List<Modifier> modifiers = new();

        public StatAttribute(AttributeData data, float baseValue) : base(data, baseValue) { }

        #region Modifiers

        public void AddModifier(Modifier modifier)
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

        protected void RecalculateValue()
        {
            float before = Value;

            Value = CalculateFinalValue();

            InvokeOnAttributeValueChanged();
            InvokeOnAttributeValueChangedByAmount(before);

            float CalculateFinalValue()
            {
                float finalValue = baseValue;

                finalValue += modifiers.Where(mod => mod.type == ModifierType.Flat).Sum(mod => mod.value);
                finalValue *= 1 + modifiers.Where(mod => mod.type == ModifierType.PercentAdd).Sum(mod => mod.value);

                foreach (Modifier mod in modifiers.Where(s => s.type == ModifierType.PercentMult))
                    finalValue *= 1 + mod.value;

                return finalValue;
            }
        }

        #endregion
    }

    public abstract class DerivedStatAttribute : StatAttribute
    {
        //refers to attributes it derives from
        //gets baseValue via the related attributes, then adds its own modifiers

        private List<StatAttribute> factors = new();

        
        //need to get the concrete attributes into here for calculations
        protected DerivedStatAttribute(DerivedAttributeData data, float baseValue, List<StatAttribute> factors) :
            base(data, baseValue)
        {
            
            
        }
        
        //when recalculating value with local modifiers use the values of the factors as base (just multiply them for now)
    }

    /// <summary>
    ///     modifiers are added immediately, not stored
    /// </summary>
    public class Resource : Attribute
    {
        public Resource(AttributeData data, float baseValue) : base(data, baseValue) { }

        public bool TrySpend(float amount)
        {
            if (Value < amount)
                return false;

            AlterValue(-amount);

            return true;
        }

        public void Gain(float  amount) => AlterValue(amount);
        public void Spend(float amount) => AlterValue(-amount);

        public void ModifyValue(Modifier modifier)
        {
            if (modifier.type is ModifierType.Flat)
                AlterValue(modifier.value);
            else if (modifier.type is ModifierType.PercentAdd or ModifierType.PercentMult)
                MultiplyValue(modifier.value);
        }

        private void AlterValue(float amount)
        {
            float before = Value;

            Value += amount;

            InvokeOnAttributeValueChanged();
            InvokeOnAttributeValueChangedByAmount(before);
        }

        private void MultiplyValue(float factor)
        {
            float before = Value;

            Value *= factor;

            InvokeOnAttributeValueChanged();
            InvokeOnAttributeValueChangedByAmount(before);
        }
    }
}