using System.Collections.Generic;
using TeppichsAttributes.Attributes;
using TeppichsAttributes.Data;
using TeppichsTools.Creation.Pools;
using UnityEngine;

namespace TeppichsAttributes.Visualizers
{
    public class AttributeContainerVisualizer : MonoBehaviour
    {
        [SerializeField] private AttributeVisualizer attributeVisualizerPrefab;
        private                  AttributeContainer  container;

        private PrefabObjectPool<AttributeVisualizer> pool;

        private void Awake() => pool = new PrefabObjectPool<AttributeVisualizer>(attributeVisualizerPrefab, transform);

        public void Initialize(AttributeContainer attributeContainer)
        {
            container = attributeContainer;

            foreach (KeyValuePair<AttributeData, Stat> stat in attributeContainer.stats)
                pool.Next().Initialize(stat.Value);
            
            //derived stats
            
            //resources
        }
    }
}