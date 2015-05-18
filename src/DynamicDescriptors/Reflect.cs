namespace DynamicDescriptors
{
    using System;
    using System.Globalization;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Provides various reflection-related methods.
    /// </summary>
    internal static class Reflect
    {
        /// <summary>
        /// Returns the name of the property referred to by the specified property expression.
        /// </summary>
        /// <typeparam name="TSource">
        /// Type containing the property.
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// Type of the property.
        /// </typeparam>
        /// <param name="propertyExpression">
        /// An Expression representing a Func mapping an instance of type TSource to an instance
        /// of type TProperty.
        /// </param>
        /// <returns>
        /// The name of the property referred to by the specified property expression.
        /// </returns>
        public static string GetPropertyName<TSource, TProperty>(
            Expression<Func<TSource, TProperty>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException(
                    "propertyExpression",
                    "propertyExpression should not be null.");

            return GetPropertyInfo<TSource, TProperty>(propertyExpression).Name;
        }

        /// <summary>
        /// Returns a PropertyInfo instance for the property referred to by the specified
        /// property expression.
        /// </summary>
        /// <typeparam name="TSource">
        /// Type containing the property.
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// Type of the property.
        /// </typeparam>
        /// <param name="propertyExpression">
        /// An Expression representing a Func mapping an instance of type TSource to an instance
        /// of type TProperty.
        /// </param>
        /// <returns>
        /// A PropertyInfo instance for the property referred to by the specified
        /// property expression.
        /// </returns>
        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(
            Expression<Func<TSource, TProperty>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException(
                    "propertyExpression",
                    "propertyExpression should not be null.");

            Type sourceType = typeof(TSource);

            MemberExpression member = propertyExpression.Body as MemberExpression;
            if (member == null)
            {
                string message = String.Format(
                    CultureInfo.InvariantCulture,
                    "Expression '{0}' refers to a method, not a property.",
                    propertyExpression.ToString());
                throw new ArgumentException(message);
            }

            PropertyInfo propertyInfo = member.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                string message = String.Format(
                    CultureInfo.InvariantCulture,
                    "Expression '{0}' refers to a field, not a property.",
                    propertyExpression.ToString());
                throw new ArgumentException(message);
            }

            if (sourceType != propertyInfo.ReflectedType
                && !sourceType.IsSubclassOf(propertyInfo.ReflectedType))
            {
                string message = String.Format(
                    CultureInfo.InvariantCulture,
                    "Expresion '{0}' refers to a property that is not from type {1}.",
                    propertyExpression.ToString(),
                    sourceType);
                throw new ArgumentException(message);
            }

            return propertyInfo;
        }
    }
}
