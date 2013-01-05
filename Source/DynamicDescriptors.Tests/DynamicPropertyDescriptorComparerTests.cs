namespace DynamicDescriptors.Tests
{
    using NUnit.Framework;

    [TestFixture]
    internal sealed class DynamicPropertyDescriptorComparerTests
    {
        [Test]
        public void Compare_XIsNullAndYIsNot_YComesFirst()
        {
            DynamicPropertyDescriptorComparer comparer = new DynamicPropertyDescriptorComparer();
            DynamicPropertyDescriptor x = null;
            DynamicPropertyDescriptor y = new DynamicPropertyDescriptor(new MockPropertyDescriptor("Y"));
            int result = comparer.Compare(x, y);
            Assert.That(result, Is.GreaterThan(0));
        }

        [Test]
        public void Compare_YIsNullAndXIsNot_XComesFirst()
        {
            DynamicPropertyDescriptorComparer comparer = new DynamicPropertyDescriptorComparer();
            DynamicPropertyDescriptor x = new DynamicPropertyDescriptor(new MockPropertyDescriptor("X"));
            DynamicPropertyDescriptor y = null;
            int result = comparer.Compare(x, y);
            Assert.That(result, Is.LessThan(0));
        }

        [Test]
        public void Compare_BothXAndYAreNull_ValuesAreEqual()
        {
            DynamicPropertyDescriptorComparer comparer = new DynamicPropertyDescriptorComparer();
            DynamicPropertyDescriptor x = null;
            DynamicPropertyDescriptor y = null;
            int result = comparer.Compare(x, y);
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Compare_XHasAPropertyOrderButYDoesNot_XComesFirst()
        {
            DynamicPropertyDescriptorComparer comparer = new DynamicPropertyDescriptorComparer();
            DynamicPropertyDescriptor x = new DynamicPropertyDescriptor(new MockPropertyDescriptor("X")).SetPropertyOrder(1);
            DynamicPropertyDescriptor y = new DynamicPropertyDescriptor(new MockPropertyDescriptor("Y")).SetPropertyOrder(null);
            int result = comparer.Compare(x, y);
            Assert.That(result, Is.LessThan(0));
        }

        [Test]
        public void Compare_YHasAPropertyOrderButXDoesNot_YComesFirst()
        {
            DynamicPropertyDescriptorComparer comparer = new DynamicPropertyDescriptorComparer();
            DynamicPropertyDescriptor x = new DynamicPropertyDescriptor(new MockPropertyDescriptor("X")).SetPropertyOrder(null);
            DynamicPropertyDescriptor y = new DynamicPropertyDescriptor(new MockPropertyDescriptor("Y")).SetPropertyOrder(1);
            int result = comparer.Compare(x, y);
            Assert.That(result, Is.GreaterThan(0));
        }

        [Test]
        public void Compare_BothHaveAPropertyOrderButXComesFirst_XComesFirst()
        {
            DynamicPropertyDescriptorComparer comparer = new DynamicPropertyDescriptorComparer();
            DynamicPropertyDescriptor x = new DynamicPropertyDescriptor(new MockPropertyDescriptor("X")).SetPropertyOrder(0);
            DynamicPropertyDescriptor y = new DynamicPropertyDescriptor(new MockPropertyDescriptor("Y")).SetPropertyOrder(1);
            int result = comparer.Compare(x, y);
            Assert.That(result, Is.LessThan(0));
        }

        [Test]
        public void Compare_BothHaveAPropertyOrderButYComesFirst_YComesFirst()
        {
            DynamicPropertyDescriptorComparer comparer = new DynamicPropertyDescriptorComparer();
            DynamicPropertyDescriptor x = new DynamicPropertyDescriptor(new MockPropertyDescriptor("X")).SetPropertyOrder(1);
            DynamicPropertyDescriptor y = new DynamicPropertyDescriptor(new MockPropertyDescriptor("Y")).SetPropertyOrder(0);
            int result = comparer.Compare(x, y);
            Assert.That(result, Is.GreaterThan(0));
        }

        [Test]
        public void Compare_BothHaveEqualPropertyOrderButXComesFirstAlphabetically_XComesFirst()
        {
            DynamicPropertyDescriptorComparer comparer = new DynamicPropertyDescriptorComparer();
            DynamicPropertyDescriptor x = new DynamicPropertyDescriptor(new MockPropertyDescriptor("0")).SetPropertyOrder(0);
            DynamicPropertyDescriptor y = new DynamicPropertyDescriptor(new MockPropertyDescriptor("1")).SetPropertyOrder(0);
            int result = comparer.Compare(x, y);
            Assert.That(result, Is.LessThan(0));
        }

        [Test]
        public void Compare_BothHaveEqualPropertyOrderButYComesFirstAlphabetically_YComesFirst()
        {
            DynamicPropertyDescriptorComparer comparer = new DynamicPropertyDescriptorComparer();
            DynamicPropertyDescriptor x = new DynamicPropertyDescriptor(new MockPropertyDescriptor("1")).SetPropertyOrder(0);
            DynamicPropertyDescriptor y = new DynamicPropertyDescriptor(new MockPropertyDescriptor("0")).SetPropertyOrder(0);
            int result = comparer.Compare(x, y);
            Assert.That(result, Is.GreaterThan(0));
        }

        [Test]
        public void Compare_NeitherValueHasAPropertyOrderButXComesFirstAlphabetically_XComesFirst()
        {
            DynamicPropertyDescriptorComparer comparer = new DynamicPropertyDescriptorComparer();
            DynamicPropertyDescriptor x = new DynamicPropertyDescriptor(new MockPropertyDescriptor("0"));
            DynamicPropertyDescriptor y = new DynamicPropertyDescriptor(new MockPropertyDescriptor("1"));
            int result = comparer.Compare(x, y);
            Assert.That(result, Is.LessThan(0));
        }

        [Test]
        public void Compare_NeitherValueHasAPropertyOrderButYComesFirstAlphabetically_YComesFirst()
        {
            DynamicPropertyDescriptorComparer comparer = new DynamicPropertyDescriptorComparer();
            DynamicPropertyDescriptor x = new DynamicPropertyDescriptor(new MockPropertyDescriptor("1"));
            DynamicPropertyDescriptor y = new DynamicPropertyDescriptor(new MockPropertyDescriptor("0"));
            int result = comparer.Compare(x, y);
            Assert.That(result, Is.GreaterThan(0));
        }
    }
}
