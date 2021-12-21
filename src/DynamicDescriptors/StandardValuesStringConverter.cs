﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DynamicDescriptors;

/// <summary>
/// A <see cref="StringConverter"/> that provides a collection of standard values.
/// </summary>
public sealed class StandardValuesStringConverter : StringConverter
{
    /// <summary>
    /// An empty StandardValuesCollection, to be returned when the values factory is not populated.
    /// </summary>
    private static readonly StandardValuesCollection EmptyStandardValuesCollection = new StandardValuesCollection(new string[] { });

    /// <summary>
    /// A factory that supplies the standard values to be supported.
    /// </summary>
    private readonly Func<string[]> _valuesFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="StandardValuesStringConverter"/> class.
    /// </summary>
    /// <param name="values">
    /// An enumerator that iterates through the sequence of standard values to be supported.
    /// </param>
    public StandardValuesStringConverter(IEnumerable<string> values)
    {
        _valuesFactory = values != null
            ? () => values.ToArray()
            : null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StandardValuesStringConverter"/> class.
    /// </summary>
    /// <param name="valuesFactory">A function that supplies the standard values to be supported.</param>
    public StandardValuesStringConverter(Func<string[]> valuesFactory)
    {
        _valuesFactory = valuesFactory;
    }

    /// <summary>
    /// Returns a value indicating whether this object supports a standard set of values that
    /// can be picked from a list, using the specified context.
    /// </summary>
    /// <param name="context">
    /// An <see cref="ITypeDescriptorContext"/> that provides a format context.
    /// </param>
    /// <returns>
    /// true if <see cref="TypeConverter.GetStandardValues()"/> should be called to
    /// find a common set of values the object supports; otherwise, false.
    /// </returns>
    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
        return true;
    }

    /// <summary>
    /// Returns whether the collection of standard values returned from
    /// <see cref="TypeConverter.GetStandardValues()"/> is an exclusive list of
    /// possible values, using the specified context.
    /// </summary>
    /// <param name="context">
    /// An <see cref="ITypeDescriptorContext"/> that provides a format context.
    /// </param>
    /// <returns>
    /// true if the <see cref="TypeConverter.StandardValuesCollection"/> returned from
    /// <see cref="TypeConverter.GetStandardValues()"/> is an exhaustive list of
    /// possible values; false if other values are possible.
    /// </returns>
    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
    {
        return true;
    }

    /// <summary>
    /// Returns a collection of standard string values.
    /// </summary>
    /// <param name="context">
    /// An <see cref="ITypeDescriptorContext"/> that provides a format context that
    /// can be used to extract additional information about the environment from which this
    /// converter is invoked. This parameter or properties of this parameter can be null.
    /// </param>
    /// <returns>
    /// A <see cref="TypeConverter.StandardValuesCollection"/> that holds a standard set of valid values,
    /// or null if the data type does not support a standard set of values.
    /// </returns>
    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
        return _valuesFactory != null
            ? new StandardValuesCollection(_valuesFactory.Invoke())
            : EmptyStandardValuesCollection;
    }
}
