using EnumMapperTestProject.Models;

namespace EnumMapperTestProject.ConnectionStrings
{
    public abstract class ConnectionStringBuilder<T>
        where T : ConnectionStringBuilder<T>
    {
        protected abstract T ReferenceType { get; }

        protected string Host { get; set; }
        protected int Port { get; set; }
        protected string Database { get; set; }
        protected string Username { get; set; }
        protected string Password { get; set; }
        protected string ExtraParams { get; set; }

        public virtual T Set(ConnectionString conn)
        {
            SetHost(conn.Host, conn.Port);
            SetDatabase(conn.Database);
            SetUsername(conn.Username);
            SetPassword(conn.Password);
            SetExtraParams(conn.ExtraParams);

            return ReferenceType;
        }

        public T SetHost(string host, int port = 0)
        {
            Host = host;
            Port = port;
            return ReferenceType;
        }

        public T SetDatabase(string database)
        {
            Database = database;
            return ReferenceType;
        }

        public T SetUsername(string username)
        {
            Username = username;
            return ReferenceType;
        }

        public T SetPassword(string password)
        {
            Password = password;
            return ReferenceType;
        }

        public T SetExtraParams(string extraParams)
        {
            ExtraParams = extraParams;
            return ReferenceType;
        }

        public abstract string Build();
    }
}
