using System;
using System.ComponentModel;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace DynamicDescriptors.Tests
{
    public sealed class DynamicTypeDescriptorTests
    {
        [AttributeUsage(AttributeTargets.Property)]
        private sealed class AttributeOne : Attribute { }

        [AttributeUsage(AttributeTargets.Property)]
        private sealed class AttributeTwo : Attribute { }

        private sealed class ExampleType
        {
            [AttributeOne]
            public string Property1 { get; set; }

            [AttributeTwo]
            public string Property2 { get; set; }

            [AttributeOne, AttributeTwo]
            public string Property3 { get; set; }

            public bool Property4 { get; set; }

            public string field;

            public string Method() { return null; }
        }

        [Fact]
        public void Constructor_ParentIsNull_ThrowsArgumentNullException()
        {
            const string message = "parent should not be null.\r\nParameter name: parent";
            Action act = () => new DynamicTypeDescriptor(null);
            act.ShouldThrow<ArgumentNullException>().WithMessage(message);
        }

        [Fact]
        public void GetProperties_ReturnsNormalProperties()
        {
            var instance = new ExampleType();
            var descriptor = DynamicDescriptor.CreateFromInstance(instance);
            var properties = descriptor.GetProperties();

            bool containsProperty1 = false, containsProperty2 = false, containsProperty3 = false, containsProperty4 = false;
            foreach (PropertyDescriptor property in properties)
            {
                if (property.Name == "Property1") containsProperty1 = true;
                if (property.Name == "Property2") containsProperty2 = true;
                if (property.Name == "Property3") containsProperty3 = true;
                if (property.Name == "Property4") containsProperty4 = true;
            }

            containsProperty1.Should().BeTrue();
            containsProperty2.Should().BeTrue();
            containsProperty3.Should().BeTrue();
            containsProperty4.Should().BeTrue();
        }

        [Fact]
        public void GetProperties_SingleAttribute_ReturnsPropertiesWithThatAttribute()
        {
            var instance = new ExampleType();
            var descriptor = DynamicDescriptor.CreateFromInstance(instance);
            var properties = descriptor.GetProperties(new Attribute[] { new AttributeOne() });

            bool containsProperty1 = false, containsProperty2 = false, containsProperty3 = false, containsProperty4 = false;
            foreach (PropertyDescriptor property in properties)
            {
                if (property.Name == "Property1") containsProperty1 = true;
                if (property.Name == "Property2") containsProperty2 = true;
                if (property.Name == "Property3") containsProperty3 = true;
                if (property.Name == "Property4") containsProperty4 = true;
            }

            containsProperty1.Should().BeTrue();
            containsProperty2.Should().BeFalse();
            containsProperty3.Should().BeTrue();
            containsProperty4.Should().BeFalse();
        }

        [Fact]
        public void GetProperties_MultipleAttributes_ReturnsPropertiesWithAllOfThoseAttributes()
        {
            var instance = new ExampleType();
            var descriptor = DynamicDescriptor.CreateFromInstance(instance);
            var properties = descriptor.GetProperties(new Attribute[] { new AttributeOne(), new AttributeTwo() });

            bool containsProperty1 = false, containsProperty2 = false, containsProperty3 = false, containsProperty4 = false;
            foreach (PropertyDescriptor property in properties)
            {
                if (property.Name == "Property1") containsProperty1 = true;
                if (property.Name == "Property2") containsProperty2 = true;
                if (property.Name == "Property3") containsProperty3 = true;
                if (property.Name == "Property4") containsProperty4 = true;
            }

            containsProperty1.Should().BeFalse();
            containsProperty2.Should().BeFalse();
            containsProperty3.Should().BeTrue();
            containsProperty4.Should().BeFalse();
        }

        [Fact]
        public void GetProperties_SomePropertiesAreNotActive_ReturnsOnlyActiveProperties()
        {
            var instance = new ExampleType();
            var descriptor = DynamicDescriptor.CreateFromInstance(instance);

            descriptor.GetDynamicProperty("Property1").SetActive(true);
            descriptor.GetDynamicProperty("Property2").SetActive(false);
            descriptor.GetDynamicProperty("Property3").SetActive(true);
            descriptor.GetDynamicProperty("Property4").SetActive(false);

            var properties = descriptor.GetProperties();

            properties.Should().HaveCount(2);
            properties[0].Name.Should().Be("Property1");
            properties[1].Name.Should().Be("Property3");
        }

        [Fact]
        public void GetProperties_NullOrEmptyAttributeArray_ReturnsSameValueAsGetPropertiesWithoutAttributesSpecified()
        {
            var instance = new ExampleType();
            var descriptor = DynamicDescriptor.CreateFromInstance(instance);

            descriptor.GetProperties(null).Should().BeEquivalentTo(descriptor.GetProperties());
            descriptor.GetProperties(Array.Empty<Attribute>()).Should().BeEquivalentTo(descriptor.GetProperties());
        }

        [Fact]
        public void GetProperties_NoPropertyOrderSet_ReturnsPropertiesInAlphabeticalOrder()
        {
            var instance = new ExampleType();
            var descriptor = DynamicDescriptor.CreateFromInstance(instance);

            var properties = descriptor.GetProperties();

            properties[0].Name.Should().Be("Property1");
            properties[1].Name.Should().Be("Property2");
            properties[2].Name.Should().Be("Property3");
            properties[3].Name.Should().Be("Property4");
        }

        [Fact]
        public void GetProperties_PropertyOrderSet_ReturnsPropertiesInOrderSpecifiedByPropertyOrder()
        {
            var instance = new ExampleType();
            var descriptor = DynamicDescriptor.CreateFromInstance(instance);

            descriptor.GetDynamicProperty("Property1").SetPropertyOrder(3);
            descriptor.GetDynamicProperty("Property2").SetPropertyOrder(2);
            descriptor.GetDynamicProperty("Property3").SetPropertyOrder(1);
            descriptor.GetDynamicProperty("Property4").SetPropertyOrder(0);

            var properties = descriptor.GetProperties();

            properties[0].Name.Should().Be("Property4");
            properties[1].Name.Should().Be("Property3");
            properties[2].Name.Should().Be("Property2");
            properties[3].Name.Should().Be("Property1");
        }

        [Fact]
        public void GetProperties_PropertyOrderSet_MultiplePropertiesHaveTheSameOrder_ReturnThosePropertiesInAlphabeticalOrder()
        {
            var instance = new ExampleType();
            var descriptor = DynamicDescriptor.CreateFromInstance(instance);

            descriptor.GetDynamicProperty("Property1").SetPropertyOrder(1);
            descriptor.GetDynamicProperty("Property2").SetPropertyOrder(1);
            descriptor.GetDynamicProperty("Property3").SetPropertyOrder(0);
            descriptor.GetDynamicProperty("Property4").SetPropertyOrder(0);

            var properties = descriptor.GetProperties();

            properties[0].Name.Should().Be("Property3");
            properties[1].Name.Should().Be("Property4");
            properties[2].Name.Should().Be("Property1");
            properties[3].Name.Should().Be("Property2");
        }

        [Fact]
        public void GetProperties_SomePropertyOrdersSet_ReturnsPropertiesInOrderSpecifiedByPropertyOrderThenOtherPropertiesInAlphabeticalOrder()
        {
            var instance = new ExampleType();
            var descriptor = DynamicDescriptor.CreateFromInstance(instance);

            descriptor.GetDynamicProperty("Property1").SetPropertyOrder(null);
            descriptor.GetDynamicProperty("Property2").SetPropertyOrder(null);
            descriptor.GetDynamicProperty("Property3").SetPropertyOrder(1);
            descriptor.GetDynamicProperty("Property4").SetPropertyOrder(0);

            var properties = descriptor.GetProperties();

            properties[0].Name.Should().Be("Property4");
            properties[1].Name.Should().Be("Property3");
            properties[2].Name.Should().Be("Property1");
            properties[3].Name.Should().Be("Property2");
        }

        [Fact]
        public void GetDynamicProperty_PropertyNameDoesNotMatchAProperty_ReturnsNull()
        {
            var instance = new ExampleType();
            var descriptor = DynamicDescriptor.CreateFromInstance(instance);

            descriptor.GetDynamicProperty(null).Should().BeNull();
            descriptor.GetDynamicProperty("NotAValidPropertyName").Should().BeNull();
        }

        [Fact]
        public void GetDynamicProperty_PropertyNameMatchesAProperty_ReturnsDynamicPropertyDescriptorForThatProperty()
        {
            var instance = new ExampleType();
            var descriptor = DynamicDescriptor.CreateFromInstance(instance);

            var propertyDescriptor = descriptor.GetDynamicProperty("Property1");

            propertyDescriptor.Should().NotBeNull();
            propertyDescriptor.Name.Should().Be("Property1");
        }

        [Fact]
        public void GetDynamicProperty_ExpressionRefersToSomethingOtherThanAProperty_ReturnsNull()
        {
            var instance = new ExampleType();
            var descriptor = DynamicDescriptor.CreateFromInstance(instance);

            Action act1 = () => descriptor.GetDynamicProperty((ExampleType o) => o.field);
            act1.ShouldThrow<ArgumentException>().WithMessage("Expression 'o => o.field' refers to a field, not a property.");

            Action act2 = () => descriptor.GetDynamicProperty((ExampleType o) => o.Method());
            act2.ShouldThrow<ArgumentException>().WithMessage("Expression 'o => o.Method()' refers to a method, not a property.");
        }

        [Fact]
        public void GetDynamicProperty_ExpressionRefersToAReferenceTypeProperty_ReturnsDynamicPropertyDescriptorForThatProperty()
        {
            var instance = new ExampleType();
            var descriptor = DynamicDescriptor.CreateFromInstance(instance);

            var propertyDescriptor = descriptor.GetDynamicProperty((ExampleType o) => o.Property1);

            propertyDescriptor.Should().NotBeNull();
            propertyDescriptor.Name.Should().Be("Property1");
        }

        [Fact]
        public void GetDynamicProperty_ExpressionRefersToAValueTypeProperty_ReturnsDynamicPropertyDescriptorForThatProperty()
        {
            var instance = new ExampleType();
            var descriptor = DynamicDescriptor.CreateFromInstance(instance);

            var propertyDescriptor = descriptor.GetDynamicProperty((ExampleType o) => o.Property4);

            propertyDescriptor.Should().NotBeNull();
            propertyDescriptor.Name.Should().Be("Property4");
        }

        [Fact]
        public void GetDynamicProperties_AllPropertiesActive_ReturnsSequenceContainingAllDynamicProperties()
        {
            var instance = new ExampleType();
            var descriptor = DynamicDescriptor.CreateFromInstance(instance);

            var properties = descriptor.GetDynamicProperties().ToArray();

            properties.Should().HaveCount(4);
            properties[0].Name.Should().Be("Property1");
            properties[1].Name.Should().Be("Property2");
            properties[2].Name.Should().Be("Property3");
            properties[3].Name.Should().Be("Property4");
        }

        [Fact]
        public void GetDynamicProperties_SomePropertiesInactive_ReturnsSequenceContainingAllDynamicProperties()
        {
            var instance = new ExampleType();
            var descriptor = DynamicDescriptor.CreateFromInstance(instance);
            descriptor.GetDynamicProperty((ExampleType o) => o.Property1).SetActive(false);
            descriptor.GetDynamicProperty((ExampleType o) => o.Property4).SetActive(false);

            var properties = descriptor.GetDynamicProperties().ToArray();

            properties.Should().HaveCount(4);
            properties[0].Name.Should().Be("Property1");
            properties[1].Name.Should().Be("Property2");
            properties[2].Name.Should().Be("Property3");
            properties[3].Name.Should().Be("Property4");
        }

        [Fact]
        public void PropertySet_RaisesPropertyChangedEvent()
        {
            string propertyChanged = null;

            var instance = new ExampleType();
            var descriptor = DynamicDescriptor.CreateFromInstance(instance);
            descriptor.PropertyChanged += (s, e) =>
            {
                propertyChanged = e.PropertyName;
            };

            var property = descriptor.GetDynamicProperty(nameof(instance.Property1));
            property.SetValue(descriptor, "modified");

            instance.Property1.Should().Be("modified");
            propertyChanged.Should().Be("Property1");
        }

        [Fact]
        public void PropertyReset_RaisesPropertyChangedEvent()
        {
            string propertyChanged = null;

            var instance = new ExampleType();
            var descriptor = DynamicDescriptor.CreateFromInstance(instance);
            descriptor.PropertyChanged += (s, e) =>
            {
                propertyChanged = e.PropertyName;
            };

            var property = descriptor.GetDynamicProperty(nameof(instance.Property1));
            property.ResetValue(descriptor);

            instance.Property1.Should().BeNull();
            propertyChanged.Should().Be("Property1");
        }
    }
}
