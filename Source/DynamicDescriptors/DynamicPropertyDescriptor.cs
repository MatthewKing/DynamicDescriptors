namespace DynamicDescriptors
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// A runtime-customizable implementation of PropertyDescriptor.
    /// </summary>
    public sealed class DynamicPropertyDescriptor : PropertyDescriptor
    {
        /// <summary>
        /// The PropertyDescriptor on which this DynamicPropertyDescriptor is based.
        /// </summary>
        private readonly PropertyDescriptor descriptor;

        /// <summary>
        /// A dictionary mapping editor base types to editor instances.
        /// </summary>
        private readonly IDictionary<Type, object> editorDictionary;

        /// <summary>
        /// If this value is not null, it will be returned by the Category property;
        /// otherwise, the base descriptor's Category property will be returned.
        /// </summary>
        private string categoryOverride;

        /// <summary>
        /// If this value is not null, it will be returned by the Converter property;
        /// otherwise, the base descriptor's Converter property will be returned.
        /// </summary>
        private TypeConverter converterOverride;

        /// <summary>
        /// If this value is not null, it will be returned by the Description property;
        /// otherwise, the base descriptor's Description property will be returned.
        /// </summary>
        private string descriptionOverride;

        /// <summary>
        /// If this value is not null, it will be returned by the DisplayName property;
        /// otherwise, the base descriptor's DisplayName property will be returned.
        /// </summary>
        private string displayNameOverride;

        /// <summary>
        /// If this value is not null, it will be returned by the IsReadOnly property;
        /// otherwise, the base descriptor's IsReadOnly property will be returned.
        /// </summary>
        private Nullable<bool> isReadOnlyOverride;

        /// <summary>
        /// The order in which this property will be retrieved from its type descriptor.
        /// </summary>
        private Nullable<int> propertyOrder;

        /// <summary>
        /// Initializes a new instance of the DynamicPropertyDescriptor class.
        /// </summary>
        /// <param name="descriptor">
        /// The PropertyDescriptor on which this DynamicPropertyDescriptor will be based.
        /// </param>
        public DynamicPropertyDescriptor(PropertyDescriptor descriptor)
            : base(Preconditions.CheckNotNull(descriptor, "descriptor"))
        {
            this.descriptor = descriptor;
            this.editorDictionary = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Returns a value indicating whether resetting an object changes its value.
        /// </summary>
        /// <param name="component">The component to test for reset capability.</param>
        /// <returns>true if resetting the component changes its value; otherwise, false.</returns>
        public override bool CanResetValue(object component)
        {
            return this.descriptor.CanResetValue(component);
        }

        /// <summary>
        /// Gets the name of the category to which the member belongs, as specified in the
        /// System.ComponentModel.CategoryAttribute.
        /// </summary>
        public override string Category
        {
            get
            {
                return this.categoryOverride ?? this.descriptor.Category;
            }
        }

        /// <summary>
        /// Gets the type of the component this property is bound to.
        /// </summary>
        public override Type ComponentType
        {
            get { return this.descriptor.ComponentType; }
        }

        /// <summary>
        /// Gets the type converter for this property.
        /// </summary>
        public override TypeConverter Converter
        {
            get { return this.converterOverride ?? this.descriptor.Converter; }
        }

        /// <summary>
        /// Gets the description of the member, as specified in the
        /// System.ComponentModel.DescriptionAttribute.
        /// </summary>
        public override string Description
        {
            get
            {
                return this.descriptionOverride ?? this.descriptor.Description;
            }
        }

        /// <summary>
        /// Gets the name that can be displayed in a window, such as a Properties window.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return this.displayNameOverride ?? this.descriptor.DisplayName;
            }
        }

        /// <summary>
        /// Gets an editor of the specified type.
        /// </summary>
        /// <param name="editorBaseType">
        /// The base type of editor, which is used to differentiate between multiple
        /// editors that a property supports.
        /// </param>
        /// <returns>
        /// An instance of the requested editor type, or null if an editor cannot be found.
        /// </returns>
        public override object GetEditor(Type editorBaseType)
        {
            if (editorBaseType != null)
            {
                object editor;
                if (this.editorDictionary.TryGetValue(editorBaseType, out editor))
                {
                    return editor;
                }
            }

            return this.descriptor.GetEditor(editorBaseType);
        }

        /// <summary>
        /// Returns the current value of the property on a component.
        /// </summary>
        /// <param name="component">
        /// The component with the property for which to retrieve the value.
        /// </param>
        /// <returns>The value of a property for a given component.</returns>
        public override object GetValue(object component)
        {
            return this.descriptor.GetValue(component);
        }

        /// <summary>
        /// Gets a value indicating whether this property is read-only.
        /// </summary>
        public override bool IsReadOnly
        {
            get { return this.isReadOnlyOverride ?? this.descriptor.IsReadOnly; }
        }

        /// <summary>
        /// Gets the order in which this property will be retrieved from its type descriptor.
        /// </summary>
        public Nullable<int> PropertyOrder
        {
            get { return this.propertyOrder; }
        }

        /// <summary>
        /// Gets the type of the property.
        /// </summary>
        public override Type PropertyType
        {
            get { return this.descriptor.PropertyType; }
        }

        /// <summary>
        /// Resets the value for this property of the component to the default value.
        /// </summary>
        /// <param name="component">
        /// The component with the property value that is to be reset to the default value.
        /// </param>
        public override void ResetValue(object component)
        {
            this.descriptor.ResetValue(component);
        }

        /// <summary>
        /// Sets the value of the component to a different value.
        /// </summary>
        /// <param name="component">
        /// The component with the property value that is to be set.
        /// </param>
        /// <param name="value">
        /// The new value.
        /// </param>
        public override void SetValue(object component, object value)
        {
            this.descriptor.SetValue(component, value);
        }

        /// <summary>
        /// Determines a value indicating whether the value of this property needs to be
        /// persisted.
        /// </summary>
        /// <param name="component">
        /// The component with the property to be examined for persistence.
        /// </param>
        /// <returns>true if the property should be persisted; otherwise, false.</returns>
        public override bool ShouldSerializeValue(object component)
        {
            return this.descriptor.ShouldSerializeValue(component);
        }

        #region Fluent interface

        /// <summary>
        /// Sets the override for the Category property.
        /// </summary>
        /// <param name="category">The new Category value.</param>
        /// <returns>This DynamicPropertyDescriptor instance.</returns>
        public DynamicPropertyDescriptor SetCategory(string category)
        {
            this.categoryOverride = category;
            return this;
        }

        /// <summary>
        /// Sets the override for the Converter property.
        /// </summary>
        /// <param name="converter">The new Converter value.</param>
        /// <returns>This DynamicPropertyDescriptor instance.</returns>
        public DynamicPropertyDescriptor SetConverter(TypeConverter converter)
        {
            this.converterOverride = converter;
            return this;
        }

        /// <summary>
        /// Sets the override for the Description property.
        /// </summary>
        /// <param name="description">The new Description value.</param>
        /// <returns>This DynamicPropertyDescriptor instance.</returns>
        public DynamicPropertyDescriptor SetDescription(string description)
        {
            this.descriptionOverride = description;
            return this;
        }

        /// <summary>
        /// Sets the override for the DisplayName property.
        /// </summary>
        /// <param name="displayName">The new DisplayName value.</param>
        /// <returns>This DynamicPropertyDescriptor instance.</returns>
        public DynamicPropertyDescriptor SetDisplayName(string displayName)
        {
            this.displayNameOverride = displayName;
            return this;
        }

        /// <summary>
        /// Sets the editor for this type.
        /// </summary>
        /// <param name="editorBaseType">
        /// The base type of editor, which is used to differentiate between multiple editors
        /// that a property supports.
        /// </param>
        /// <param name="editor">
        /// An instance of the requested editor type.
        /// </param>
        /// <returns>This DynamicPropertyDescriptor instance.</returns>
        public DynamicPropertyDescriptor SetEditor(Type editorBaseType, object editor)
        {
            if (editorBaseType != null)
            {
                if (this.editorDictionary.ContainsKey(editorBaseType))
                {
                    if (editor == null)
                    {
                        this.editorDictionary.Remove(editorBaseType);
                    }
                    else
                    {
                        this.editorDictionary[editorBaseType] = editor;
                    }
                }
                else
                {
                    if (editor != null)
                    {
                        this.editorDictionary.Add(editorBaseType, editor);
                    }
                }
            }

            return this;
        }

        /// <summary>
        /// Sets the order in which this property will be retrieved from its type descriptor.
        /// </summary>
        /// <param name="propertyOrder">The order in which this property will be retrieved.</param>
        /// <returns>This DynamicPropertyDescriptor instance.</returns>
        public DynamicPropertyDescriptor SetPropertyOrder(Nullable<int> propertyOrder)
        {
            this.propertyOrder = propertyOrder;
            return this;
        }

        /// <summary>
        /// Sets the override for the IsReadOnly property.
        /// </summary>
        /// <param name="readOnly">The new IsReadOnly value.</param>
        /// <returns>This DynamicPropertyDescriptor instance.</returns>
        public DynamicPropertyDescriptor SetReadOnly(Nullable<bool> readOnly)
        {
            this.isReadOnlyOverride = readOnly;
            return this;
        }

        #endregion
    }
}
