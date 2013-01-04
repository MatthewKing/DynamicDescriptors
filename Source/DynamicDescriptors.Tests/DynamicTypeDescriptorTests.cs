namespace DynamicDescriptors.Tests
{
    using System;
    using System.ComponentModel;
    using NUnit.Framework;

    [TestFixture]
    internal sealed class DynamicTypeDescriptorTests
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
        }

        [Test]
        public void Constructor_ParentIsNull_ThrowsArgumentNullException()
        {
            const string message = "parent should not be null.";
            Assert.That(() => new DynamicTypeDescriptor<object>(null),
                Throws.TypeOf<ArgumentNullException>()
                      .And.Message.Contains(message));
        }

        [Test]
        public void GetProperties_ReturnsNormalProperties()
        {
            ExampleType instance = new ExampleType();
            DynamicTypeDescriptor<ExampleType> descriptor = DynamicDescriptor.Create(instance);
            PropertyDescriptorCollection properties = descriptor.GetProperties();

            bool containsProperty1 = false, containsProperty2 = false, containsProperty3 = false;
            foreach (PropertyDescriptor property in properties)
            {
                if (property.Name == "Property1") containsProperty1 = true;
                if (property.Name == "Property2") containsProperty2 = true;
                if (property.Name == "Property3") containsProperty3 = true;
            }

            Assert.That(containsProperty1, Is.True);
            Assert.That(containsProperty2, Is.True);
            Assert.That(containsProperty3, Is.True);
        }

        [Test]
        public void GetProperties_SingleAttribute_ReturnsPropertiesWithThatAttribute()
        {
            ExampleType instance = new ExampleType();
            DynamicTypeDescriptor<ExampleType> descriptor = DynamicDescriptor.Create(instance);
            PropertyDescriptorCollection properties = descriptor.GetProperties(new Attribute[] { new AttributeOne() });

            bool containsProperty1 = false, containsProperty2 = false, containsProperty3 = false;
            foreach (PropertyDescriptor property in properties)
            {
                if (property.Name == "Property1") containsProperty1 = true;
                if (property.Name == "Property2") containsProperty2 = true;
                if (property.Name == "Property3") containsProperty3 = true;
            }

            Assert.That(containsProperty1, Is.True);
            Assert.That(containsProperty2, Is.False);
            Assert.That(containsProperty3, Is.True);
        }

        [Test]
        public void GetProperties_MultipleAttributes_ReturnsPropertiesWithAllOfThoseAttributes()
        {
            ExampleType instance = new ExampleType();
            DynamicTypeDescriptor<ExampleType> descriptor = DynamicDescriptor.Create(instance);
            PropertyDescriptorCollection properties = descriptor.GetProperties(new Attribute[] { new AttributeOne(), new AttributeTwo() });

            bool containsProperty1 = false, containsProperty2 = false, containsProperty3 = false;
            foreach (PropertyDescriptor property in properties)
            {
                if (property.Name == "Property1") containsProperty1 = true;
                if (property.Name == "Property2") containsProperty2 = true;
                if (property.Name == "Property3") containsProperty3 = true;
            }

            Assert.That(containsProperty1, Is.False);
            Assert.That(containsProperty2, Is.False);
            Assert.That(containsProperty3, Is.True);
        }

        [Test]
        public void GetProperties_NullOrEmptyAttributeArray_ReturnsSameValueAsGetPropertiesWithoutAttributesSpecified()
        {
            ExampleType instance = new ExampleType();
            DynamicTypeDescriptor<ExampleType> descriptor = DynamicDescriptor.Create(instance);

            Assert.That(descriptor.GetProperties(null), Is.EqualTo(descriptor.GetProperties()));
            Assert.That(descriptor.GetProperties(new Attribute[] { }), Is.EqualTo(descriptor.GetProperties()));
        }

        [Test]
        public void GetDynamicProperty_PropertyNameDoesNotMatchAProperty_ReturnsNull()
        {
            ExampleType instance = new ExampleType();
            DynamicTypeDescriptor<ExampleType> descriptor = DynamicDescriptor.Create(instance);

            Assert.That(descriptor.GetDynamicProperty(null), Is.Null);
            Assert.That(descriptor.GetDynamicProperty("NotAValidPropertyName"), Is.Null);
        }

        [Test]
        public void GetDynamicProperty_PropertyNameMatchesAProperty_ReturnsDynamicPropertyDescriptorForThatProperty()
        {
            ExampleType instance = new ExampleType();
            DynamicTypeDescriptor<ExampleType> descriptor = DynamicDescriptor.Create(instance);

            DynamicPropertyDescriptor propertyDescriptor = descriptor.GetDynamicProperty("Property1");

            Assert.That(propertyDescriptor, Is.Not.Null);
            Assert.That(propertyDescriptor.Name, Is.EqualTo("Property1"));
        }
   }
}
