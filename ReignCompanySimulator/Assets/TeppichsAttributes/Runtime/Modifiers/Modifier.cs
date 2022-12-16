using System;
using UnityEngine;

namespace TeppichsAttributes.Modifiers
{
    [Serializable]
    public sealed class Modifier
    {
        [SerializeField]     public ModifierType type;
        [SerializeField]     public float        value;
        [SerializeReference] public object       source;

        public Modifier(float value, ModifierType type, object source = null)
        {
            this.value  = value;
            this.type   = type;
            this.source = source;
        }

        public Modifier CreateDuplicate() => new(value, type, source);
    }
}