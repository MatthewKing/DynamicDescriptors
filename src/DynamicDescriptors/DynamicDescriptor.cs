using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DynamicDescriptors;

/// <summary>
/// Facilitates the creation of custom type descriptors.
/// </summary>
public static class DynamicDescriptor
{
    /// <summary>
    /// Returns a new <see cref="DynamicTypeDescriptor"/> instance that will supply
    /// dynamic custom type information for the specified object.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the object for which dynamic custom type information will be supplied.
    /// </typeparam>
    /// <param name="instance">
    /// The object for which dynamic custom type information will be supplied.
    /// </param>
    /// <returns>
    /// A new <see cref="DynamicTypeDescriptor"/> instance that will supply dynamic
    /// custom type information for the specified object.
    /// </returns>
    public static DynamicTypeDescriptor CreateFromInstance<T>(T instance)
    {
        if (instance == null)
        {
            throw new ArgumentNullException(nameof(instance));
        }

        var provider = TypeDescriptor.GetProvider(instance);
        var descriptor = provider.GetTypeDescriptor(instance);
        return new DynamicTypeDescriptor(descriptor);
    }

    /// <summary>
    /// Returns a new <see cref="DynamicTypeDescriptor"/> instance that wraps the specified
    /// <see cref="ICustomTypeDescriptor"/> instance.
    /// </summary>
    /// <param name="descriptor">
    /// The <see cref="ICustomTypeDescriptor"/> instance to wrap.
    /// </param>
    /// <returns>
    /// A new <see cref="DynamicTypeDescriptor"/> instance that wraps the specified
    /// <see cref="ICustomTypeDescriptor"/> instance.
    /// </returns>
    public static DynamicTypeDescriptor CreateFromDescriptor(ICustomTypeDescriptor descriptor)
    {
        if (descriptor == null)
        {
            throw new ArgumentNullException(nameof(descriptor));
        }

        return new DynamicTypeDescriptor(descriptor);
    }

    /// <summary>
    /// Returns a new <see cref="DynamicTypeDescriptor"/> instance that exposes properties
    /// defined by the data present in the specified dictionary.
    /// </summary>
    /// <param name="data">A dictionary mapping property names to property values.</param>
    /// <returns>
    /// A new <see cref="DynamicTypeDescriptor"/> instance that exposes properties defined by
    /// the data present in the specified dictionary.
    /// </returns>
    public static DynamicTypeDescriptor CreateFromDictionary(IDictionary<string, object> data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        var descriptor = new DictionaryTypeDescriptor(data);

        return new DynamicTypeDescriptor(descriptor);
    }

    /// <summary>
    /// Returns a new <see cref="DynamicTypeDescriptor"/> instance that exposes properties
    /// defined by the data present in the specified dictionary.
    /// </summary>
    /// <param name="data">A dictionary mapping property names to property values.</param>
    /// <param name="types">A dictionary mapping property names to property types.</param>
    /// <returns>
    /// A new <see cref="DynamicTypeDescriptor"/> instance that exposes properties defined
    /// by the data present in the specified dictionary.
    /// </returns>
    public static DynamicTypeDescriptor CreateFromDictionary(IDictionary<string, object> data, IDictionary<string, Type> types)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        var descriptor = new DictionaryTypeDescriptor(data, types);

        return new DynamicTypeDescriptor(descriptor);
    }
}
