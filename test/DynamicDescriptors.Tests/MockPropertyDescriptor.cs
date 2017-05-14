using System;
using System.ComponentModel;

namespace DynamicDescriptors.Tests
{
    internal sealed class MockPropertyDescriptor : PropertyDescriptor
    {
        public MockPropertyDescriptor()
            : this("MockProperty") { }

        public MockPropertyDescriptor(string name)
            : base(name, new Attribute[] { }) { }

        public Type ComponentTypeResult { get; set; }
        public override Type ComponentType { get { return this.ComponentTypeResult; } }

        public bool IsReadOnlyResult { get; set; }
        public override bool IsReadOnly { get { return this.IsReadOnlyResult; } }

        public Type PropertyTypeResult { get; set; }
        public override Type PropertyType { get { return this.PropertyTypeResult; } }

        public object GetEditorResult { get; set; }
        public object GetEditorEditorBaseType { get; private set; }
        public bool GetEditorCalled { get; private set; }
        public override object GetEditor(Type editorBaseType)
        {
            GetEditorCalled = true;
            GetEditorEditorBaseType = editorBaseType;
            return this.GetEditorResult;
        }

        public object GetValueResult { get; set; }
        public object GetValueComponent { get; private set; }
        public bool GetValueCalled { get; private set; }
        public override object GetValue(object component)
        {
            GetValueCalled = true;
            GetValueComponent = component;
            return this.GetValueResult;
        }

        public object SetValueComponent { get; private set; }
        public object SetValueValue { get; private set; }
        public bool SetValueCalled { get; private set; }
        public override void SetValue(object component, object value)
        {
            SetValueCalled = true;
            SetValueComponent = component;
            SetValueValue = value;
        }

        public object ResetValueComponent { get; private set; }
        public bool ResetValueCalled { get; private set; }
        public override void ResetValue(object component)
        {
            ResetValueCalled = true;
            ResetValueComponent = component;
        }

        public bool CanResetValueResult { get; set; }
        public object CanResetValueComponent { get; private set; }
        public bool CanResetValueCalled { get; private set; }
        public override bool CanResetValue(object component)
        {
            CanResetValueCalled = true;
            CanResetValueComponent = component;
            return CanResetValueResult;
        }

        public bool ShouldSerializeValueResult { get; set; }
        public object ShouldSerializeValueComponent { get; private set; }
        public bool ShouldSerializeValueCalled { get; private set; }
        public override bool ShouldSerializeValue(object component)
        {
            ShouldSerializeValueCalled = true;
            ShouldSerializeValueComponent = component;
            return ShouldSerializeValueResult;
        }

        public string CategoryResult { get; set; }
        public override string Category { get { return CategoryResult; } }

        public TypeConverter ConverterResult { get; set; }
        public override TypeConverter Converter { get { return ConverterResult; } }

        public string DescriptionResult { get; set; }
        public override string Description { get { return DescriptionResult; } }

        public string DisplayNameResult { get; set; }
        public override string DisplayName { get { return DisplayNameResult; } }
    }
}
