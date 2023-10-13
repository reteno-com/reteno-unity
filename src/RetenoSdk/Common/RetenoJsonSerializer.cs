using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Scripts.RetenoSdk.Common
{
    /// <summary>
    /// The reteno json serializer class
    /// </summary>
    public static class RetenoJsonSerializer
    {
        /// <summary>
        /// Serializes the obj
        /// </summary>
        /// <param name="obj">The obj</param>
        /// <returns>The string</returns>
        public static string Serialize(object obj)
        {
            var jsonBuilder = new StringBuilder();
            SerializeObject(obj, jsonBuilder);
            return jsonBuilder.ToString();
        }

        /// <summary>
        /// Serializes the object using the specified obj
        /// </summary>
        /// <param name="obj">The obj</param>
        /// <param name="jsonBuilder">The json builder</param>
        private static void SerializeObject(object obj, StringBuilder jsonBuilder)
        {
            jsonBuilder.Append("{");

            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                var propertyName = GetCamelCasePropertyName(property.Name);
                var propertyValue = property.GetValue(obj);

                if (property.PropertyType.IsEnum)
                {
                    SerializeEnum(propertyName, propertyValue, jsonBuilder);
                }
                else if (IsValueObject(property.PropertyType))
                {
                    SerializeValueObject(propertyName, propertyValue, jsonBuilder);
                }
                else if (IsCollection(property.PropertyType))
                {
                    SerializeCollection(propertyName, propertyValue as IEnumerable, jsonBuilder);
                }
                else
                {
                    SerializeProperty(propertyName, propertyValue, jsonBuilder);
                }

                if (i < properties.Length - 1)
                {
                    jsonBuilder.Append(",");
                }
            }

            jsonBuilder.Append("}");
        }

        /// <summary>
        /// Serializes the property using the specified property name
        /// </summary>
        /// <param name="propertyName">The property name</param>
        /// <param name="propertyValue">The property value</param>
        /// <param name="jsonBuilder">The json builder</param>
        private static void SerializeProperty(string propertyName, object propertyValue, StringBuilder jsonBuilder)
        {
            jsonBuilder.Append($"\"{propertyName}\":");

            if (propertyValue == null)
            {
                jsonBuilder.Append("null");
            }
            else if (propertyValue is string)
            {
                jsonBuilder.Append($"\"{propertyValue}\"");
            }
            else if (propertyValue is bool booleanValue)
            {
                jsonBuilder.Append(booleanValue.ToString().ToLowerInvariant());
            }
            else
            {
                jsonBuilder.Append(propertyValue);
            }
        }

        /// <summary>
        /// Serializes the enum using the specified property name
        /// </summary>
        /// <param name="propertyName">The property name</param>
        /// <param name="propertyValue">The property value</param>
        /// <param name="jsonBuilder">The json builder</param>
        private static void SerializeEnum(string propertyName, object propertyValue, StringBuilder jsonBuilder)
        {
            jsonBuilder.Append($"\"{propertyName}\":\"{propertyValue}\"");
        }

        /// <summary>
        /// Serializes the value object using the specified property name
        /// </summary>
        /// <param name="propertyName">The property name</param>
        /// <param name="propertyValue">The property value</param>
        /// <param name="jsonBuilder">The json builder</param>
        private static void SerializeValueObject(string propertyName, object propertyValue, StringBuilder jsonBuilder)
        {
            if (propertyName != null)
            {
                jsonBuilder.Append($"\"{propertyName}\":");
            }

            jsonBuilder.Append(Serialize(propertyValue));
        }

        /// <summary>
        /// Serializes the collection using the specified property name
        /// </summary>
        /// <param name="propertyName">The property name</param>
        /// <param name="collection">The collection</param>
        /// <param name="jsonBuilder">The json builder</param>
        private static void SerializeCollection(string propertyName, IEnumerable collection, StringBuilder jsonBuilder)
        {
            var isEmptyCollection = true;

            jsonBuilder.Append($"\"{propertyName}\":[");
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    SerializeValue(item, jsonBuilder);
                    jsonBuilder.Append(",");
                    isEmptyCollection = false;
                }
            }

            jsonBuilder.Length--;
            jsonBuilder.Append(isEmptyCollection ? "null" : "]");
        }

        /// <summary>
        /// Serializes the value using the specified value
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="jsonBuilder">The json builder</param>
        private static void SerializeValue(object value, StringBuilder jsonBuilder)
        {
            if (value == null)
            {
                jsonBuilder.Append("null");
            }
            else if (value is string)
            {
                jsonBuilder.Append($"\"{value}\"");
            }
            else if (value is bool booleanValue)
            {
                jsonBuilder.Append(booleanValue.ToString().ToLowerInvariant());
            }
            else if (IsValueObject(value.GetType()))
            {
                SerializeValueObject(null, value, jsonBuilder);
            }
            else if (IsCollection(value.GetType()))
            {
                SerializeCollection(null, value as IEnumerable, jsonBuilder);
            }
            else
            {
                jsonBuilder.Append(value);
            }
        }

        /// <summary>
        /// Gets the camel case property name using the specified property name
        /// </summary>
        /// <param name="propertyName">The property name</param>
        /// <returns>The string</returns>
        private static string GetCamelCasePropertyName(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return propertyName;
            }

            if (char.IsLower(propertyName[0]))
            {
                return propertyName;
            }

            var camelCaseFirstChar = char.ToLowerInvariant(propertyName[0]);
            if (propertyName.Length > 1)
            {
                return camelCaseFirstChar + propertyName.Substring(1);
            }

            return camelCaseFirstChar.ToString();
        }

        /// <summary>
        /// Describes whether is value object
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The bool</returns>
        private static bool IsValueObject(Type type)
        {
            return type.IsClass && !type.IsPrimitive && type.Namespace != null && !type.Namespace.StartsWith("System");
        }

        /// <summary>
        /// Describes whether is collection
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The bool</returns>
        private static bool IsCollection(Type type)
        {
            return type != typeof(string) && type.GetInterfaces().Any(i => i == typeof(IEnumerable));
        }
    }
}