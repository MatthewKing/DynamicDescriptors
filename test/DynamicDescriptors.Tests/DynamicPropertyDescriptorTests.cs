using System;
using System.ComponentModel;
using FluentAssertions;
using Xunit;

namespace DynamicDescriptors.Tests
{
    public sealed class DynamicPropertyDescriptorTests
    {
        [Fact]
        public void Constructor_DescriptorIsNull_ThrowsArgumentNullException()
        {
            const string message = "descriptor should not be null.\r\nParameter name: descriptor";
            Action act = () => new DynamicPropertyDescriptor(null);
            act.ShouldThrow<ArgumentNullException>().WithMessage(message);
        }

        [Fact]
        public void CanResetValue_ReturnsResultOfDescriptorCanResetValue()
        {
            var mockDescriptor = new MockPropertyDescriptor();
            var dynamicDescriptor = new DynamicPropertyDescriptor(mockDescriptor);

            var component = new object();

            mockDescriptor.CanResetValueResult = true;
            dynamicDescriptor.CanResetValue(component).Should().BeTrue();
            mockDescriptor.CanResetValueComponent.Should().Be(component);

            mockDescriptor.CanResetValueResult = false;
            dynamicDescriptor.CanResetValue(component).Should().BeFalse();
            mockDescriptor.CanResetValueComponent.Should().Be(component);
        }

        [Fact]
        public void ComponentType_ReturnsResultOfDescriptorComponentType()
        {
            var mockDescriptor = new MockPropertyDescriptor();
            var dynamicDescriptor = new DynamicPropertyDescriptor(mockDescriptor);

            var type = typeof(string);
            mockDescriptor.ComponentTypeResult = type;
            dynamicDescriptor.ComponentType.Should().Be(type);
        }

        [Fact]
        public void GetValue_ReturnsResultOfDescriptorGetValue()
        {
            var mockDescriptor = new MockPropertyDescriptor();
            var dynamicDescriptor = new DynamicPropertyDescriptor(mockDescriptor);

            var component = new object();
            var result = new object();

            mockDescriptor.GetValueResult = result;
            dynamicDescriptor.GetValue(component).Should().Be(result);
            mockDescriptor.GetValueComponent.Should().Be(component);
        }

        [Fact]
        public void PropertyType_ReturnsResultOfDescriptorPropertyType()
        {
            var mockDescriptor = new MockPropertyDescriptor();
            var dynamicDescriptor = new DynamicPropertyDescriptor(mockDescriptor);

            var type = typeof(string);
            mockDescriptor.PropertyTypeResult = type;
            dynamicDescriptor.PropertyType.Should().Be(type);
        }

        [Fact]
        public void ResetValue_CallsDescriptorResetValue()
        {
            var mockDescriptor = new MockPropertyDescriptor();
            var dynamicDescriptor = new DynamicPropertyDescriptor(mockDescriptor);

            var component = new object();
            dynamicDescriptor.ResetValue(component);
            mockDescriptor.ResetValueComponent.Should().Be(component);
            mockDescriptor.ResetValueCalled.Should().BeTrue();
        }

        [Fact]
        public void SetValue_CallsDescriptorSetValue()
        {
            var mockDescriptor = new MockPropertyDescriptor();
            var dynamicDescriptor = new DynamicPropertyDescriptor(mockDescriptor);

            var component = new object();
            var value = new object();
            dynamicDescriptor.SetValue(component, value);

            mockDescriptor.SetValueComponent.Should().Be(component);
            mockDescriptor.SetValueValue.Should().Be(value);
            mockDescriptor.SetValueCalled.Should().BeTrue();
        }

        [Fact]
        public void ShouldSerializeValue_ReturnsResultOfDescriptorShouldSerializeValue()
        {
            var mockDescriptor = new MockPropertyDescriptor();
            var dynamicDescriptor = new DynamicPropertyDescriptor(mockDescriptor);

            var component = new object();

            mockDescriptor.ShouldSerializeValueResult = true;
            dynamicDescriptor.ShouldSerializeValue(component).Should().BeTrue();
            mockDescriptor.ShouldSerializeValueComponent.Should().Be(component);

            mockDescriptor.ShouldSerializeValueResult = false;
            dynamicDescriptor.ShouldSerializeValue(component).Should().BeFalse();
            mockDescriptor.ShouldSerializeValueComponent.Should().Be(component);
        }

        [Fact]
        public void Category_NoOverride_ReturnsDescriptorValue()
        {
            var mockDescriptor = new MockPropertyDescriptor();
            mockDescriptor.CategoryResult = "Base";

            var dynamicDescriptor = new DynamicPropertyDescriptor(mockDescriptor);

            dynamicDescriptor.Category.Should().Be("Base");
        }

        [Fact]
        public void Category_Override_ReturnsOverrideValue()
        {
            var mockDescriptor = new MockPropertyDescriptor();
            mockDescriptor.CategoryResult = "Base";

            var dynamicDescriptor = new DynamicPropertyDescriptor(mockDescriptor);
            dynamicDescriptor.SetCategory("Override");

            dynamicDescriptor.Category.Should().Be("Override");
        }

        [Fact]
        public void Converter_NoOverride_ReturnsDescriptorValue()
        {
            var converter = new TypeConverter();

            var mockDescriptor = new MockPropertyDescriptor();
            mockDescriptor.ConverterResult = converter;

            var dynamicDescriptor = new DynamicPropertyDescriptor(mockDescriptor);

            dynamicDescriptor.Converter.Should().Be(converter);
        }

        [Fact]
        public void Converter_Override_ReturnsOverrideValue()
        {
            var converterBase = new TypeConverter();
            var converterOverride = new TypeConverter();

            var mockDescriptor = new MockPropertyDescriptor();
            mockDescriptor.ConverterResult = converterBase;

            var dynamicDescriptor = new DynamicPropertyDescriptor(mockDescriptor);
            dynamicDescriptor.SetConverter(converterOverride);

            dynamicDescriptor.Converter.Should().Be(converterOverride);
        }

        [Fact]
        public void Description_NoOverride_ReturnsDescriptorValue()
        {
            var mockDescriptor = new MockPropertyDescriptor();
            mockDescriptor.DescriptionResult = "Base";

            var dynamicDescriptor = new DynamicPropertyDescriptor(mockDescriptor);

            dynamicDescriptor.Description.Should().Be("Base");
        }

        [Fact]
        public void Description_Override_ReturnsOverrideValue()
        {
            var mockDescriptor = new MockPropertyDescriptor();
            mockDescriptor.DescriptionResult = "Base";

            var dynamicDescriptor = new DynamicPropertyDescriptor(mockDescriptor);
            dynamicDescriptor.SetDescription("Override");

            dynamicDescriptor.Description.Should().Be("Override");
        }

        [Fact]
        public void DisplayName_NoOverride_ReturnsDescriptorValue()
        {
            var mockDescriptor = new MockPropertyDescriptor();
            mockDescriptor.DisplayNameResult = "Base";

            var dynamicDescriptor = new DynamicPropertyDescriptor(mockDescriptor);

            dynamicDescriptor.DisplayName.Should().Be("Base");
        }

        [Fact]
        public void DisplayName_Override_ReturnsOverrideValue()
        {
            var mockDescriptor = new MockPropertyDescriptor();
            mockDescriptor.DisplayNameResult = "Base";

            var dynamicDescriptor = new DynamicPropertyDescriptor(mockDescriptor);
            dynamicDescriptor.SetDisplayName("Override");

            dynamicDescriptor.DisplayName.Should().Be("Override");
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsReadOnly_NoOverride_ReturnsDescriptorValue(bool value)
        {
            var mockDescriptor = new MockPropertyDescriptor();
            mockDescriptor.IsReadOnlyResult = value;

            var dynamicDescriptor = new DynamicPropertyDescriptor(mockDescriptor);

            dynamicDescriptor.IsReadOnly.Should().Be(value);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public void IsReadOnly_Override_ReturnsOverrideValue(bool descriptorValue, bool overrideValue)
        {
            var mockDescriptor = new MockPropertyDescriptor();
            mockDescriptor.IsReadOnlyResult = descriptorValue;

            var dynamicDescriptor = new DynamicPropertyDescriptor(mockDescriptor);
            dynamicDescriptor.SetReadOnly(overrideValue);

            dynamicDescriptor.IsReadOnly.Should().Be(overrideValue);
        }

        [Fact]
        public void GetEditor_NoOverride_ReturnsDescriptorValue()
        {
            var baseEditor = new MockUITypeEditor();

            var mockDescriptor = new MockPropertyDescriptor();
            mockDescriptor.GetEditorResult = baseEditor;

            var dynamicDescriptor = new DynamicPropertyDescriptor(mockDescriptor);

            dynamicDescriptor.GetEditor(typeof(MockUITypeEditor)).Should().Be(baseEditor);
        }

        [Fact]
        public void GetEditor_Override_ReturnsOverrideValue()
        {
            var baseEditor = new MockUITypeEditor();
            var overrideEditor = new MockUITypeEditor();

            var mockDescriptor = new MockPropertyDescriptor();
            mockDescriptor.GetEditorResult = baseEditor;

            var dynamicDescriptor = new DynamicPropertyDescriptor(mockDescriptor);
            dynamicDescriptor.SetEditor(typeof(MockUITypeEditor), overrideEditor);

            dynamicDescriptor.GetEditor(typeof(MockUITypeEditor)).Should().Be(overrideEditor);
        }

        [Fact]
        public void GetEditor_OverrideThenClear_ReturnsDescriptorValue()
        {
            var baseEditor = new MockUITypeEditor();
            var overrideEditor = new MockUITypeEditor();

            var mockDescriptor = new MockPropertyDescriptor();
            mockDescriptor.GetEditorResult = baseEditor;

            var dynamicDescriptor = new DynamicPropertyDescriptor(mockDescriptor);
            dynamicDescriptor.SetEditor(typeof(MockUITypeEditor), overrideEditor);
            dynamicDescriptor.SetEditor(typeof(MockUITypeEditor), null);

            dynamicDescriptor.GetEditor(typeof(MockUITypeEditor)).Should().Be(baseEditor);
        }

        [Fact]
        public void GetEditor_MultipleOverrides_ReturnsMostRecentOverrideValue()
        {
            var baseEditor = new MockUITypeEditor();
            var overrideEditor1 = new MockUITypeEditor();
            var overrideEditor2 = new MockUITypeEditor();

            var mockDescriptor = new MockPropertyDescriptor();
            mockDescriptor.GetEditorResult = baseEditor;

            var dynamicDescriptor = new DynamicPropertyDescriptor(mockDescriptor);
            dynamicDescriptor.SetEditor(typeof(MockUITypeEditor), overrideEditor1);
            dynamicDescriptor.SetEditor(typeof(MockUITypeEditor), overrideEditor2);

            dynamicDescriptor.GetEditor(typeof(MockUITypeEditor)).Should().Be(overrideEditor2);
        }
    }
}
