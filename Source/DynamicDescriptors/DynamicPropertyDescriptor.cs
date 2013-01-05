namespace DynamicDescriptors
{
    using System;
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
        /// If this value is not null, it will be returned by the Category property;
        /// otherwise, the base descriptor's Category property will be returned.
        /// </summary>
        private string categoryOverride;

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
        }

        /// <summary>
        /// Returns a value indicating returns whether resetting an object changes its value.
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
            get { return this.descriptor.IsReadOnly; }
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
        /// Sets the order in which this property will be retrieved from its type descriptor.
        /// </summary>
        /// <param name="propertyOrder">The order in which this property will be retrieved.</param>
        /// <returns>This DynamicPropertyDescriptor instance.</returns>
        public DynamicPropertyDescriptor SetPropertyOrder(Nullable<int> propertyOrder)
        {
            this.propertyOrder = propertyOrder;
            return this;
        }

        #endregion
    }
}
