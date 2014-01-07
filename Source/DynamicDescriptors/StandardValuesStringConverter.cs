﻿namespace DynamicDescriptors
{
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// A System.ComponentModel.StringConverter that provides a collection of standard values.
    /// </summary>
    public sealed class StandardValuesStringConverter : StringConverter
    {
        /// <summary>
        /// The standard values to be supported.
        /// </summary>
        private readonly List<string> values;

        /// <summary>
        /// Initializes a new instance of the StandardValuesStringConverter class.
        /// </summary>
        /// <param name="values">
        /// An enumerator that iterates through the sequence of standard values to be supported.
        /// </param>
        public StandardValuesStringConverter(IEnumerable<string> values)
        {
            this.values = values != null
                ? new List<string>(values)
                : new List<string>();
        }

        /// <summary>
        /// Returns a value indicating whether this object supports a standard set of values that
        /// can be picked from a list, using the specified context.
        /// </summary>
        /// <param name="context">
        /// An System.ComponentModel.ITypeDescriptorContext that provides a format context.
        /// </param>
        /// <returns>
        /// true if System.ComponentModel.TypeConverter.GetStandardValues() should be called to
        /// find a common set of values the object supports; otherwise, false.
        /// </returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Returns whether the collection of standard values returned from
        /// System.ComponentModel.TypeConverter.GetStandardValues() is an exclusive list of
        /// possible values, using the specified context.
        /// </summary>
        /// <param name="context">
        /// An System.ComponentModel.ITypeDescriptorContext that provides a format context.
        /// </param>
        /// <returns>
        /// true if the System.ComponentModel.TypeConverter.StandardValuesCollection returned from
        /// System.ComponentModel.TypeConverter.GetStandardValues() is an exhaustive list of
        /// possible values; false if other values are possible.
        /// </returns>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Returns a collection of standard string values values.
        /// </summary>
        /// <param name="context">
        /// An System.ComponentModel.ITypeDescriptorContext that provides a format context that
        /// can be used to extract additional information about the environment from which this
        /// converter is invoked. This parameter or properties of this parameter can be null.
        /// </param>
        /// <returns>
        /// A System.ComponentModel.TypeConverter.StandardValuesCollection that holds a standard
        /// set of valid values, or null if the data type does not support a standard set of
        /// values.
        /// </returns>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(this.values);
        }
    }
}
