using System;
using TeppichsTools.Data;

namespace TeppichsAttributes.Runtime
{
    public class AttributeContainer
    {
        public AttributeDictionary attributeDictionary = new AttributeDictionary();
    }
    
    
    [Serializable]
    public class AttributeDictionary : UnitySerializedDictionary<AttributeData, Attribute> { }
}