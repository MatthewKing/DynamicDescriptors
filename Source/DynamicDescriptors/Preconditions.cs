namespace DynamicDescriptors
{
    using System;

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
                string message = parameterName + " should not be null.";
                throw new ArgumentNullException(parameterName, message);
            }

            return value;
        }
    }
}
