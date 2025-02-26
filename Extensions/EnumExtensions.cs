using System.Collections.Concurrent;
using System.Reflection;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace EnumMapperTestProject.Extensions
{
    public static class EnumExtensions
    {
        private static readonly ConcurrentDictionary<Type, Dictionary<string, Enum>> EnumCache = new();

        private static Dictionary<string, Enum> InitEnumCache(Type enumType)
        {
            var cache = new Dictionary<string, Enum>(StringComparer.OrdinalIgnoreCase);

            foreach (var enumValue in Enum.GetValues(enumType).Cast<Enum>())
            {
                var description = enumValue.GetDescriptionAttribute();
                var name = enumValue.ToString();

                // Add description to cache (if not already present)
                cache.TryAdd(description, enumValue);

                // Add name to cache (if not already present)
                cache.TryAdd(name, enumValue);
            }

            return cache;
        }

        public static IEnumerable<T> GetEnumValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        #region To description from enum value

        /// <summary>
        /// Gets the description attribute of an enum value. Falls back to the enum name if no description is found.
        /// Uses caching to avoid repeated reflection.
        /// </summary>
        public static string GetDescription<T>(this T enumerationValue) 
            where T : Enum
        {
            var type = enumerationValue.GetType();
            var cache = EnumCache.GetOrAdd(type, _ => InitEnumCache(type));

            // Try to find the description in cache (preferably by the actual value)
            return cache.TryGetValue(enumerationValue.ToString(), out var value) ? value.GetDescriptionAttribute() : enumerationValue.ToString();
        }

        #endregion

        #region From description to enum value

        /// <summary>
        /// Retrieves an enum value from its description (or name).
        /// </summary>
        public static T? GetEnumFromDescription<T>(this string? description) where T : struct, Enum
        {
            var type = typeof(T);
            var typeCache = EnumCache.GetOrAdd(type, _ => InitEnumCache(type));

            return typeCache.TryGetValue(description, out var enumValue) ? (T?)enumValue : null;
        }

        /// <summary>
        /// Retrieves an enum value from its description (or name) for a given enum type.
        /// </summary>
        public static object? GetEnumFromDescription(this Type enumType, string? description)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("Provided type must be an enum.", nameof(enumType));
            }

            var cache = EnumCache.GetOrAdd(enumType, _ => InitEnumCache(enumType));
            return cache.GetValueOrDefault(description);
        }

        #endregion

        /// <summary>
        /// Retrieves the DescriptionAttribute value of an enum value, or falls back to its name.
        /// </summary>
        public static string GetDescriptionAttribute(this Enum enumerationValue)
        {
            var memberInfo = enumerationValue.GetType().GetMember(enumerationValue.ToString());
            if (memberInfo.Length == 0) return enumerationValue.ToString();

            var attribute = memberInfo[0].GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? enumerationValue.ToString();
        }
    }
}
