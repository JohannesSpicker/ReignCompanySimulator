using System;
using System.Collections.Generic;
using System.Linq;
using TeppichsAttributes.Attributes;
using TeppichsTools.Data;
using UnityEngine;

namespace TeppichsAttributes.Data
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

            foreach (KeyValuePair<DerivedStatData, float> derivedStat in derivedStats)
                container.derivedStats[derivedStat.Key] = new DerivedStat(derivedStat.Key, derivedStat.Value,
                                                                          derivedStat.Key.factors.Select(factor =>
                                                                              container.stats[factor]));

            foreach (KeyValuePair<ResourceData, float> resource in resources)
                container.resources[resource.Key] = new Resource(resource.Key, resource.Value,
                                                                 resource.Key.maxAttribute is DerivedStatData
                                                                     derivedStatData
                                                                     ? container.derivedStats[derivedStatData]
                                                                     : container.stats[resource.Key.maxAttribute]);
        }

        [Serializable]
        public class StatConfigDictionary : UnitySerializedDictionary<AttributeData, float> { }

        [Serializable]
        public class DerivedStatConfigDictionary : UnitySerializedDictionary<DerivedStatData, float> { }

        [Serializable]
        public class ResourceConfigDictionary : UnitySerializedDictionary<ResourceData, float> { }
    }
}