namespace DynamicDescriptors.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    internal sealed class DynamicDescriptorTests
    {
        [Test]
        public void Create_InstanceIsNull_ThrowsArgumentNullException()
        {
            const string message = "instance should not be null.";
            Assert.That(() => DynamicDescriptor.Create<object>(null),
                Throws.TypeOf<ArgumentNullException>()
                      .And.Message.Contains(message));
        }

        [Test]
        public void Create_InstanceIsNotNull_ReturnsNewDynamicTypeDescriptorInstance()
        {
            object instance = new object();
            DynamicTypeDescriptor<object> descriptor = DynamicDescriptor.Create(instance);

            Assert.That(descriptor, Is.Not.Null);
        }
    }
}
