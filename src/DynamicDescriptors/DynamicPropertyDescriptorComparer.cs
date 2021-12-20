using System;
using System.Collections.Generic;

namespace DynamicDescriptors;

/// <summary>
/// An implementation of <see cref="IComparer{T}"/> for <see cref="DynamicPropertyDescriptorComparer"/> instances.
/// </summary>
internal sealed class DynamicPropertyDescriptorComparer : IComparer<DynamicPropertyDescriptor>
{
    /// <summary>
    /// The <see cref="StringComparer"/> to use to compare property names when unable to use property order.
    /// </summary>
    private static readonly StringComparer sc = StringComparer.OrdinalIgnoreCase;

    /// <summary>
    /// Compares two <see cref="DynamicPropertyDescriptor"/> instances and returns a value indicating
    /// whether one is less than, equal to, or greater than the other.
    /// </summary>
    /// <param name="x">The first <see cref="DynamicPropertyDescriptor"/> instance to compare.</param>
    /// <param name="y">The second <see cref="DynamicPropertyDescriptor"/> instance to compare.</param>
    /// <returns>
    /// A value that is less than zero if x is less than y, zero if x equals y,
    /// or greater than zero if x is greater than y.
    /// </returns>
    public int Compare(DynamicPropertyDescriptor x, DynamicPropertyDescriptor y)
    {
        if (x == null)
        {
            if (y == null)
            {
                // two nulls shall be considered equal.
                return 0;
            }
            else
            {
                // y comes first by virtue of it not being null.
                return 1;
            }
        }
        else
        {
            if (y == null)
            {
                // x comes first by virtue of it not being null.
                return -1;
            }
            else
            {
                if (x.PropertyOrder.HasValue)
                {
                    if (y.PropertyOrder.HasValue)
                    {
                        if (x.PropertyOrder.Value > y.PropertyOrder.Value)
                        {
                            // y comes first by virtue of it having a lower property order.
                            return 1;
                        }
                        else if (y.PropertyOrder.Value > x.PropertyOrder.Value)
                        {
                            // x comes first by virtue of it having a lower property order.
                            return -1;
                        }
                        else
                        {
                            // property orders are the same, so use alphabetical order.
                            return sc.Compare(x.Name, y.Name);
                        }
                    }
                    else
                    {
                        // x comes first by virtue of it having a property order.
                        return -1;
                    }
                }
                else
                {
                    if (y.PropertyOrder.HasValue)
                    {
                        // y comes first by virtue of it having a property order.
                        return 1;
                    }
                    else
                    {
                        // neither have a property order, so use alphabetical order.
                        return sc.Compare(x.Name, y.Name);
                    }
                }
            }
        }
    }
}
