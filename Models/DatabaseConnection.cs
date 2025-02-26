namespace EnumMapperTestProject.Models
{
    public class DatabaseConnection(string name, string connectionString)
    {
        public string Name { get; set; } = name;
        public string ConnectionString { get; set; } = connectionString;
    }
}
