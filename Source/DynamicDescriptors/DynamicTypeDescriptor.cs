namespace DynamicDescriptors
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// A runtime-customizable implementation of ICustomTypeDescriptor.
    /// </summary>
    public sealed class DynamicTypeDescriptor : CustomTypeDescriptor, ICustomTypeDescriptor
    {
        /// <summary>
        /// A list containing the properties associated with this type descriptor.
        /// </summary>
        private readonly IList<DynamicPropertyDescriptor> dynamicProperties;

        /// <summary>
        /// Comparer to use when sorting a list of dynamic property descriptors.
        /// </summary>
        private readonly IComparer<DynamicPropertyDescriptor> comparer;

        /// <summary>
        /// Initializes a new instance of the DynamicTypeDescriptor class.
        /// </summary>
        /// <param name="parent">The parent custom type descriptor.</param>
        public DynamicTypeDescriptor(ICustomTypeDescriptor parent)
            : base(Preconditions.CheckNotNull(parent, "parent"))
        {
            this.dynamicProperties = new List<DynamicPropertyDescriptor>();
            this.comparer = new DynamicPropertyDescriptorComparer();

            foreach (PropertyDescriptor propertyDescriptor in base.GetProperties())
            {
                this.dynamicProperties.Add(new DynamicPropertyDescriptor(propertyDescriptor));
            }
        }

        /// <summary>
        /// Returns a collection of property descriptors for the object represented by this type
        /// descriptor.
        /// </summary>
        /// <returns>
        /// A PropertyDescriptorCollection containing the property descriptions for the object
        /// represented by this type descriptor.
        /// </returns>
        public override PropertyDescriptorCollection GetProperties()
        {
            return this.GetProperties(null);
        }

        /// <summary>
        /// Returns a collection of property descriptors for the object represented by this type
        /// descriptor.
        /// </summary>
        /// <param name="attributes">
        /// An array of attributes to use as a filter. This can be null.
        /// </param>
        /// <returns>
        /// A PropertyDescriptorCollection containing the property descriptions for the object
        /// represented by this type descriptor.
        /// </returns>
        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            List<DynamicPropertyDescriptor> properties = new List<DynamicPropertyDescriptor>();

            foreach (DynamicPropertyDescriptor property in this.dynamicProperties)
            {
                if (attributes == null || property.Attributes.Contains(attributes))
                {
                    properties.Add(property);
                }
            }

            properties.Sort(this.comparer);

            return new PropertyDescriptorCollection(properties.ToArray());
        }

        /// <summary>
        /// Returns the specified dynamic property descriptor for the object represented by this
        /// type descriptor.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>
        /// The specified dynamic property descriptor for the object represented by this type
        /// descriptor.
        /// </returns>
        public DynamicPropertyDescriptor GetDynamicProperty(string propertyName)
        {
            foreach (DynamicPropertyDescriptor property in this.dynamicProperties)
            {
                if (String.Equals(property.Name, propertyName))
                {
                    return property;
                }
            }

            return null;
        }
    }
}
