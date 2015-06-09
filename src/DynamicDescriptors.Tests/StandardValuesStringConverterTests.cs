namespace DynamicDescriptors.Tests
{
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    internal sealed class StandardValuesStringConverterTests
    {
        [Test]
        public void Constructor_ValuesIsNull_DoesNotThrowException()
        {
            Assert.That(() => new StandardValuesStringConverter(null), Throws.Nothing);
        }

        [Test]
        public void GetStandardValues_UsesDeferredExecution()
        {
            int divisor = 1;
            var values = Enumerable.Range(1, 10).Where(i => i % divisor == 0).Select(i => i.ToString());
            var converter = new StandardValuesStringConverter(values);

            divisor = 2;
            Assert.That(converter.GetStandardValues(), Is.EquivalentTo(new[] { "2", "4", "6", "8", "10" }));

            divisor = 3;
            Assert.That(converter.GetStandardValues(), Is.EquivalentTo(new[] { "3", "6", "9" }));
        }
    }
}
