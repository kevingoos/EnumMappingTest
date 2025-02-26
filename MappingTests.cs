using Dapper;
using EnumMapperTestProject.ConnectionStrings;
using EnumMapperTestProject.Models;
using EnumMapperTestProject.TypeHandlers;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace EnumMapperTestProject
{
    //PostgresDapperMapper.RegisterEnum<TestEnum>();

    //var mapper = new EnumMapper<TestEnum>();
    //var parsed = mapper.Parse("testtt");

    //var param = new NpgsqlParameter();
    //mapper.SetValue(param, TestEnum.test2);

    //var result = _queryExecutor.ExecuteSingle<TestClass>(new EnumTestQuery());

    [TestFixture]
    internal class MappingTests
    {
        [OneTimeSetUp]
        protected virtual void SetUp()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            SqlMapper.AddTypeHandler(typeof(TestEnum), new EnumMapper<TestEnum>());

            LoadPostgresDatabaseFromAppsettings("Default", "DefaultConnection");
        }

        protected void LoadPostgresDatabaseFromAppsettings(string connectionStringName, string settingsName)
        {
            var connectionString = GetConnectionStringFromAppsettings(settingsName);
            PostgresDatabaseConnection.Instance.AddConnectionString(connectionStringName, connectionString);
        }

        private string GetConnectionStringFromAppsettings(string settingsName)
        {
            var config = TestConfiguration.GetConfiguration();
            var connectionString = config.GetConnectionString(settingsName);
            if (connectionString == null)
                throw new Exception("Cannot find a connectionstring in the appsettings.json");
            return connectionString;
        }

        [TestCase(TestEnum.test2)]
        public void MapEnumPgNameTest(TestEnum expected)
        {
            using var connection = CreateConnection();

            try
            {
                connection.Open();
                var result = connection.QuerySingleOrDefault<TestClass>("SELECT \"TestRole\" AS TestRole FROM integration_tests.enum_tests;");

                Assert.That(result, Is.Not.Null);
                Assert.That(result.TestRole, Is.EqualTo(expected));
            }
            finally
            {
                connection.Close();
            }
        }

        protected NpgsqlConnection? CreateConnection()
        {
            return PostgresDatabaseConnection.Instance.Create("AMDB");
        }
    }
}
