namespace DynamicDescriptors
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Facilitates the creation of custom type descriptors.
    /// </summary>
    public static class DynamicDescriptor
    {
        /// <summary>
        /// Returns a new DynamicTypeDescriptor&lt;T&gt; instance that will supply dynamic
        /// custom type information for the specified object.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the object for which dynamic custom type information will be supplied.
        /// </typeparam>
        /// <param name="instance">
        /// The object for which dynamic custom type information will be supplied.
        /// </param>
        /// <returns>
        /// A new DynamicTypeDescriptor&lt;T&gt; instance that will supply dynamic custom type
        /// information for the specified object.
        /// </returns>
        public static DynamicTypeDescriptor CreateFromInstance<T>(T instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance", "instance should not be null.");
            }

            TypeDescriptionProvider provider = TypeDescriptor.GetProvider(instance);
            ICustomTypeDescriptor descriptor = provider.GetTypeDescriptor(instance);
            return new DynamicTypeDescriptor(descriptor);
        }

        /// <summary>
        /// Returns a new DynamicTypeDescriptor&lt;T&gt; instance that wraps the specified
        /// ICustomTypeDescriptor.
        /// </summary>
        /// <param name="descriptor">
        /// The ICustomTypeDescriptor to wrap.
        /// </param>
        /// <returns>
        /// A new DynamicTypeDescriptor&lt;T&gt; instance that wraps the specified
        /// ICustomTypeDescriptor.
        /// </returns>
        public static DynamicTypeDescriptor CreateFromDescriptor(ICustomTypeDescriptor descriptor)
        {
            if (descriptor == null)
            {
                throw new ArgumentNullException("instance", "instance should not be null.");
            }

            return new DynamicTypeDescriptor(descriptor);
        }
    }
}
