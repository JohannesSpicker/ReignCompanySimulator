using TeppichsAttributes.Attributes;
using UnityEngine;

namespace TeppichsAttributes.Visualizers
{
    public abstract class AttributeVisualizer : MonoBehaviour
    {
        private Attribute attribute;

        private void OnEnable()
        {
            attribute.OnAttributeValueChanged += Refresh;
            Refresh(attribute.Value);
        }

        private void OnDisable() => attribute.OnAttributeValueChanged -= Refresh;

        public void Initialize(Attribute att)
        {
            attribute                   =  att;
            att.OnAttributeValueChanged += Refresh;
            Refresh(att.Value);
        }

        protected abstract void Refresh(float value);
    }
}