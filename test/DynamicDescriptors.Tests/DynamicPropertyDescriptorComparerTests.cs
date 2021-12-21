using FluentAssertions;
using Xunit;

namespace DynamicDescriptors.Tests;

public sealed class DynamicPropertyDescriptorComparerTests
{
    [Fact]
    public void Compare_XIsNullAndYIsNot_YComesFirst()
    {
        var comparer = new DynamicPropertyDescriptorComparer();
        var x = default(DynamicPropertyDescriptor);
        var y = new DynamicPropertyDescriptor(new MockPropertyDescriptor("Y"));
        var result = comparer.Compare(x, y);
        result.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Compare_YIsNullAndXIsNot_XComesFirst()
    {
        var comparer = new DynamicPropertyDescriptorComparer();
        var x = new DynamicPropertyDescriptor(new MockPropertyDescriptor("X"));
        var y = default(DynamicPropertyDescriptor);
        var result = comparer.Compare(x, y);
        result.Should().BeLessThan(0);
    }

    [Fact]
    public void Compare_BothXAndYAreNull_ValuesAreEqual()
    {
        var comparer = new DynamicPropertyDescriptorComparer();
        var x = default(DynamicPropertyDescriptor);
        var y = default(DynamicPropertyDescriptor);
        var result = comparer.Compare(x, y);
        result.Should().Be(0);
    }

    [Fact]
    public void Compare_XHasAPropertyOrderButYDoesNot_XComesFirst()
    {
        var comparer = new DynamicPropertyDescriptorComparer();
        var x = new DynamicPropertyDescriptor(new MockPropertyDescriptor("X")).SetPropertyOrder(1);
        var y = new DynamicPropertyDescriptor(new MockPropertyDescriptor("Y")).SetPropertyOrder(null);
        var result = comparer.Compare(x, y);
        result.Should().BeLessThan(0);
    }

    [Fact]
    public void Compare_YHasAPropertyOrderButXDoesNot_YComesFirst()
    {
        var comparer = new DynamicPropertyDescriptorComparer();
        var x = new DynamicPropertyDescriptor(new MockPropertyDescriptor("X")).SetPropertyOrder(null);
        var y = new DynamicPropertyDescriptor(new MockPropertyDescriptor("Y")).SetPropertyOrder(1);
        var result = comparer.Compare(x, y);
        result.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Compare_BothHaveAPropertyOrderButXComesFirst_XComesFirst()
    {
        var comparer = new DynamicPropertyDescriptorComparer();
        var x = new DynamicPropertyDescriptor(new MockPropertyDescriptor("X")).SetPropertyOrder(0);
        var y = new DynamicPropertyDescriptor(new MockPropertyDescriptor("Y")).SetPropertyOrder(1);
        var result = comparer.Compare(x, y);
        result.Should().BeLessThan(0);
    }

    [Fact]
    public void Compare_BothHaveAPropertyOrderButYComesFirst_YComesFirst()
    {
        var comparer = new DynamicPropertyDescriptorComparer();
        var x = new DynamicPropertyDescriptor(new MockPropertyDescriptor("X")).SetPropertyOrder(1);
        var y = new DynamicPropertyDescriptor(new MockPropertyDescriptor("Y")).SetPropertyOrder(0);
        var result = comparer.Compare(x, y);
        result.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Compare_BothHaveEqualPropertyOrderButXComesFirstAlphabetically_XComesFirst()
    {
        var comparer = new DynamicPropertyDescriptorComparer();
        var x = new DynamicPropertyDescriptor(new MockPropertyDescriptor("0")).SetPropertyOrder(0);
        var y = new DynamicPropertyDescriptor(new MockPropertyDescriptor("1")).SetPropertyOrder(0);
        var result = comparer.Compare(x, y);
        result.Should().BeLessThan(0);
    }

    [Fact]
    public void Compare_BothHaveEqualPropertyOrderButYComesFirstAlphabetically_YComesFirst()
    {
        var comparer = new DynamicPropertyDescriptorComparer();
        var x = new DynamicPropertyDescriptor(new MockPropertyDescriptor("1")).SetPropertyOrder(0);
        var y = new DynamicPropertyDescriptor(new MockPropertyDescriptor("0")).SetPropertyOrder(0);
        var result = comparer.Compare(x, y);
        result.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Compare_NeitherValueHasAPropertyOrderButXComesFirstAlphabetically_XComesFirst()
    {
        var comparer = new DynamicPropertyDescriptorComparer();
        var x = new DynamicPropertyDescriptor(new MockPropertyDescriptor("0"));
        var y = new DynamicPropertyDescriptor(new MockPropertyDescriptor("1"));
        var result = comparer.Compare(x, y);
        result.Should().BeLessThan(0);
    }

    [Fact]
    public void Compare_NeitherValueHasAPropertyOrderButYComesFirstAlphabetically_YComesFirst()
    {
        var comparer = new DynamicPropertyDescriptorComparer();
        var x = new DynamicPropertyDescriptor(new MockPropertyDescriptor("1"));
        var y = new DynamicPropertyDescriptor(new MockPropertyDescriptor("0"));
        var result = comparer.Compare(x, y);
        result.Should().BeGreaterThan(0);
    }
}
