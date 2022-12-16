using System;
using TeppichsAttributes.Data;
using TeppichsTools.Data;

namespace TeppichsAttributes.Attributes
{
    [Serializable]
    public class AttributeContainer
    {
        public StatDictionary        stats        = new();
        public DerivedStatDictionary derivedStats = new();
        public ResourceDictionary    resources    = new();
    }

    [Serializable]
    public class StatDictionary : UnitySerializedDictionary<AttributeData, Stat> { }

    [Serializable]
    public class DerivedStatDictionary : UnitySerializedDictionary<DerivedStatData, DerivedStat> { }

    [Serializable]
    public class ResourceDictionary : UnitySerializedDictionary<AttributeData, Resource> { }
}