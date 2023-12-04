using System;
using System.Collections.Generic;

namespace Runtime.Infrastructure.Utility
{
    public static class EnumExtensions
    {
        public static IEnumerable<T> GetAllValues<T>() where T : Enum
        {
            var allValues = Enum.GetValues(typeof(T));
            int count = allValues.Length;
            for (var i = 0; i < count; i++)
                yield return (T) allValues.GetValue(i);
        }
    }
}