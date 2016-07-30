using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DynamicDescriptors
{
    /// <summary>
    /// A dictionary-backed implementation of <see cref="ICustomTypeDescriptor"/>.
    /// </summary>
    internal sealed class DictionaryTypeDescriptor : CustomTypeDescriptor, ICustomTypeDescriptor, INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// A dictionary mapping property names to property values.
        /// </summary>
        private readonly IDictionary<string, object> _data;

        /// <summary>
        /// A list containing the properties associated with this type descriptor.
        /// </summary>
        private readonly List<DictionaryPropertyDescriptor> _propertyDescriptors;

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryTypeDescriptor"/> class.
        /// </summary>
        /// <param name="data">A dictionary mapping property names to property values.</param>
        public DictionaryTypeDescriptor(IDictionary<string, object> data)
            : this(data, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryTypeDescriptor"/> class.
        /// </summary>
        /// <param name="data">A dictionary mapping property names to property values.</param>
        /// <param name="types">A dictionary mapping property names to property types.</param>
        public DictionaryTypeDescriptor(IDictionary<string, object> data, IDictionary<string, Type> types)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data", "data should not be null.");
            }

            _data = data;
            _propertyDescriptors = new List<DictionaryPropertyDescriptor>();

            foreach (KeyValuePair<string, object> pair in _data)
            {
                Type type;
                if (types == null || !types.TryGetValue(pair.Key, out type))
                {
                    type = typeof(object);
                }

                DictionaryPropertyDescriptor propertyDescriptor = new DictionaryPropertyDescriptor(data, pair.Key, type);
                propertyDescriptor.AddValueChanged(this, (s, e) => OnPropertyChanged(pair.Key));

                _propertyDescriptors.Add(propertyDescriptor);
            }
        }

        /// <summary>
        /// Returns an object that contains the property described by the specified property
        /// descriptor.
        /// </summary>
        /// <param name="pd">
        /// A <see cref="PropertyDescriptor"/> that represents the property whose owner is to be found.
        /// </param>
        /// <returns>An object that represents the owner of the specified property.</returns>
        public override object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        /// <summary>
        /// Returns a collection of property descriptors for the object represented by this type
        /// descriptor.
        /// </summary>
        /// <returns>
        /// A <see cref="PropertyDescriptorCollection"/> containing the property descriptions for the object
        /// represented by this type descriptor.
        /// </returns>
        public override PropertyDescriptorCollection GetProperties()
        {
            return GetProperties(null);
        }

        /// <summary>
        /// Returns a collection of property descriptors for the object represented by this type
        /// descriptor.
        /// </summary>
        /// <param name="attributes">
        /// An array of attributes to use as a filter. This can be null.
        /// </param>
        /// <returns>
        /// A <see cref="PropertyDescriptorCollection"/> containing the property descriptions for the object
        /// represented by this type descriptor.
        /// </returns>
        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return new PropertyDescriptorCollection(_propertyDescriptors.ToArray());
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
