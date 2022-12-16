using System;
using TeppichsTools.Data;

namespace TeppichsAttributes.Runtime
{
    [Serializable]
    public class AttributeContainer
    {
        public StatDictionary        stats        = new();
        public DerivedStatDictionary derivedStats = new();
        public ResourceDictionary    resources    = new();
    }

    [Serializable]
    public class StatDictionary : UnitySerializedDictionary<AttributeData, Attribute> { }

    [Serializable]
    public class DerivedStatDictionary : UnitySerializedDictionary<DerivedStatData, Attribute> { }

    [Serializable]
    public class ResourceDictionary : UnitySerializedDictionary<AttributeData, Attribute> { }
}