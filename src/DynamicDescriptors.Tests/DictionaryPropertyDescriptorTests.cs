using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DynamicDescriptors.Tests
{
    [TestFixture]
    internal sealed class DictionaryPropertyDescriptorTests
    {
        [Test]
        public void Constructor_DataIsNull_ThrowsArgumentNullException()
        {
            IDictionary<string, object> data = null;
            string propertyName = "propertyName";
            Type propertyType = typeof(object);

            const string message = "data should not be null.";
            Assert.That(() => new DictionaryPropertyDescriptor(data, propertyName, propertyType),
                Throws.TypeOf<ArgumentNullException>()
                      .And.Message.Contains(message));
        }

        [Test]
        public void Constructor_PropertyNameIsNull_ThrowsArgumentNullException()
        {
            IDictionary<string, object> data = new Dictionary<string, object>();
            string propertyName = null;
            Type propertyType = typeof(object);

            const string message = "propertyName should not be null.";
            Assert.That(() => new DictionaryPropertyDescriptor(data, propertyName, propertyType),
                Throws.TypeOf<ArgumentNullException>()
                      .And.Message.Contains(message));
        }

        [Test]
        public void Constructor_PropertyNameIsEmpty_ThrowsArgumentException()
        {
            IDictionary<string, object> data = new Dictionary<string, object>();
            string propertyName = String.Empty;
            Type propertyType = typeof(object);

            const string message = "propertyName should not be an empty string.";
            Assert.That(() => new DictionaryPropertyDescriptor(data, propertyName, propertyType),
                Throws.TypeOf<ArgumentException>()
                      .And.Message.Contains(message));
        }

        [Test]
        public void Constructor_PropertyTypeIsNull_ThrowsArgumentNullException()
        {
            IDictionary<string, object> data = new Dictionary<string, object>();
            string propertyName = "propertyName";
            Type propertyType = null;

            const string message = "propertyType should not be null.";
            Assert.That(() => new DictionaryPropertyDescriptor(data, propertyName, propertyType),
                Throws.TypeOf<ArgumentNullException>()
                      .And.Message.Contains(message));
        }

        [Test]
        public void GetValue_ReturnsValueFromUnderlyingDictionary()
        {
            IDictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Property1", "Value1");
            data.Add("Property2", 2);

            DictionaryPropertyDescriptor pd1 = new DictionaryPropertyDescriptor(data, "Property1", typeof(string));
            Assert.That(pd1.GetValue(null), Is.EqualTo(data["Property1"]));

            DictionaryPropertyDescriptor pd2 = new DictionaryPropertyDescriptor(data, "Property2", typeof(int));
            Assert.That(pd2.GetValue(null), Is.EqualTo(data["Property2"]));
        }

        [Test]
        public void SetValue_ChangesValueInUnderlyingDictionaryToSpecifiedValue()
        {
            IDictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Property1", "Value1");
            data.Add("Property2", 2);

            DictionaryPropertyDescriptor pd1 = new DictionaryPropertyDescriptor(data, "Property1", typeof(string));
            pd1.SetValue(null, "Modified");
            Assert.That(data["Property1"], Is.EqualTo("Modified"));

            DictionaryPropertyDescriptor pd2 = new DictionaryPropertyDescriptor(data, "Property2", typeof(int));
            pd2.SetValue(null, 0);
            Assert.That(data["Property2"], Is.EqualTo(0));
        }

        [Test]
        public void ResetValue_ChangesValueInUnderlyingDictionaryToNull()
        {
            IDictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Property1", "Value1");
            data.Add("Property2", 2);

            DictionaryPropertyDescriptor pd1 = new DictionaryPropertyDescriptor(data, "Property1", typeof(string));
            pd1.ResetValue(null);
            Assert.That(data["Property1"], Is.Null);

            DictionaryPropertyDescriptor pd2 = new DictionaryPropertyDescriptor(data, "Property2", typeof(int));
            pd2.ResetValue(null);
            Assert.That(data["Property2"], Is.Null);
        }

        [Test]
        public void PropertyType_ReturnsSpecifiedType()
        {
            IDictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Property1", "Value1");
            data.Add("Property2", 2);

            DictionaryPropertyDescriptor pd1 = new DictionaryPropertyDescriptor(data, "Property1", typeof(string));
            Assert.That(pd1.PropertyType, Is.EqualTo(typeof(string)));

            DictionaryPropertyDescriptor pd2 = new DictionaryPropertyDescriptor(data, "Property2", typeof(int));
            Assert.That(pd2.PropertyType, Is.EqualTo(typeof(int)));
        }
    }
}
