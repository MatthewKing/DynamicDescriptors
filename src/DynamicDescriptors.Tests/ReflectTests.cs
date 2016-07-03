using System;
using NUnit.Framework;

namespace DynamicDescriptors.Tests
{
    [TestFixture]
    internal sealed class ReflectTests
    {
        private sealed class ExampleClass
        {
            public string field;
            public string Method() { return null; }
            public string Property { get; set; }
        }

        [Test]
        public void GetPropertyName_ExpressionRefersToAMethod_ThrowsArgumentException()
        {
            const string message = "Expression 'o => o.Method()' refers to a method, not a property.";
            Assert.That(() => Reflect.GetPropertyName<ExampleClass, string>(o => o.Method()),
                Throws.TypeOf<ArgumentException>()
                      .And.Message.Contains(message));
        }

        [Test]
        public void GetPropertyName_ExpressionRefersToAField_ThrowsArgumentException()
        {
            const string message = "Expression 'o => o.field' refers to a field, not a property.";
            Assert.That(() => Reflect.GetPropertyName<ExampleClass, string>(o => o.field),
                Throws.TypeOf<ArgumentException>()
                      .And.Message.Contains(message));
        }

        [Test]
        public void GetPropertyName_ReturnsCorrectValue()
        {
            var value = Reflect.GetPropertyName<ExampleClass, string>(o => o.Property);
            var expected = "Property";

            Assert.That(value, Is.EqualTo(expected));
        }

        [Test]
        public void GetPropertyInfo_ExpressionRefersToAMethod_ThrowsArgumentException()
        {
            const string message = "Expression 'o => o.Method()' refers to a method, not a property.";
            Assert.That(() => Reflect.GetPropertyInfo<ExampleClass, string>(o => o.Method()),
                Throws.TypeOf<ArgumentException>()
                      .And.Message.Contains(message));
        }

        [Test]
        public void GetPropertyInfo_ExpressionRefersToAField_ThrowsArgumentException()
        {
            const string message = "Expression 'o => o.field' refers to a field, not a property.";
            Assert.That(() => Reflect.GetPropertyInfo<ExampleClass, string>(o => o.field),
                Throws.TypeOf<ArgumentException>()
                      .And.Message.Contains(message));
        }

        [Test]
        public void GetPropertyInfo_ReturnsCorrectValue()
        {
            var value = Reflect.GetPropertyInfo<ExampleClass, string>(o => o.Property);
            var expected = typeof(ExampleClass).GetProperty("Property");

            Assert.That(value, Is.EqualTo(expected));
        }
    }
}
