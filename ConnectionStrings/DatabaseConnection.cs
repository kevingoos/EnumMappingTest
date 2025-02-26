using EnumMapperTestProject.Exceptions;
using EnumMapperTestProject.Models;

namespace EnumMapperTestProject.ConnectionStrings
{
    public abstract class DatabaseConnection<T>
        where T : DatabaseConnection<T>, new()
    {
        private static T? _connection;
        public static T Instance => _connection ??= new T();
        protected readonly Dictionary<string, string> ConnectionStringDictionary = new();

        #region Manage connectionstrings

        public void AddConnectionString(string name, string? connectionString)
        {
            if (connectionString == null) return;
            if (ConnectionStringExists(name))
            {
                ConnectionStringDictionary[name] = connectionString;
            }
            else
            {
                ConnectionStringDictionary.Add(name, connectionString);
            }
        }

        public void AddConnectionString(DatabaseConnection connection)
        {
            AddConnectionString(connection.Name, connection.ConnectionString);
        }

        public bool ConnectionStringExists(string name)
        {
            return ConnectionStringDictionary.ContainsKey(name);
        }

        public string GetConnectionString(string name)
        {
            if (ConnectionStringDictionary.TryGetValue(name, out var connectionString))
            {
                return connectionString;
            }

            throw new ConnectionStringNotFoundException($"Cannot find a connectionstring with name {name}");
        }

        public DatabaseConnection GetDbConnection(string name)
        {
            if (ConnectionStringDictionary.TryGetValue(name, out var connectionString))
            {
                return new DatabaseConnection(name, connectionString);
            }

            throw new ConnectionStringNotFoundException($"Cannot find a connectionstring with name {name}");
        }

        public void RemoveConnectionString(string name)
        {
            ConnectionStringDictionary.Remove(name);
        }

        #endregion
    }
}
