using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace DynamicDescriptors.Tests
{
    public sealed class DictionaryTypeDescriptorTests
    {
        [Fact]
        public void Constructor_DataDictionaryIsNull_ThrowsArgumentNullException()
        {
            const string message = "data should not be null.\r\nParameter name: data";

            Action act1 = () => new DictionaryTypeDescriptor(null);
            act1.ShouldThrow<ArgumentNullException>().WithMessage(message);

            Action act2 = () => new DictionaryTypeDescriptor(null, null);
            act2.ShouldThrow<ArgumentNullException>().WithMessage(message);
        }

        [Fact]
        public void GetPropertyOwner_ReturnsTypeDescriptor()
        {
            var data = new Dictionary<string, object>();
            var typeDescriptor = new DictionaryTypeDescriptor(data);
            var propertyDescriptor = new MockPropertyDescriptor();

            typeDescriptor.GetPropertyOwner(propertyDescriptor).Should().Be(typeDescriptor);
        }

        [Fact]
        public void GetProperties_ReturnsPropertiesFromDataDictionary()
        {
            var data = new Dictionary<string, object>();
            data["Property1"] = "value1";
            data["Property2"] = 2;

            var typeDescriptor = new DictionaryTypeDescriptor(data);

            var properties = typeDescriptor.GetProperties();

            properties[0].Name.Should().Be("Property1");
            properties[1].Name.Should().Be("Property2");
        }

        [Fact]
        public void GetProperties_TypeIncluded_UsesSpecifiedType()
        {
            var data = new Dictionary<string, object>();
            data["Property1"] = "value1";
            data["Property2"] = 2;

            var types = new Dictionary<string, Type>();
            types["Property1"] = typeof(string);
            types["Property2"] = typeof(int);

            var typeDescriptor = new DictionaryTypeDescriptor(data, types);

            var properties = typeDescriptor.GetProperties();

            properties[0].Name.Should().Be("Property1");
            properties[0].PropertyType.Should().Be(typeof(string));
            properties[1].Name.Should().Be("Property2");
            properties[1].PropertyType.Should().Be(typeof(int));
        }

        [Fact]
        public void GetProperties_TypeOmitted_UsesObjectType()
        {
            var data = new Dictionary<string, object>();
            data["Property1"] = "value1";
            data["Property2"] = 2;

            var typeDescriptor = new DictionaryTypeDescriptor(data);

            var properties = typeDescriptor.GetProperties();

            properties[0].Name.Should().Be("Property1");
            properties[0].PropertyType.Should().Be(typeof(object));
            properties[1].Name.Should().Be("Property2");
            properties[1].PropertyType.Should().Be(typeof(object));
        }

        [Fact]
        public void PropertySet_RaisesPropertyChangedEvent()
        {
            var data = new Dictionary<string, object>();
            data["Property1"] = "value1";
            data["Property2"] = "value2";

            string propertyChanged = null;

            var descriptor = new DictionaryTypeDescriptor(data);
            descriptor.PropertyChanged += (s, e) =>
            {
                propertyChanged = e.PropertyName;
            };

            var properties = descriptor.GetProperties();
            properties[0].SetValue(descriptor, "modified");

            data["Property1"].Should().Be("modified");
            propertyChanged.Should().Be("Property1");
        }

        [Fact]
        public void PropertyReset_RaisesPropertyChangedEvent()
        {
            var data = new Dictionary<string, object>();
            data["Property1"] = "value1";
            data["Property2"] = "value2";

            string propertyChanged = null;

            var descriptor = new DictionaryTypeDescriptor(data);
            descriptor.PropertyChanged += (s, e) =>
            {
                propertyChanged = e.PropertyName;
            };

            var properties = descriptor.GetProperties();
            properties[0].ResetValue(descriptor);

            data["Property1"].Should().Be(null);
            propertyChanged.Should().Be("Property1");
        }
    }
}
