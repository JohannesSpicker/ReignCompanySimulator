using System;
using UnityEngine;

namespace TeppichsAttributes.Data
{
    [Serializable, CreateAssetMenu(menuName = "TeppichsAttributes/ResourceData", order = 3)]
    public sealed class ResourceData : AttributeData
    {
        [SerializeField] public AttributeData maxAttribute;

        [SerializeField] public bool increaseValueOnMaxAttributeChange;
        [SerializeField] public bool decreaseValueOnMaxAttributeChange;
    }
}