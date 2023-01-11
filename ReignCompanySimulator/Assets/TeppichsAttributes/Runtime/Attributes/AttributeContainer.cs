using System;
using TeppichsAttributes.Data;
using TeppichsTools.Data;

namespace TeppichsAttributes.Attributes
{
    [Serializable]
    public sealed class AttributeContainer
    {
        public StatDictionary        stats        = new();
        public DerivedStatDictionary derivedStats = new();
        public ResourceDictionary    resources    = new();

        public Attribute GetAttribute(AttributeData attributeData)
        {
            if (attributeData is ResourceData resourceData)
                return GetResource(resourceData);

            if (attributeData is DerivedStatData derivedStatData)
                return GetDerivedStat(derivedStatData);

            return GetStat(attributeData);
        }

        public Stat        GetStat(AttributeData          attributeData)   => stats[attributeData];
        public DerivedStat GetDerivedStat(DerivedStatData derivedStatData) => derivedStats[derivedStatData];
        public Resource    GetResource(ResourceData       resourceData)    => resources[resourceData];
    }

    [Serializable]
    public class StatDictionary : UnitySerializedDictionary<AttributeData, Stat> { }

    [Serializable]
    public class DerivedStatDictionary : UnitySerializedDictionary<DerivedStatData, DerivedStat> { }

    [Serializable]
    public class ResourceDictionary : UnitySerializedDictionary<ResourceData, Resource> { }
}