using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace DynamicDescriptors.Tests;

public sealed class DynamicDescriptorTests
{
    [Fact]
    public void CreateFromInstance_InstanceIsNull_ThrowsArgumentNullException()
    {
        var action = () => DynamicDescriptor.CreateFromInstance<object>(null);
        action.Should().Throw<ArgumentNullException>();
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
        var action = () => DynamicDescriptor.CreateFromDescriptor(null);
        action.Should().Throw<ArgumentNullException>();
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
        var action1 = () => DynamicDescriptor.CreateFromDictionary(null);
        action1.Should().Throw<ArgumentNullException>();

        var action2 = () => DynamicDescriptor.CreateFromDictionary(null, null);
        action2.Should().Throw<ArgumentNullException>();
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
