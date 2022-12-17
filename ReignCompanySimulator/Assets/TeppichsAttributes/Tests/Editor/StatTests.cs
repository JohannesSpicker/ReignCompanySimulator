using FluentAssertions;
using NUnit.Framework;
using TeppichsAttributes.Attributes;
using TeppichsAttributes.Builders;
using TeppichsAttributes.Data;

namespace TeppichsAttributes.Tests.Editor
{
    public class StatTests
    {
        private const int numberOfTests = 5;

        [Test]
        public void StatHasBaseValue([Random(float.MinValue, float.MaxValue, numberOfTests)] float baseValue)
        {
            AttributeData attributeData = An.AttributeData;
            Stat          stat          = A.Stat.WithAttributeDate(attributeData).WithBaseValue(baseValue);

            stat.Value.Should().Be(baseValue);
        }

        [Test]
        public void StatIsNotHigherThanMaxValue(
            [Random(float.MinValue / 2f, float.MaxValue / 2f, numberOfTests)] float baseValue,
            [Random(float.MinValue / 2f, float.MaxValue / 2f, numberOfTests)] float maxValueDifference)
        {
            float         maxValue      = baseValue + maxValueDifference;
            AttributeData attributeData = An.AttributeData.WithMaxValue(maxValue);
            Stat          stat          = A.Stat.WithAttributeDate(attributeData).WithBaseValue(baseValue);

            stat.Value.Should().BeLessOrEqualTo(maxValue);
        }

        [Test]
        public void StatIsNotLowerThanMinValue(
            [Random(float.MinValue / 2f, float.MaxValue / 2f, numberOfTests)] float baseValue,
            [Random(float.MinValue / 2f, float.MaxValue / 2f, numberOfTests)] float minValueDifference)
        {
            float         minValue      = baseValue + minValueDifference;
            AttributeData attributeData = An.AttributeData.WithMinValue(minValue);
            Stat          stat          = A.Stat.WithAttributeDate(attributeData).WithBaseValue(baseValue);

            stat.Value.Should().BeGreaterOrEqualTo(minValue);
        }

        //TODO: test modifiers
    }
}