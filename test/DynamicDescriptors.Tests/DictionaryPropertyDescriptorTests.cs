using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace DynamicDescriptors.Tests;

public sealed class DictionaryPropertyDescriptorTests
{
    [Fact]
    public void Constructor_DataIsNull_ThrowsArgumentNullException()
    {
        IDictionary<string, object> data = null;
        string propertyName = "propertyName";
        Type propertyType = typeof(object);

        var action = () => new DictionaryPropertyDescriptor(data, propertyName, propertyType);
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Constructor_PropertyNameIsNull_ThrowsArgumentNullException()
    {
        IDictionary<string, object> data = new Dictionary<string, object>();
        string propertyName = null;
        Type propertyType = typeof(object);

        var action = () => new DictionaryPropertyDescriptor(data, propertyName, propertyType);
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Constructor_PropertyNameIsEmpty_ThrowsArgumentException()
    {
        IDictionary<string, object> data = new Dictionary<string, object>();
        string propertyName = String.Empty;
        Type propertyType = typeof(object);

        var action = () => new DictionaryPropertyDescriptor(data, propertyName, propertyType);
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Constructor_PropertyTypeIsNull_ThrowsArgumentNullException()
    {
        IDictionary<string, object> data = new Dictionary<string, object>();
        string propertyName = "propertyName";
        Type propertyType = null;

        var action = () => new DictionaryPropertyDescriptor(data, propertyName, propertyType);
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void GetValue_ReturnsValueFromUnderlyingDictionary()
    {
        var data = new Dictionary<string, object>();
        data.Add("Property1", "Value1");
        data.Add("Property2", 2);

        var pd1 = new DictionaryPropertyDescriptor(data, "Property1", typeof(string));
        pd1.GetValue(null).Should().Be("Value1");

        var pd2 = new DictionaryPropertyDescriptor(data, "Property2", typeof(int));
        pd2.GetValue(null).Should().Be(2);
    }

    [Fact]
    public void SetValue_ChangesValueInUnderlyingDictionaryToSpecifiedValue()
    {
        var data = new Dictionary<string, object>();
        data.Add("Property1", "Value1");
        data.Add("Property2", 2);

        var pd1 = new DictionaryPropertyDescriptor(data, "Property1", typeof(string));
        pd1.SetValue(null, "Modified");
        data["Property1"].Should().Be("Modified");

        var pd2 = new DictionaryPropertyDescriptor(data, "Property2", typeof(int));
        pd2.SetValue(null, 0);
        data["Property2"].Should().Be(0);
    }

    [Fact]
    public void ResetValue_ChangesValueInUnderlyingDictionaryToNull()
    {
        var data = new Dictionary<string, object>();
        data.Add("Property1", "Value1");
        data.Add("Property2", 2);

        var pd1 = new DictionaryPropertyDescriptor(data, "Property1", typeof(string));
        pd1.ResetValue(null);
        data["Property1"].Should().BeNull();

        var pd2 = new DictionaryPropertyDescriptor(data, "Property2", typeof(int));
        pd2.ResetValue(null);
        data["Property2"].Should().BeNull();
    }

    [Fact]
    public void PropertyType_ReturnsSpecifiedType()
    {
        var data = new Dictionary<string, object>();
        data.Add("Property1", "Value1");
        data.Add("Property2", 2);

        var pd1 = new DictionaryPropertyDescriptor(data, "Property1", typeof(string));
        pd1.PropertyType.Should().Be(typeof(string));

        var pd2 = new DictionaryPropertyDescriptor(data, "Property2", typeof(int));
        pd2.PropertyType.Should().Be(typeof(int));
    }
}
