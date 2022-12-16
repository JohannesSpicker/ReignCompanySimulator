using System;
using System.Collections.Generic;
using TeppichsTools.Data;
using UnityEngine;

namespace TeppichsAttributes.Runtime
{
    [CreateAssetMenu(menuName = "TeppichsAttributes/AttributeConfig", order = 0)]
    public class AttributeConfig : ScriptableObject
    {
        [SerializeField] public StatConfigDictionary        stats        = new();
        [SerializeField] public DerivedStatConfigDictionary derivedStats = new();
        [SerializeField] public ResourceConfigDictionary    resources    = new();

        public void ApplyConfig(AttributeContainer container)
        {
            container.stats.Clear();
            container.derivedStats.Clear();
            container.resources.Clear();

            foreach (KeyValuePair<AttributeData, float> stat in stats)
                container.stats[stat.Key] = new Stat(stat.Key, stat.Value);

            //  foreach (KeyValuePair<DerivedStatData, float> derivedStat in derivedStats)
            //      container.derivedStats[derivedStat.Key] = new DerivedStat(derivedStat.Key, derivedStat.Value);

            foreach (KeyValuePair<ResourceData, float> resource in resources)
            {
                Attribute maxAttribute = container.stats[resource.Key.maxAttribute];//TODO: needs to support maxAttribute being a derivedStat
                container.resources[resource.Key] = new Resource(resource.Key, resource.Value, maxAttribute);
            }
        }
    }

    [Serializable]
    public class StatConfigDictionary : UnitySerializedDictionary<AttributeData, float> { }

    [Serializable]
    public class DerivedStatConfigDictionary : UnitySerializedDictionary<DerivedStatData, float> { }

    [Serializable]
    public class ResourceConfigDictionary : UnitySerializedDictionary<ResourceData, float> { }
}