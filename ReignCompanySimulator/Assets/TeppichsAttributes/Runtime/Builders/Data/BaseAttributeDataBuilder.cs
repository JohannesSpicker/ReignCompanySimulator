using TeppichsAttributes.Data;
using TeppichsTools.Creation;
using UnityEngine;

namespace TeppichsAttributes.Builders.Data
{
    public abstract class BaseAttributeDataBuilder<T> : Builder<T> where T : AttributeData
    {
        private string inGameName;
        private float  maxValue;
        private float  minValue;
        private Sprite sprite;
        private bool   usesMaxValue;
        private bool   usesMinValue;

        public BaseAttributeDataBuilder<T> WithName(string name)
        {
            inGameName = name;

            return this;
        }

        public BaseAttributeDataBuilder<T> WithSprite(Sprite spr)
        {
            sprite = spr;

            return this;
        }

        public BaseAttributeDataBuilder<T> WithMaxValue(float max)
        {
            maxValue     = max;
            usesMaxValue = true;

            return this;
        }

        public BaseAttributeDataBuilder<T> WithMinValue(float min)
        {
            minValue     = min;
            usesMinValue = true;

            return this;
        }

        protected override T Build()
        {
            T attributeData = ScriptableObject.CreateInstance<T>();

            attributeData.inGameName   = inGameName;
            attributeData.sprite       = sprite;
            attributeData.usesMaxValue = usesMaxValue;
            attributeData.maxValue     = maxValue;
            attributeData.usesMinValue = usesMinValue;
            attributeData.minValue     = minValue;

            return attributeData;
        }
    }
}