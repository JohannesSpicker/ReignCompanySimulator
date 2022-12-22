using TeppichsAttributes.Attributes;
using UnityEngine;

namespace TeppichsAttributes.Visualizers
{
    public abstract class AttributeVisualizer<T> : MonoBehaviour where T : Attribute
    {
        private T attribute;

        private void OnEnable()
        {
            if (attribute is null)
                return;

            attribute.OnAttributeValueChanged += Refresh;
            Refresh(attribute.Value);
        }

        private void OnDisable()
        {
            if (attribute is null)
                return;

            attribute.OnAttributeValueChanged -= Refresh;
        }

        public void Initialize(T att)
        {
            attribute = att;
            att.OnAttributeValueChanged += Refresh;
            DisplayAttribute(att);
        }

        protected virtual void Refresh(float value)
        {
            DisplayValue(value);
        }

        protected virtual void DisplayAttribute(Attribute att)
        {
            DisplayName(att.data.inGameName);
            DisplaySprite(att.data.sprite);
            DisplayMinValue(att.data.minValue);
            DisplayMaxValue(att.data.maxValue);
            DisplayBaseValue(att.baseValue);
            DisplayValue(att.Value);
        }

        protected abstract void DisplayName(string inGameName);
        protected abstract void DisplaySprite(Sprite sprite);
        protected abstract void DisplayMinValue(float minValue);
        protected abstract void DisplayMaxValue(float maxValue);
        protected abstract void DisplayBaseValue(float baseValue);
        protected abstract void DisplayValue(float value);
    }
}