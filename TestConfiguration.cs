
using EnumMapperTestProject.Extensions;
using Microsoft.Extensions.Configuration;

namespace EnumMapperTestProject
{
    public static class TestConfiguration
    {
        public static class Category
        {
            public const string UnitTestsCategory = "UnitTests";
            public const string IntegrationTestsCategory = "IntegrationTests";
            public const string EndToEndTestsCategory = "EndToEndTests";
            public const string IgnoreBuildServerCategory = "IgnoreBuildServer";
        }

        public static IConfigurationRoot GetConfiguration()
        {            
            return new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json", true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static T Get<T>(string key)
        {
            var config = GetConfiguration();
            return config.Get<T>(key);
        }
    }
}
