namespace DynamicDescriptors
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// A dictionary-backed implementation of ICustomTypeDescriptor.
    /// </summary>
    internal sealed class DictionaryTypeDescriptor : CustomTypeDescriptor, ICustomTypeDescriptor
    {
        /// <summary>
        /// A dictionary mapping property names to property values.
        /// </summary>
        private readonly IDictionary<string, object> data;

        /// <summary>
        /// A list containing the properties associated with this type descriptor.
        /// </summary>
        private readonly List<DictionaryPropertyDescriptor> propertyDescriptors;

        /// <summary>
        /// Initializes a new instance of the DictionaryTypeDescriptor class.
        /// </summary>
        /// <param name="data">A dictionary mapping property names to property values.</param>
        public DictionaryTypeDescriptor(IDictionary<string, object> data)
            : this(data, null) { }

        /// <summary>
        /// Initializes a new instance of the DictionaryTypeDescriptor class.
        /// </summary>
        /// <param name="data">A dictionary mapping property names to property values.</param>
        /// <param name="types">A dictionary mapping property names to property types.</param>
        public DictionaryTypeDescriptor(
            IDictionary<string, object> data,
            IDictionary<string, Type> types)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data", "data should not be null.");
            }

            this.data = data;
            this.propertyDescriptors = new List<DictionaryPropertyDescriptor>();

            foreach (KeyValuePair<string, object> pair in this.data)
            {
                Type type;
                if (types == null || !types.TryGetValue(pair.Key, out type))
                {
                    type = typeof(object);
                }

                DictionaryPropertyDescriptor propertyDescriptor =
                    new DictionaryPropertyDescriptor(data, pair.Key, type);

                this.propertyDescriptors.Add(propertyDescriptor);
            }
        }

        /// <summary>
        /// Returns an object that contains the property described by the specified property
        /// descriptor.
        /// </summary>
        /// <param name="pd">
        /// A PropertyDescriptor that represents the property whose owner is to be found.
        /// </param>
        /// <returns>An object that represents the owner of the specified property.</returns>
        public override object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this.data;
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
            return new PropertyDescriptorCollection(this.propertyDescriptors.ToArray());
        }
    }
}
