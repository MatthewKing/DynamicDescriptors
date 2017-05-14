using System;
using System.Collections.Generic;
using System.ComponentModel;
using NUnit.Framework;

namespace DynamicDescriptors.Tests
{
    [TestFixture]
    internal sealed class DictionaryTypeDescriptorTests
    {
        [Test]
        public void Constructor_DataDictionaryIsNull_ThrowsArgumentNullException()
        {
            const string message = "data should not be null.";

            Assert.That(() => new DictionaryTypeDescriptor(null),
                Throws.TypeOf<ArgumentNullException>()
                      .And.Message.Contains(message));

            Assert.That(() => new DictionaryTypeDescriptor(null, null),
                Throws.TypeOf<ArgumentNullException>()
                      .And.Message.Contains(message));
        }

        [Test]
        public void GetPropertyOwner_ReturnsTypeDescriptor()
        {
            IDictionary<string, object> data = new Dictionary<string, object>();
            DictionaryTypeDescriptor typeDescriptor = new DictionaryTypeDescriptor(data);
            PropertyDescriptor propertyDescriptor = new MockPropertyDescriptor();

            Assert.That(typeDescriptor.GetPropertyOwner(propertyDescriptor), Is.EqualTo(typeDescriptor));
        }

        [Test]
        public void GetProperties_ReturnsPropertiesFromDataDictionary()
        {
            IDictionary<string, object> data = new Dictionary<string, object>();
            data["Property1"] = "value1";
            data["Property2"] = 2;

            DictionaryTypeDescriptor typeDescriptor = new DictionaryTypeDescriptor(data);

            PropertyDescriptorCollection properties = typeDescriptor.GetProperties();
            Assert.That(properties[0].Name, Is.EqualTo("Property1"));
            Assert.That(properties[1].Name, Is.EqualTo("Property2"));
        }

        [Test]
        public void GetProperties_TypeIncluded_UsesSpecifiedType()
        {
            IDictionary<string, object> data = new Dictionary<string, object>();
            data["Property1"] = "value1";
            data["Property2"] = 2;

            IDictionary<string, Type> types = new Dictionary<string, Type>();
            types["Property1"] = typeof(string);
            types["Property2"] = typeof(int);

            DictionaryTypeDescriptor typeDescriptor = new DictionaryTypeDescriptor(data, types);

            PropertyDescriptorCollection properties = typeDescriptor.GetProperties();
            Assert.That(properties[0].Name, Is.EqualTo("Property1"));
            Assert.That(properties[0].PropertyType, Is.EqualTo(typeof(string)));
            Assert.That(properties[1].Name, Is.EqualTo("Property2"));
            Assert.That(properties[1].PropertyType, Is.EqualTo(typeof(int)));
        }

        [Test]
        public void GetProperties_TypeOmitted_UsesObjectType()
        {
            IDictionary<string, object> data = new Dictionary<string, object>();
            data["Property1"] = "value1";
            data["Property2"] = 2;

            DictionaryTypeDescriptor typeDescriptor = new DictionaryTypeDescriptor(data);

            PropertyDescriptorCollection properties = typeDescriptor.GetProperties();
            Assert.That(properties[0].Name, Is.EqualTo("Property1"));
            Assert.That(properties[0].PropertyType, Is.EqualTo(typeof(object)));
            Assert.That(properties[1].Name, Is.EqualTo("Property2"));
            Assert.That(properties[1].PropertyType, Is.EqualTo(typeof(object)));
        }

        [Test]
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

            Assert.That(data["Property1"], Is.EqualTo("modified"));
            Assert.That(propertyChanged, Is.EqualTo("Property1"));
        }

        [Test]
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

            Assert.That(data["Property1"], Is.EqualTo(null));
            Assert.That(propertyChanged, Is.EqualTo("Property1"));
        }
    }
}
