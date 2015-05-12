namespace DynamicDescriptors
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// A dictionary-backed implementation of PropertyDescription.
    /// </summary>
    internal sealed class DictionaryPropertyDescriptor : PropertyDescriptor
    {
        /// <summary>
        /// A dictionary mapping property names to property values.
        /// </summary>
        private readonly IDictionary<string, object> data;

        /// <summary>
        /// The name of the property.
        /// </summary>
        private readonly string propertyName;

        /// <summary>
        /// The type of the property.
        /// </summary>
        private readonly Type propertyType;

        /// <summary>
        /// Initializes a new instance of the DictionaryPropertyDescriptor class.
        /// </summary>
        /// <param name="data">The dictionary that backs this property descriptor.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="propertyType">The type of the property.</param>
        public DictionaryPropertyDescriptor(
            IDictionary<string, object> data,
            string propertyName,
            Type propertyType)
            : base(Preconditions.CheckNotNullOrEmpty(propertyName, "propertyName"), null)
        {
            if (data == null)
            {
                throw new ArgumentNullException(
                    "data",
                    "data should not be null.");
            }

            if (propertyType == null)
            {
                throw new ArgumentNullException(
                    "propertyType",
                    "propertyType should not be null.");
            }

            this.data = data;
            this.propertyName = propertyName;
            this.propertyType = propertyType;
        }

        /// <summary>
        /// Returns a value indicating returns whether resetting an object changes its value.
        /// </summary>
        /// <param name="component">The component to test for reset capability.</param>
        /// <returns>true if resetting the component changes its value; otherwise, false.</returns>
        public override bool CanResetValue(object component)
        {
            return false;
        }

        /// <summary>
        /// Gets the type of the component this property is bound to.
        /// </summary>
        public override Type ComponentType
        {
            get { return null; }
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
            return this.data[this.propertyName];
        }

        /// <summary>
        /// Gets a value indicating whether this property is read-only.
        /// </summary>
        public override bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the type of the property.
        /// </summary>
        public override Type PropertyType
        {
            get { return this.propertyType; }
        }

        /// <summary>
        /// Resets the value for this property of the component to the default value.
        /// </summary>
        /// <param name="component">
        /// The component with the property value that is to be reset to the default value.
        /// </param>
        public override void ResetValue(object component)
        {
            this.data[this.propertyName] = null;
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
            this.data[this.propertyName] = value;
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
            return false;
        }
    }
}
