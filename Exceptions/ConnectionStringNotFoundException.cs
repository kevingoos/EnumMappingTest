namespace EnumMapperTestProject.Exceptions
{
    public class ConnectionStringNotFoundException(string connStringName) : Exception($"Cannot find the connectionstring with the name {connStringName}. Please add the connectionstring to the DatabaseConnection class!");
}
