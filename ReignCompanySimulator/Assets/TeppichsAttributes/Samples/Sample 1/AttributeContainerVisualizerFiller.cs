using TeppichsAttributes.Attributes;
using TeppichsAttributes.Data;
using TeppichsAttributes.Visualizers;
using TeppichsTools.Behavior;
using UnityEngine;

namespace TeppichsAttributes.Samples.Sample_1
{
    public class AttributeContainerVisualizerFiller : ComponentCacher<AttributeContainerVisualizer>
    {
        [SerializeField] private AttributeConfig config;
        private AttributeContainer container = new();

        private void Start()
        {
            config.ApplyConfig(container);
            myComponent.Initialize(container);
        }
    }

    public class ComponentCacher<T> : TransformCachingMonoBehaviour
    {
        [SerializeField, HideInInspector] protected T myComponent;

        public override void OnValidate()
        {
            base.OnValidate();
            myComponent = GetComponent<T>();
        }
    }
}