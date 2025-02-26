using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Dapper;
using static Dapper.SqlMapper;

namespace EnumMapperTestProject.TypeHandlers
{
    public class DapperMapper
    {
        public static void ConfigureMapping<TE>()
        {
            //Add mapping for the given type
            var type = typeof(TE);
            var map = new ConcurrentDictionary<string, string>();

            foreach (var prop in type.GetProperties())
            {
                var attr = prop.GetCustomAttribute<ColumnAttribute>();
                if (attr != null)
                {
                    map[prop.Name] = attr.Name;
                }
            }

            SetTypeMap(type, new CustomPropertyTypeMap(type, (t, columnName) => t.GetProperties().FirstOrDefault(prop =>
                    map.TryGetValue(prop.Name, out var mappedName) && mappedName == columnName || prop.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase)) ?? throw new InvalidOperationException()
            ));
        }

        public static void AddTypeHandlerIfNotExists(Type type, ITypeHandler handler)
        {
            if (!HasTypeHandler(type))
            {
                AddTypeHandler(type, handler);
            }
        }
    }
}
