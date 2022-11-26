using System;
using UnityEngine;

namespace TeppichsAttributes.Runtime
{
    [Serializable]
    public class Modifier
    {
        [SerializeField] public ModifierType type;
        [SerializeField] public float        value;
        [SerializeField] public object       source;

        #region Constructors

        public Modifier(float value, ModifierType type, object source)
        {
            this.value  = value;
            this.type   = type;
            this.source = source;
        }

        public Modifier(float value, ModifierType type) : this(value, type, null) { }

        public Modifier CreateDuplicate() => new(value, type, source);

        #endregion
    }
}