using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace DynamicDescriptors.Tests
{
    public sealed class StandardValuesStringConverterTests
    {
        [Fact]
        public void EnumerableConstructor_ValuesIsNull_DoesNotThrowException()
        {
            Action act = () => new StandardValuesStringConverter(null as IEnumerable<string>);
            act.ShouldNotThrow();
        }

        [Fact]
        public void FuncConstructor_ValuesIsNull_DoesNotThrowException()
        {
            Action act = () => new StandardValuesStringConverter(null as Func<string[]>);
            act.ShouldNotThrow();
        }

        [Fact]
        public void GetStandardValues_NoValuesFactoryProvided_ReturnsEmptyCollection()
        {
            var converter1 = new StandardValuesStringConverter(null as Func<string[]>);
            converter1.GetStandardValues().Should().BeEmpty();

            var converter2 = new StandardValuesStringConverter(null as IEnumerable<string>);
            converter2.GetStandardValues().Should().BeEmpty();
        }

        [Fact]
        public void GetStandardValues_UsesDeferredExecution()
        {
            var divisor = 1;
            var values = Enumerable.Range(1, 10).Where(i => i % divisor == 0).Select(i => i.ToString());
            var converter = new StandardValuesStringConverter(values);

            divisor = 2;
            converter.GetStandardValues().Should().ContainInOrder(new string[] { "2", "4", "6", "8", "10" });

            divisor = 3;
            converter.GetStandardValues().Should().ContainInOrder(new string[] { "3", "6", "9" });
        }
    }
}
