using System;
using System.Collections.Generic;
using UnityEngine;

namespace TeppichsAttributes
{
    /// <summary>
    ///     This is a flyweight.
    /// </summary>
    [Serializable]
    public class AttributeData : ScriptableObject
    {
        [SerializeField] public string inGameName; //TODO: exchange for localizedString
        [SerializeField] public Sprite sprite;

        [SerializeField] public bool  usesMaxValue;
        [SerializeField] public float maxValue;
        [SerializeField] public bool  usesMinValue;
        [SerializeField] public float minValue;
    }

    [Serializable]
    public class DerivedStatData : AttributeData
    {
        [SerializeField] public List<AttributeData> factors = new();
    }

    [Serializable]
    public class ResourceData : AttributeData
    {
        [SerializeField] public AttributeData maxAttribute;

        [SerializeField] public bool increaseValueWithMaxAttribute;
        [SerializeField] public bool decreaseValueWithMaxAttribute;
    }
}