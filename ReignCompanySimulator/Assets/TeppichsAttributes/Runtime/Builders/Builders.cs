using TeppichsAttributes.Builders.Attributes;
using TeppichsAttributes.Builders.Data;
using TeppichsAttributes.Builders.Modifiers;

namespace TeppichsAttributes.Builders
{
    public static class A
    {
        #region Modifiers

        public static ModifierBuilder Modifier => new();

        #endregion

        #region Attributes

        public static StatBuilder Stat => new();
        public static DerivedStatBuilder DerivedStat => new();
        public static ResourceBuilder Resource => new();

        #endregion

        #region AttributeDatas

        public static DerivedStatDataBuilder DerivedStatData => new();
        public static ResourceDataBuilder ResourceData => new();

        #endregion
    }

    public static class An
    {
        public static AttributeDataBuilder AttributeData => new();
    }
}