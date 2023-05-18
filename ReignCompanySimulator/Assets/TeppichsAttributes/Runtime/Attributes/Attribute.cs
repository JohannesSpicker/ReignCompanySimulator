using System;
using TeppichsAttributes.Data;
using TeppichsAttributes.Modifiers;
using UnityEngine;

namespace TeppichsAttributes.Attributes
{
    public abstract class Attribute
    {
        public readonly AttributeData data;

        protected Attribute(AttributeData data, float baseValue)
        {
            this.data = data;
            BaseValue = baseValue;
            Value     = baseValue;
        }

        #region Value

        public float BaseValue { get => baseValue; protected set => baseValue = ClampNewValue(value); }
        public float Value     { get => value;     protected set => this.value = ClampNewValue(value); }

        private float baseValue;
        private float value;

        private float ClampNewValue(float newValue)
        {
            if (data.usesMinValue && data.usesMaxValue)
                return Mathf.Clamp(newValue, data.minValue, data.maxValue);

            if (data.usesMinValue)
                return Mathf.Max(newValue, data.minValue);

            if (data.usesMaxValue)
                return Mathf.Min(newValue, data.maxValue);

            return newValue;
        }

        public bool IsAtMaxValue => data.usesMaxValue && Value == data.maxValue;
        public bool IsAtMinValue => data.usesMinValue && Value == data.minValue;

        #endregion

        #region Modify

        /// <summary>
        ///     Adds a modifier to the attribute and recalculates the value.
        ///     Stats use this for temporary modification i.e. equipment.
        ///     Resources use this for permanent modification.
        /// </summary>
        public abstract void AddModifier(Modifier modifier);

        /// <summary>
        ///     Adds an amount to the baseValue of the attribute.
        ///     Use this for permanent modifications i.e. leveling up.
        /// </summary>
        public abstract void AddToBaseValue(float amount);

        #endregion

        #region Events

        public event Action<float> OnAttributeValueChanged;
        public event Action<float> OnAttributeValueChangedByAmount;

        protected void InvokeOnAttributeValueChanged() => OnAttributeValueChanged?.Invoke(Value);

        protected void InvokeOnAttributeValueChangedByAmount(float before) =>
            OnAttributeValueChangedByAmount?.Invoke(Value - before);

        #endregion
    }
}