using System;

namespace DynamicDescriptors;

/// <summary>
/// Provides internal precondition helper methods.
/// </summary>
internal static class Preconditions
{
    /// <summary>
    /// Provides 'is not null' parameter validation.
    /// </summary>
    /// <typeparam name="T">The type of the parameter to validate.</typeparam>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <returns>The value of the parameter (if it was not null).</returns>
    public static T CheckNotNull<T>(T value, string parameterName)
        where T : class
    {
        if (value == null)
        {
            throw new ArgumentNullException(parameterName);
        }

        return value;
    }

    /// <summary>
    /// Provides 'is not null or an empty string' parameter validation.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <returns>The value of the parameter (if it was not null or empty).</returns>
    public static string CheckNotNullOrEmpty(string value, string parameterName)
    {
        if (value == null)
        {
            throw new ArgumentNullException(parameterName);
        }

        if (value.Length == 0)
        {
            throw new ArgumentException($"{parameterName} should be an empty string.", parameterName);
        }

        return value;
    }
}
