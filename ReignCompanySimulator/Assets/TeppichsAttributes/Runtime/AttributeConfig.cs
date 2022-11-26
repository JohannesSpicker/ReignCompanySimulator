using System;
using System.Collections.Generic;
using TeppichsTools.Data;
using UnityEngine;

namespace TeppichsAttributes.Runtime
{
    [CreateAssetMenu(menuName = "TeppichsAttributes/AttributeConfig", order = 0)]
    public class AttributeConfig : ScriptableObject
    {
        [SerializeField] public AttributeConfigDictionary attributes = new();

        public void ApplyConfig(AttributeContainer container)
        {
            container.attributeDictionary.Clear();

            foreach (KeyValuePair<AttributeData, float> attribute in attributes)
                container.attributeDictionary[attribute.Key] = new Attribute(attribute.Key, attribute.Value);
        }
    }

    [Serializable]
    public class AttributeConfigDictionary : UnitySerializedDictionary<AttributeData, float> { }
}