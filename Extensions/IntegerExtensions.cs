namespace EnumMapperTestProject.Extensions
{
    public static class IntegerExtensions
    {
        public static string ReturnFormatIfNotNull(this int value, string format)
        {
            return value != 0 ? string.Format(format, value) : string.Empty;
        }
    }
}
