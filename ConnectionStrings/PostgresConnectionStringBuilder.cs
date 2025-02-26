using EnumMapperTestProject.Extensions;

namespace EnumMapperTestProject.ConnectionStrings
{
    public class PostgresConnectionStringBuilder : ConnectionStringBuilder<PostgresConnectionStringBuilder>
    {
        protected override PostgresConnectionStringBuilder ReferenceType => this;

        public override string Build()
        {
            return $"Host={Host};{Port.ReturnFormatIfNotNull("Port={0};")}Username={Username};Password={Password};Database={Database};{ExtraParams}";
        }
    }
}
