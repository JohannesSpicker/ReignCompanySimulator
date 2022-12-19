using TeppichsAttributes.Attributes;
using UnityEngine;

namespace TeppichsAttributes.Visualizers
{
    public abstract class AttributeVisualizer<T> : MonoBehaviour where T : Attribute
    {
        private T attribute;

        private void OnEnable()
        {
            attribute.OnAttributeValueChanged += Refresh;
            Refresh(attribute.Value);
        }

        private void OnDisable() => attribute.OnAttributeValueChanged -= Refresh;

        public void Initialize(T att)
        {
            attribute                   =  att;
            att.OnAttributeValueChanged += Refresh;
            Refresh(att.Value);
        }

        protected abstract void Refresh(float value);
    }
}