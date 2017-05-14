using System;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace DynamicDescriptors.Tests
{
    public sealed class ReflectTests
    {
        private sealed class ExampleClass
        {
            public string field;
            public string Method() { return null; }
            public string Property { get; set; }
        }

        [Fact]
        public void GetPropertyName_ExpressionRefersToAMethod_ThrowsArgumentException()
        {
            const string message = "Expression 'o => o.Method()' refers to a method, not a property.";
            Action act = () => Reflect.GetPropertyName<ExampleClass, string>(o => o.Method());
            act.ShouldThrow<ArgumentException>().WithMessage(message);
        }

        [Fact]
        public void GetPropertyName_ExpressionRefersToAField_ThrowsArgumentException()
        {
            const string message = "Expression 'o => o.field' refers to a field, not a property.";
            Action act = () => Reflect.GetPropertyName<ExampleClass, string>(o => o.field);
            act.ShouldThrow<ArgumentException>().WithMessage(message);
        }

        [Fact]
        public void GetPropertyName_ReturnsCorrectValue()
        {
            var value = Reflect.GetPropertyName<ExampleClass, string>(o => o.Property);
            var expected = "Property";

            value.Should().Be(expected);
        }

        [Fact]
        public void GetPropertyInfo_ExpressionRefersToAMethod_ThrowsArgumentException()
        {
            const string message = "Expression 'o => o.Method()' refers to a method, not a property.";
            Action act = () => Reflect.GetPropertyInfo<ExampleClass, string>(o => o.Method());
            act.ShouldThrow<ArgumentException>().WithMessage(message);
        }

        [Fact]
        public void GetPropertyInfo_ExpressionRefersToAField_ThrowsArgumentException()
        {
            const string message = "Expression 'o => o.field' refers to a field, not a property.";
            Action act = () => Reflect.GetPropertyInfo<ExampleClass, string>(o => o.field);
            act.ShouldThrow<ArgumentException>().WithMessage(message);
        }

        [Fact]
        public void GetPropertyInfo_ReturnsCorrectValue()
        {
            var value = Reflect.GetPropertyInfo<ExampleClass, string>(o => o.Property);
            var expected = typeof(ExampleClass).GetTypeInfo().GetProperty("Property");

            value.Should().BeSameAs(expected);
        }
    }
}
