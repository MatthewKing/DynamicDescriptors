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
        /// Gets the type of the component this property is bound to.
        /// </summary>
        public override Type ComponentType
        {
            get { return this.descriptor.ComponentType; }
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
    }
}
