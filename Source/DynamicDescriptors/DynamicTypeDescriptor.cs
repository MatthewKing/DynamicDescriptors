namespace DynamicDescriptors
{
    using System.ComponentModel;

    /// <summary>
    /// A runtime-customizable implementation of ICustomTypeDescriptor.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the object for which dynamic custom type information will be supplied.
    /// </typeparam>
    public sealed class DynamicTypeDescriptor<T> : CustomTypeDescriptor, ICustomTypeDescriptor
    {
        /// <summary>
        /// Initializes a new instance of the DynamicTypeDescriptor class.
        /// </summary>
        /// <param name="parent">The parent custom type descriptor.</param>
        public DynamicTypeDescriptor(ICustomTypeDescriptor parent)
            : base(parent) { }
    }
}
