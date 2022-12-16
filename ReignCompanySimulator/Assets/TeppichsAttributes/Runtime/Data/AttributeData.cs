using System;
using UnityEngine;

namespace TeppichsAttributes.Data
{
    /// <summary>
    ///     This is a flyweight.
    /// </summary>
    [Serializable, CreateAssetMenu(menuName = "TeppichsAttributes/AttributeData", order = 1)]
    public class AttributeData : ScriptableObject
    {
        [SerializeField] public string inGameName; //TODO: exchange for localizedString
        [SerializeField] public Sprite sprite;

        [SerializeField] public bool  usesMaxValue;
        [SerializeField] public float maxValue;
        [SerializeField] public bool  usesMinValue;
        [SerializeField] public float minValue;
    }
}