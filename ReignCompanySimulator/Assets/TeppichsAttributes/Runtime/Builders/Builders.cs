using TeppichsAttributes.Builders.Attributes;
using TeppichsAttributes.Builders.Data;
using TeppichsAttributes.Builders.Modifiers;

namespace TeppichsAttributes.Builders
{
    public static class A
    {
        public static StatBuilder            Stat            => new();
        
        public static DerivedStatDataBuilder DerivedStatData => new();

        public static ModifierBuilder Modifier => new();
    }

    public static class An
    {
        public static AttributeDataBuilder AttributeData => new();
    }
}