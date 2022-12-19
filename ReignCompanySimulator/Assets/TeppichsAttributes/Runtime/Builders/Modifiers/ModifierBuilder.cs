using TeppichsAttributes.Modifiers;
using TeppichsTools.Creation;

namespace TeppichsAttributes.Builders.Modifiers
{
    public class ModifierBuilder : Builder<Modifier>
    {
        private object       source;
        private ModifierType type;
        private float        value;

        public ModifierBuilder WithValue(float val)
        {
            value = val;

            return this;
        }

        public ModifierBuilder WithType(ModifierType modifierType)
        {
            type = modifierType;

            return this;
        }

        public ModifierBuilder WithSource(object src)
        {
            source = src;

            return this;
        }

        protected override Modifier Build() => new(value, type, source);
    }
}