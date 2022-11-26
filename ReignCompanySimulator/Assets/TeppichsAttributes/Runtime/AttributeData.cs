using System;
using UnityEngine;

namespace TeppichsAttributes
{
    /// <summary>
    /// This is a flyweight.
    /// </summary>
    [Serializable]
    public class AttributeData : ScriptableObject
    {
        [SerializeField] public  string inGameName;//exchange for localizedString
        [SerializeField] private Sprite sprite;

        [SerializeField] public bool  usesMaxValue;
        [SerializeField] public float maxValue;
        [SerializeField] public bool  usesMinValue;
        [SerializeField] public float minValue;
    }
}