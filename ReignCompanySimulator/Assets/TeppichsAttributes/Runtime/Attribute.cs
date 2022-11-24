using System;

namespace TeppichsAttributes
{
    [Serializable]
    public class Attribute
    {
        //attribute data
        
        
    }

    [Serializable]
    public class AttributeData
    {
        [SerializeField] public string inGameName;
        [SerializeField] public bool usesMaxValue = false;
        [SerializeField] public float maxValue = 0f;
        
    }
}