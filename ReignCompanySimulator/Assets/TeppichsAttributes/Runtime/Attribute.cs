using System;
using System.Collections.Generic;
using System.Linq;
using TeppichsAttributes.Runtime;
using UnityEngine;

namespace TeppichsAttributes
{
    public abstract class Attribute
    {
        protected float         baseValue;
        private   AttributeData data;

        protected Attribute(AttributeData data, float baseValue)
        {
            this.data      = data;
            this.baseValue = baseValue;
            Value          = baseValue;
        }

        #region Value

        private float value;

        public float Value
        {
            get => value;
            protected set
            {
                if (data.usesMinValue && data.usesMaxValue)
                    this.value = Mathf.Clamp(value, data.minValue, data.maxValue);
                else if (data.usesMinValue)
                    this.value = Mathf.Max(value, data.minValue);
                else if (data.usesMaxValue)
                    this.value = Mathf.Min(value, data.maxValue);
                else
                    this.value = value;
            }
        }

        public event Action<float> OnAttributeValueChanged;
        public event Action<float> OnAttributeValueChangedByAmount;

        protected void InvokeOnAttributeValueChanged() => OnAttributeValueChanged?.Invoke(Value);

        protected void InvokeOnAttributeValueChangedByAmount(float before) =>
            OnAttributeValueChangedByAmount?.Invoke(Value - before);

        #endregion
    }

    public class Stat : Attribute
    {
        protected List<Modifier> modifiers = new();

        public Stat(AttributeData data, float baseValue) : base(data, baseValue) { }

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

        #region Calculate Value

        protected virtual float BaseValue => baseValue;

        private void RecalculateValue()
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

    /// <summary>
    ///     refers to attributes it derives from
    ///     gets baseValue via the related attributes, then adds its own modifiers
    /// </summary>
    public abstract class DerivedStat : Stat
    {
        private List<Stat> factors = new();

        //need to get the concrete attributes into here for calculations
        public DerivedStat(DerivedStatData data, float baseValue, List<Stat> factors) : base(data, baseValue) { }

        protected override float BaseValue =>
            factors.Select(factor => factor.Value).Aggregate(1f, (acc, val) => acc * val);

        //recalculate when factors change
    }

    /// <summary>
    ///     modifiers are added immediately, not stored
    /// </summary>
    public class Resource : Attribute
    {
        private Attribute    maxAttribute;
        private ResourceData resourceData;

        public Resource(ResourceData data, float baseValue, Attribute maxAttribute) : base(data, baseValue)
        {
            resourceData                                 =  data;
            this.maxAttribute                            =  maxAttribute;
            maxAttribute.OnAttributeValueChangedByAmount += ReactToMaxAttributeChange;
        }

        ~Resource() { maxAttribute.OnAttributeValueChangedByAmount -= ReactToMaxAttributeChange; }

        private void ReactToMaxAttributeChange(float change)
        {
            if ((0         < change && resourceData.increaseValueWithMaxAttribute)
                || (change < 0      && resourceData.decreaseValueWithMaxAttribute))
                AddToValue(change);
        }

        public bool TrySpend(float amount)
        {
            if (Value < amount)
                return false;

            AlterValue(-amount, Addition);

            return true;
        }

        public void Gain(float  amount) => AlterValue(amount,  Addition);
        public void Spend(float amount) => AlterValue(-amount, Addition);

        public void ModifyValue(Modifier modifier) =>
            AlterValue(modifier.value, modifier.type is ModifierType.Flat ? Addition : Multiplication);

        private void AddToValue(float amount) => AlterValue(amount, Addition);

        private void AlterValue(float factor, Action<float> alteration)
        {
            float before = Value;

            alteration.Invoke(factor);

            if (maxAttribute != null)
                Value = Mathf.Min(Value, maxAttribute.Value);

            InvokeOnAttributeValueChanged();
            InvokeOnAttributeValueChangedByAmount(before);
        }

        private void Addition(float       amount) => Value += amount;
        private void Multiplication(float factor) => Value *= factor;
    }
}