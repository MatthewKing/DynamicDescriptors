namespace DynamicDescriptors.Tests
{
    using System;
    using System.ComponentModel;
    using NUnit.Framework;

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
    }
}
