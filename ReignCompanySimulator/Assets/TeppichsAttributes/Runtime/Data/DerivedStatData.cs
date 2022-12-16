using System;
using System.Collections.Generic;
using UnityEngine;

namespace TeppichsAttributes.Data
{
    [Serializable, CreateAssetMenu(menuName = "TeppichsAttributes/DerivedStatData", order = 2)]
    public sealed class DerivedStatData : AttributeData
    {
        [SerializeField] public List<AttributeData> factors = new();
    }
}