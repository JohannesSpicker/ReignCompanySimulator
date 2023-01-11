using System;
using TeppichsAttributes.Data;
using TeppichsAttributes.Modifiers;
using UnityEngine;

namespace TeppichsAttributes.Attributes
{
    /// <summary>
    ///     modifiers are added immediately, not stored
    /// </summary>
    public sealed class Resource : Attribute
    {
        public readonly Attribute    maxAttribute;
        public readonly ResourceData resourceData;

        public Resource(ResourceData data, float baseValue, Attribute maxAttribute = null) : base(data, baseValue)
        {
            resourceData      = data;
            this.maxAttribute = maxAttribute;

            if (maxAttribute is { })
                maxAttribute.OnAttributeValueChangedByAmount += ReactToMaxAttributeChange;
        }

        ~Resource()
        {
            if (maxAttribute is { })
                maxAttribute.OnAttributeValueChangedByAmount -= ReactToMaxAttributeChange;
        }

        private void ReactToMaxAttributeChange(float change)
        {
            if ((0         < change && resourceData.increaseValueOnMaxAttributeChange)
                || (change < 0      && resourceData.decreaseValueOnMaxAttributeChange))
                AddToValue(change);
            else if (maxAttribute.Value < Value)
                AddToValue(0f);
        }

        public bool TrySpend(float amount)
        {
            if (Value < amount)
                return false;

            AddToValue(-amount);

            return true;
        }

        public void Gain(float  amount) => AddToValue(amount);
        public void Spend(float amount) => AddToValue(-amount);

        public override void AddModifier(Modifier modifier) =>
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

        public void ResetToBaseValue() => AlterValue(baseValue - Value, Addition);
    }
}