namespace EnumMapperTestProject.Models
{
    public class ConnectionString
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ExtraParams { get; set; }

        public ConnectionString()
        {
            
        }

        public ConnectionString(string host, int port, string database, string username = "", string password = "", string extraParams = "")
        {
            Host = host;
            Port = port;
            Database = database;
            Username = username;
            Password = password;
            ExtraParams = extraParams;
        }

        public ConnectionString(string host, string database, string username = "", string password = "", string extraParams = "") : this(host, 0, database, username, password, extraParams)
        {
        }
    }
}
