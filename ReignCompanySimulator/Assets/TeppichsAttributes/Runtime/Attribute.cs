using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TeppichsAttributes.Runtime;
using UnityEngine;

namespace TeppichsAttributes
{
    public class Attribute 
    {
        private AttributeData data;
        private float         baseValue;

        [SerializeField, ReadOnly]
        private float currentValue;

        private List<Modifier> modifiers = new();

        public Attribute(AttributeData data, float baseValue)
        {
            this.data      = data;
            this.baseValue = baseValue;
        }

        protected Attribute() { throw new NotImplementedException(); }
    }

    public abstract class DerivedAttribute : Attribute
    {
        //refers to attributes it derives from
        //gets baseValue via the related attributes, then adds its own modifiers
    }

    public class Resource : Attribute
    {
        //modifiers are added immediately, not stored
    }
}