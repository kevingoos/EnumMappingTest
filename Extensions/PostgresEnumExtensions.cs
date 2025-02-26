using System.Reflection;
using NpgsqlTypes;

namespace EnumMapperTestProject.Extensions
{
    public static class PostgresEnumExtensions
    {
        public static string? GetPgName<T>(this T enumerationValue)
            where T : Enum
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            return GetPgNameAttribute(enumerationValue);
        }

        public static object? GetEnumFromPgName(this Type enumType, string pgName)
        {
            foreach (var field in enumType.GetFields())
            {
                var attribute = field.GetCustomAttribute<PgNameAttribute>();
                if (attribute != null && attribute.PgName.Equals(pgName, StringComparison.OrdinalIgnoreCase))
                {
                    return field.GetValue(null);
                }
            }

            throw new ArgumentException($"No matching enum value found for pgname: {pgName}");
        }

        public static object? GetEnumFromPgName<T>(string pgName)
            where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                var attribute = field.GetCustomAttribute<PgNameAttribute>();
                if (attribute != null && attribute.PgName.Equals(pgName, StringComparison.OrdinalIgnoreCase))
                {
                    return field.GetValue(null);
                }
            }

            throw new ArgumentException($"No matching enum value found for pgname: {pgName}");
        }

        public static string? GetPgNameAttribute(this Enum enumerationValue)
        {
            var memberInfo = enumerationValue.GetType().GetMember(enumerationValue.ToString());
            if (memberInfo.Length == 0) return null;

            var attribute = memberInfo[0].GetCustomAttribute<PgNameAttribute>();
            return attribute?.PgName;
        }
    }
}
