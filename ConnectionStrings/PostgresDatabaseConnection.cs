using EnumMapperTestProject.Exceptions;
using EnumMapperTestProject.Models;
using Npgsql;

namespace EnumMapperTestProject.ConnectionStrings
{
    public class PostgresDatabaseConnection : DatabaseConnection<PostgresDatabaseConnection>
    {
        public NpgsqlConnection Create(string connStringName = "Default")
        {
            if (!ConnectionStringDictionary.TryGetValue(connStringName, out var connectionString))
            {
                throw new ConnectionStringNotFoundException(connStringName);
            }
            return CreateWithConnectionString(connectionString);
        }

        public void AddConnectionString(string name, ConnectionString connection)
        {
            var connectionString = new PostgresConnectionStringBuilder().Set(connection).Build();
            AddConnectionString(name, connectionString);
        }

        public NpgsqlConnection CreateWithConnectionString(string connectionString)
        {
            return new NpgsqlConnection(connectionString);
        }
    }
}