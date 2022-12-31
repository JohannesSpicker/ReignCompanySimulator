using System;
using TeppichsAttributes.Data;
using UnityEngine;

namespace TeppichsAttributes.Attributes
{
    public abstract class Attribute
    {
        public readonly float         baseValue;
        public readonly AttributeData data;

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
}