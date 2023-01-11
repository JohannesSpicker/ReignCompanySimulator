using System;
using TeppichsAttributes.Data;
using UnityEngine;

namespace TeppichsAttributes.Samples.Sample_1
{
    [Serializable, CreateAssetMenu(menuName = "TeppichsAttributes/Sample/AttributeDataHolder", order = 2)]
    public class AttributeDataHolder : ScriptableObject
    {
        public enum AttributeName
        {
            StatZero,
            StatOne,
            DerivedStatZero,
            DerivedStatOne,
            ResourceZero,
            ResourceOne
        }

        [SerializeField] public AttributeData statZero;
        [SerializeField] public AttributeData statOne;

        [SerializeField] public DerivedStatData derivedStatZero;
        [SerializeField] public DerivedStatData derivedStatOne;

        [SerializeField] public ResourceData resourceZero;
        [SerializeField] public ResourceData resourceOne;

        public AttributeData GetAttribute(AttributeName attributeName) => attributeName switch
        {
            AttributeName.StatZero => statZero,
            AttributeName.StatOne => statOne,
            AttributeName.DerivedStatZero => derivedStatZero,
            AttributeName.DerivedStatOne => derivedStatOne,
            AttributeName.ResourceZero => resourceZero,
            AttributeName.ResourceOne => resourceOne,
            _ => throw new ArgumentOutOfRangeException(nameof(attributeName), attributeName, null)
        };
    }
}