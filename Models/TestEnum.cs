using NpgsqlTypes;

namespace EnumMapperTestProject.Models
{
    public enum TestEnum
    {
        [PgName("test")]
        test,
        [PgName("test 1")]
        test1,
        [PgName("testtt")]
        test2
    }
}
