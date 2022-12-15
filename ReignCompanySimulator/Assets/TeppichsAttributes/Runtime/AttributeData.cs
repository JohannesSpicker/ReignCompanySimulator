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
        [SerializeField] public  string inGameName; //TODO: exchange for localizedString
        [SerializeField] private Sprite sprite;

        [SerializeField] public bool  usesMaxValue;
        [SerializeField] public float maxValue;
        [SerializeField] public bool  usesMinValue;
        [SerializeField] public float minValue;
    }

    [Serializable]
    public class DerivedAttributeData : AttributeData
    {
        [SerializeField] private List<AttributeData> factors = new();
    }
}