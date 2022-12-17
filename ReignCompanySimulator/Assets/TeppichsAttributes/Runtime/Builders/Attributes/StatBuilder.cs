using System.Collections.Generic;
using TeppichsAttributes.Attributes;
using TeppichsAttributes.Data;
using TeppichsAttributes.Modifiers;
using TeppichsTools.Creation;

namespace TeppichsAttributes.Builders
{
    public class StatBuilder : Builder<Stat>
    {
        private          AttributeData  attributeData;
        private          float          baseValue;
        private readonly List<Modifier> modifiers = new();

        public StatBuilder WithBaseValue(float value)
        {
            baseValue = value;

            return this;
        }

        public StatBuilder WithAttributeDate(AttributeData data)
        {
            attributeData = data;

            return this;
        }

        public StatBuilder WithModifier(Modifier modifier)
        {
            modifiers.Add(modifier);

            return this;
        }

        public StatBuilder WithModifiers(List<Modifier> mods)
        {
            modifiers.AddRange(mods);

            return this;
        }

        protected override Stat Build()
        {
            Stat stat = new Stat(attributeData, baseValue);

            foreach (Modifier mod in modifiers)
                stat.AddModifier(mod);

            return stat;
        }
    }
}