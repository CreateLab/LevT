using System.Collections.Generic;

namespace ConsoleApp2
{
    public static class ExtentionClass
    {
        public static IEnumerable<T> ToOneEnum<T>(this T data)
        {
            return new[] {data};
        }
    }
}