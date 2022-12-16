using System;
using UnityEngine;

namespace TeppichsAttributes.Data
{
    [Serializable, CreateAssetMenu(menuName = "TeppichsAttributes/ResourceData", order = 3)]
    public class ResourceData : AttributeData
    {
        [SerializeField] public AttributeData maxAttribute;

        [SerializeField] public bool increaseValueInMaxAttributeChange;
        [SerializeField] public bool decreaseValueOnMaxAttributeChange;
    }
}