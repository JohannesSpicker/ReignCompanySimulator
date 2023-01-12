using Sirenix.OdinInspector;
using TeppichsAttributes.Attributes;
using TeppichsAttributes.Data;
using TeppichsAttributes.Modifiers;
using TeppichsAttributes.Visualizers;
using TeppichsTools.Behavior;
using UnityEngine;

namespace TeppichsAttributes.Samples.Sample_1
{
    public class AttributeContainerVisualizerFiller : ComponentCacher<AttributeContainerVisualizer>
    {
        [SerializeField] private AttributeConfig     config;
        [SerializeField] private AttributeDataHolder attributeDataHolder;

        [ShowInInspector] private AttributeContainer container = new();

        private void Start()
        {
            config.ApplyConfig(container);
            myComponent.Initialize(container);
        }

        [Button]
        private void AddModifierToAttribute(AttributeDataHolder.AttributeName attributeName, ModifierType modifierType,
                                            float                             amount) => container
            .GetAttribute(attributeDataHolder
                              .GetAttributeData(attributeName))
            .AddModifier(new Modifier(amount, modifierType, this));

        [Button]
        private void ResetModifiers()
        {
            container.stats[attributeDataHolder.statZero].RemoveAllModifiersFromSource(this);
            container.stats[attributeDataHolder.statOne].RemoveAllModifiersFromSource(this);
            container.derivedStats[attributeDataHolder.derivedStatZero].RemoveAllModifiersFromSource(this);
            container.derivedStats[attributeDataHolder.derivedStatOne].RemoveAllModifiersFromSource(this);
            container.resources[attributeDataHolder.resourceZero].ResetToBaseValue();
            container.resources[attributeDataHolder.resourceOne].ResetToBaseValue();
        }
    }

    public class ComponentCacher<T> : TransformCachingMonoBehaviour
    {
        [SerializeField, HideInInspector]
        protected T myComponent;

        public override void OnValidate()
        {
            base.OnValidate();
            myComponent = GetComponent<T>();
        }
    }
}