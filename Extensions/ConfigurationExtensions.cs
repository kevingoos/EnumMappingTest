using Microsoft.Extensions.Configuration;

namespace EnumMapperTestProject.Extensions
{
    public static class ConfigurationExtensions
    {
        public static T Get<T>(this IConfiguration config, string key)
        {
            var instance = Activator.CreateInstance<T>();
            config.GetSection(key).Bind(instance);
            return instance;
        }
    }
}
