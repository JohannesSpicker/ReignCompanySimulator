using TeppichsAttributes.Attributes;

namespace TeppichsAttributes.Visualizers.Primitives
{
    public abstract class ResourceVisualizer : AttributeVisualizer<Resource>
    {
        protected override void DisplayAttribute(Resource resource)
        {
            base.DisplayAttribute(resource);
            DisplayMaxAttribute(resource.maxAttribute);
        }

        protected override void Refresh(float value)
        {
            base.Refresh(value);
            DisplayMaxAttribute(attribute.maxAttribute);
        }

        protected abstract void DisplayMaxAttribute(Attribute maxAttribute);
    }
}