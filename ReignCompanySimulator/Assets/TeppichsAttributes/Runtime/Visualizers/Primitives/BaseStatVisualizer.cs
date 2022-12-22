using System.Collections.Generic;
using TeppichsAttributes.Attributes;
using TeppichsAttributes.Modifiers;

namespace TeppichsAttributes.Visualizers.Primitives
{
    public abstract class BaseStatVisualizer<T> : AttributeVisualizer<T> where T : Stat
    {
        protected override void DisplayAttribute(T stat)
        {
            base.DisplayAttribute(stat);
            DisplayModifiers(stat.Modifiers);
        }

        protected override void Refresh(float value)
        {
            base.Refresh(value);
            DisplayModifiers(attribute.Modifiers);
        }

        protected abstract void DisplayModifiers(IEnumerable<Modifier> modifiers);
    }
}