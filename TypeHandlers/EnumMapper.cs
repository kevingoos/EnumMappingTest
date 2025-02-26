using System.Data;
using Dapper;
using EnumMapperTestProject.Extensions;

namespace EnumMapperTestProject.TypeHandlers
{
    public class EnumMapper<T> : SqlMapper.TypeHandler<T>
        where T : Enum
    {
        public override void SetValue(IDbDataParameter parameter, T? value)
        {
            Console.WriteLine($"EnumMapper<T>.SetValue called for {typeof(T).Name}: {value}");
            parameter.Value = value.GetPgName() ?? value.GetDescription();
        }

        public override T Parse(object value)
        {
            Console.WriteLine($"EnumMapper<T>.Parse called for {typeof(T).Name}: {value}");
            if (value is string pgName)
            {
                return (T)(typeof(T).GetEnumFromPgName(pgName) ?? typeof(T).GetEnumFromDescription(pgName))!;
            }

            throw new ArgumentException($"Cannot convert '{value}' to enum type {typeof(T)}.");
        }
    }
}
