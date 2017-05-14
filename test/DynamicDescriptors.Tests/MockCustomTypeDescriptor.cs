using System;
using System.ComponentModel;

namespace DynamicDescriptors.Tests
{
    internal sealed class MockCustomTypeDescriptor : ICustomTypeDescriptor
    {
        public AttributeCollection GetAttributes()
        {
            return new AttributeCollection();
        }

        public string GetClassName()
        {
            return null;
        }

        public string GetComponentName()
        {
            return null;
        }

        public TypeConverter GetConverter()
        {
            return null;
        }

        public EventDescriptor GetDefaultEvent()
        {
            return null;
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return null;
        }

        public object GetEditor(Type editorBaseType)
        {
            return null;
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return new EventDescriptorCollection(new EventDescriptor[] { });
        }

        public EventDescriptorCollection GetEvents()
        {
            return GetEvents(null);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return new PropertyDescriptorCollection(new PropertyDescriptor[] { });
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return GetProperties(null);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return null;
        }
    }
}
