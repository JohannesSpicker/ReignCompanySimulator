using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TeppichsAttributes.Attributes;
using TeppichsAttributes.Builders;
using TeppichsAttributes.Data;
using TeppichsAttributes.Modifiers;
using TeppichsTools.Math;

namespace TeppichsAttributes.Tests.Editor
{
    public static class StatTests
    {
        private const int numberOfTests = 5;

        private static class Base
        {
            [Test]
            public static void StatHasBaseValue([Random(float.MinValue, float.MaxValue, numberOfTests)] float baseValue)
            {
                AttributeData attributeData = An.AttributeData;
                Stat          stat          = A.Stat.WithAttributeDate(attributeData).WithBaseValue(baseValue);

                stat.Value.Should().Be(baseValue);
            }

            [Test]
            public static void StatIsNotHigherThanMaxValue(
                [Random(float.MinValue / 2f, float.MaxValue / 2f, numberOfTests)] float baseValue,
                [Random(float.MinValue / 2f, float.MaxValue / 2f, numberOfTests)] float maxValueDifference)
            {
                float         maxValue      = baseValue + maxValueDifference;
                AttributeData attributeData = An.AttributeData.WithMaxValue(maxValue);
                Stat          stat          = A.Stat.WithAttributeDate(attributeData).WithBaseValue(baseValue);

                stat.Value.Should().BeLessOrEqualTo(maxValue);
            }

            [Test]
            public static void StatIsNotLowerThanMinValue(
                [Random(float.MinValue / 2f, float.MaxValue / 2f, numberOfTests)] float baseValue,
                [Random(float.MinValue / 2f, float.MaxValue / 2f, numberOfTests)] float minValueDifference)
            {
                float         minValue      = baseValue + minValueDifference;
                AttributeData attributeData = An.AttributeData.WithMinValue(minValue);
                Stat          stat          = A.Stat.WithAttributeDate(attributeData).WithBaseValue(baseValue);

                stat.Value.Should().BeGreaterOrEqualTo(minValue);
            }
        }

        private static class Modifiers
        {
            private static class SingleModifier
            {
                [Test]
                public static void FlatModifierAdds(
                    [Random(float.MinValue / 2f, float.MaxValue / 2f, numberOfTests)] float baseValue,
                    [Random(float.MinValue / 2f, float.MaxValue / 2f, numberOfTests)] float change)
                {
                    Stat stat = A.Stat.WithAttributeDate(An.AttributeData).WithBaseValue(baseValue)
                                 .WithModifier(A.Modifier.WithType(ModifierType.Flat).WithValue(change));

                    stat.Value.Should().Be(baseValue + change);
                }

                [Test]
                public static void PercentageAddModifierMultiplies(
                    [Random(float.MinValue / 2f, float.MaxValue / 2f, numberOfTests)] float baseValue,
                    [Random(float.MinValue / 2f, float.MaxValue / 2f, numberOfTests)] float change)
                {
                    Stat stat = A.Stat.WithAttributeDate(An.AttributeData).WithBaseValue(baseValue)
                                 .WithModifier(A.Modifier.WithType(ModifierType.PercentAdd).WithValue(change));

                    stat.Value.Should().Be(baseValue * change);
                }

                [Test]
                public static void PercentageMultiplyModifierMultiplies(
                    [Random(float.MinValue / 2f, float.MaxValue / 2f, numberOfTests)] float baseValue,
                    [Random(float.MinValue / 2f, float.MaxValue / 2f, numberOfTests)] float change)
                {
                    Stat stat = A.Stat.WithAttributeDate(An.AttributeData).WithBaseValue(baseValue)
                                 .WithModifier(A.Modifier.WithType(ModifierType.PercentMult).WithValue(change));

                    stat.Value.Should().Be(baseValue * change);
                }
            }

            private static class MultipleModifiers
            {
                [Test]
                public static void FlatModifiersAdd(
                    [Random(float.MinValue / 8f, float.MaxValue / 8f, numberOfTests)] float baseValue,
                    [Random(float.MinValue / 8f, float.MaxValue / 8f, 1)]             float mod1,
                    [Random(float.MinValue / 8f, float.MaxValue / 8f, 1)]             float mod2,
                    [Random(float.MinValue / 8f, float.MaxValue / 8f, 1)]             float mod3,
                    [Random(float.MinValue / 8f, float.MaxValue / 8f, 1)]             float mod4,
                    [Random(float.MinValue / 8f, float.MaxValue / 8f, 1)]             float mod5,
                    [Random(float.MinValue / 8f, float.MaxValue / 8f, 1)]             float mod6,
                    [Random(float.MinValue / 8f, float.MaxValue / 8f, 1)]             float mod7)
                {
                    List<float> modifierValues = new()
                    {
                        mod1,
                        mod2,
                        mod3,
                        mod4,
                        mod5,
                        mod6,
                        mod7
                    };

                    List<Modifier> modifiers = new();

                    foreach (float value in modifierValues)
                        modifiers.Add(A.Modifier.WithType(ModifierType.Flat).WithValue(value));

                    Stat stat = A.Stat.WithAttributeDate(An.AttributeData).WithBaseValue(baseValue)
                                 .WithModifiers(modifiers);

                    stat.Value.Should().Be(baseValue + modifierValues.Sum());
                }

                [Test]
                public static void PercentageAddModifiersMultiplyAdditively(
                    [Random(float.MinValue / 256f, float.MaxValue / 256f, numberOfTests)] float baseValue,
                    [Random(float.MinValue / 256f, float.MaxValue / 256f, 1)]             float mod1,
                    [Random(float.MinValue / 256f, float.MaxValue / 256f, 1)]             float mod2,
                    [Random(float.MinValue / 256f, float.MaxValue / 256f, 1)]             float mod3,
                    [Random(float.MinValue / 256f, float.MaxValue / 256f, 1)]             float mod4,
                    [Random(float.MinValue / 256f, float.MaxValue / 256f, 1)]             float mod5,
                    [Random(float.MinValue / 256f, float.MaxValue / 256f, 1)]             float mod6,
                    [Random(float.MinValue / 256f, float.MaxValue / 256f, 1)]             float mod7)
                {
                    List<float> modifierValues = new()
                    {
                        mod1,
                        mod2,
                        mod3,
                        mod4,
                        mod5,
                        mod6,
                        mod7
                    };

                    List<Modifier> modifiers = new();

                    foreach (float value in modifierValues)
                        modifiers.Add(A.Modifier.WithType(ModifierType.PercentAdd).WithValue(value));

                    Stat stat = A.Stat.WithAttributeDate(An.AttributeData).WithBaseValue(baseValue)
                                 .WithModifiers(modifiers);

                    stat.Value.Should().Be(baseValue * modifierValues.Sum());
                }

                [Test]
                public static void PercentageMultiplyModifiersMultiplyMultiplicatively(
                    [Random(float.MinValue / 256f, float.MaxValue / 256f, numberOfTests)] float baseValue,
                    [Random(float.MinValue / 256f, float.MaxValue / 256f, 1)]             float mod1,
                    [Random(float.MinValue / 256f, float.MaxValue / 256f, 1)]             float mod2,
                    [Random(float.MinValue / 256f, float.MaxValue / 256f, 1)]             float mod3,
                    [Random(float.MinValue / 256f, float.MaxValue / 256f, 1)]             float mod4,
                    [Random(float.MinValue / 256f, float.MaxValue / 256f, 1)]             float mod5,
                    [Random(float.MinValue / 256f, float.MaxValue / 256f, 1)]             float mod6,
                    [Random(float.MinValue / 256f, float.MaxValue / 256f, 1)]             float mod7)
                {
                    List<float> modifierValues = new()
                    {
                        mod1,
                        mod2,
                        mod3,
                        mod4,
                        mod5,
                        mod6,
                        mod7
                    };

                    List<Modifier> modifiers = new();

                    foreach (float value in modifierValues)
                        modifiers.Add(A.Modifier.WithType(ModifierType.PercentMult).WithValue(value));

                    Stat stat = A.Stat.WithAttributeDate(An.AttributeData).WithBaseValue(baseValue)
                                 .WithModifiers(modifiers);

                    stat.Value.Should().Be(baseValue * modifierValues.Product());
                }

                [Test]
                public static void ModifierTypesStackRight(
                    [Random(float.MinValue / 512f, float.MaxValue / 512f, numberOfTests)] float baseValue,
                    [Random(float.MinValue / 512f, float.MaxValue / 512f, 1)]             float mod1,
                    [Random(float.MinValue / 512f, float.MaxValue / 512f, 1)]             float mod2,
                    [Random(float.MinValue / 512f, float.MaxValue / 512f, 1)]             float mod3,
                    [Random(float.MinValue / 512f, float.MaxValue / 512f, 1)]             float mod4,
                    [Random(float.MinValue / 512f, float.MaxValue / 512f, 1)]             float mod5,
                    [Random(float.MinValue / 512f, float.MaxValue / 512f, 1)]             float mod6,
                    [Random(float.MinValue / 512f, float.MaxValue / 512f, 1)]             float mod7,
                    [Random(float.MinValue / 512f, float.MaxValue / 512f, 1)]             float mod8,
                    [Random(float.MinValue / 512f, float.MaxValue / 512f, 1)]             float mod9)
                {
                    List<float> flatModifierValues           = new() { mod1, mod2, mod3 };
                    List<float> percentageAddModifierValues  = new() { mod4, mod5, mod6 };
                    List<float> percentageMultModifierValues = new() { mod7, mod8, mod9 };

                    List<Modifier> modifiers = new();

                    foreach (float value in flatModifierValues)
                        modifiers.Add(A.Modifier.WithValue(value).WithType(ModifierType.Flat));

                    foreach (float value in percentageAddModifierValues)
                        modifiers.Add(A.Modifier.WithValue(value).WithType(ModifierType.PercentAdd));

                    foreach (float value in percentageMultModifierValues)
                        modifiers.Add(A.Modifier.WithValue(value).WithType(ModifierType.PercentMult));

                    Stat stat = A.Stat.WithAttributeDate(An.AttributeData).WithBaseValue(baseValue)
                                 .WithModifiers(modifiers);

                    stat.Value.Should().Be((baseValue + flatModifierValues.Sum()) * percentageAddModifierValues.Sum()
                                           * percentageMultModifierValues.Product());
                }
            }
        }
    }
}