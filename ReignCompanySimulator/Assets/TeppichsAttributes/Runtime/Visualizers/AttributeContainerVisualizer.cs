using System.Collections.Generic;
using TeppichsAttributes.Attributes;
using TeppichsAttributes.Data;
using TeppichsAttributes.Visualizers.Primitives;
using TeppichsTools.Behavior;
using TeppichsTools.Creation.Pools;
using UnityEngine;

namespace TeppichsAttributes.Visualizers
{
    public class AttributeContainerVisualizer : TransformCachingMonoBehaviour
    {
        [SerializeField] private AttributeVisualizer<Stat>        statVisualizerPrefab;
        [SerializeField] private AttributeVisualizer<DerivedStat> derivedStatVisualizerPrefab;
        [SerializeField] private AttributeVisualizer<Resource>    resourceVisualizerPrefab;

        private AttributeContainer container;

        private PrefabObjectPool<AttributeVisualizer<DerivedStat>> derivedStatPool;
        private PrefabObjectPool<AttributeVisualizer<Resource>>    resourcePool;
        private PrefabObjectPool<AttributeVisualizer<Stat>>        statPool;

        private void Awake()
        {
            statPool = new PrefabObjectPool<AttributeVisualizer<Stat>>(statVisualizerPrefab, myTransform);

            derivedStatPool =
                new PrefabObjectPool<AttributeVisualizer<DerivedStat>>(derivedStatVisualizerPrefab, myTransform);

            resourcePool = new PrefabObjectPool<AttributeVisualizer<Resource>>(resourceVisualizerPrefab, myTransform);
        }

        public void Initialize(AttributeContainer attributeContainer)
        {
            container = attributeContainer;

            foreach (KeyValuePair<AttributeData, Stat> stat in attributeContainer.stats)
                statPool.Next().Initialize(stat.Value);

            foreach (KeyValuePair<DerivedStatData, DerivedStat> derivedStat in attributeContainer.derivedStats)
                derivedStatPool.Next().Initialize(derivedStat.Value);

            foreach (KeyValuePair<ResourceData, Resource> resource in attributeContainer.resources)
                resourcePool.Next().Initialize(resource.Value);
        }
    }
}