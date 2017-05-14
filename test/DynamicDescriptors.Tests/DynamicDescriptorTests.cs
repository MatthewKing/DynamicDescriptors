using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace DynamicDescriptors.Tests
{
    public sealed class DynamicDescriptorTests
    {
        [Fact]
        public void CreateFromInstance_InstanceIsNull_ThrowsArgumentNullException()
        {
            const string message = "instance should not be null.\r\nParameter name: instance";
            Action act = () => DynamicDescriptor.CreateFromInstance<object>(null);
            act.ShouldThrow<ArgumentNullException>().WithMessage(message);
        }

        [Fact]
        public void CreateFromInstance_InstanceIsNotNull_ReturnsNewDynamicTypeDescriptorInstance()
        {
            var instance = new object();
            var descriptor = DynamicDescriptor.CreateFromInstance(instance);

            descriptor.Should().NotBeNull();
        }

        [Fact]
        public void CreateFromDescriptor_DescriptorIsNull_ThrowsArgumentNullException()
        {
            const string message = "instance should not be null.\r\nParameter name: instance";
            Action act = () => DynamicDescriptor.CreateFromDescriptor(null);
            act.ShouldThrow<ArgumentNullException>().WithMessage(message);
        }

        [Fact]
        public void CreateFromDescriptor_DescriptorIsNotNull_ReturnsNewDynamicTypeDescriptorInstance()
        {
            var baseDescriptor = new MockCustomTypeDescriptor();
            var descriptor = DynamicDescriptor.CreateFromDescriptor(baseDescriptor);

            descriptor.Should().NotBeNull();
        }

        [Fact]
        public void CreateFromDictionary_DataDictionaryIsNull_ThrowsArgumentNullException()
        {
            const string message = "data should not be null.\r\nParameter name: data";

            Action act1 = () => DynamicDescriptor.CreateFromDictionary(null);
            act1.ShouldThrow<ArgumentNullException>().WithMessage(message);

            Action act2 = () => DynamicDescriptor.CreateFromDictionary(null, null);
            act2.ShouldThrow<ArgumentNullException>().WithMessage(message);
        }

        [Fact]
        public void CreateFromDictionary_DataDictionaryIsNotNull_TypeDictionaryNotPresent_ReturnsNewDynamicTypeDescriptorInstance()
        {
            var data = new Dictionary<string, object>();
            var descriptor = DynamicDescriptor.CreateFromDictionary(data);

            descriptor.Should().NotBeNull();
        }

        [Fact]
        public void CreateFromDictionary_DataDictionaryIsNotNull_TypeDictionaryPresent_ReturnsNewDynamicTypeDescriptorInstance()
        {
            var data = new Dictionary<string, object>();
            var types = new Dictionary<string, Type>();
            var descriptor = DynamicDescriptor.CreateFromDictionary(data, types);

            descriptor.Should().NotBeNull();
        }
    }
}
