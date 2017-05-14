using System;
using System.Collections.Generic;
using System.ComponentModel;
using NUnit.Framework;

namespace DynamicDescriptors.Tests
{
    [TestFixture]
    internal sealed class DynamicDescriptorTests
    {
        [Test]
        public void CreateFromInstance_InstanceIsNull_ThrowsArgumentNullException()
        {
            const string message = "instance should not be null.";
            Assert.That(() => DynamicDescriptor.CreateFromInstance<object>(null),
                Throws.TypeOf<ArgumentNullException>()
                      .And.Message.Contains(message));
        }

        [Test]
        public void CreateFromInstance_InstanceIsNotNull_ReturnsNewDynamicTypeDescriptorInstance()
        {
            object instance = new object();
            DynamicTypeDescriptor descriptor = DynamicDescriptor.CreateFromInstance(instance);

            Assert.That(descriptor, Is.Not.Null);
        }

        [Test]
        public void CreateFromDescriptor_DescriptorIsNull_ThrowsArgumentNullException()
        {
            const string message = "instance should not be null.";
            Assert.That(() => DynamicDescriptor.CreateFromDescriptor(null),
                Throws.TypeOf<ArgumentNullException>()
                      .And.Message.Contains(message));
        }

        [Test]
        public void CreateFromDescriptor_DescriptorIsNotNull_ReturnsNewDynamicTypeDescriptorInstance()
        {
            ICustomTypeDescriptor baseDescriptor = new MockCustomTypeDescriptor();
            DynamicTypeDescriptor descriptor = DynamicDescriptor.CreateFromDescriptor(baseDescriptor);

            Assert.That(descriptor, Is.Not.Null);
        }

        [Test]
        public void CreateFromDictionary_DataDictionaryIsNull_ThrowsArgumentNullException()
        {
            const string message = "data should not be null.";

            Assert.That(() => DynamicDescriptor.CreateFromDictionary(null),
                Throws.TypeOf<ArgumentNullException>()
                      .And.Message.Contains(message));

            Assert.That(() => DynamicDescriptor.CreateFromDictionary(null, null),
                Throws.TypeOf<ArgumentNullException>()
                      .And.Message.Contains(message));
        }

        [Test]
        public void CreateFromDictionary_DataDictionaryIsNotNull_TypeDictionaryNotPresent_ReturnsNewDynamicTypeDescriptorInstance()
        {
            IDictionary<string, object> data = new Dictionary<string, object>();
            DynamicTypeDescriptor descriptor = DynamicDescriptor.CreateFromDictionary(data);

            Assert.That(descriptor, Is.Not.Null);
        }

        [Test]
        public void CreateFromDictionary_DataDictionaryIsNotNull_TypeDictionaryPresent_ReturnsNewDynamicTypeDescriptorInstance()
        {
            IDictionary<string, object> data = new Dictionary<string, object>();
            IDictionary<string, Type> types = new Dictionary<string, Type>();
            DynamicTypeDescriptor descriptor = DynamicDescriptor.CreateFromDictionary(data, types);

            Assert.That(descriptor, Is.Not.Null);
        }
    }
}
