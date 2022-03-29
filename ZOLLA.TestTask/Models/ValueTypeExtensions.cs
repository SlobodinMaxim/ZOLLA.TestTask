using System;

namespace ZOLLA.TestTask.Models
{
    internal static class ValueTypeExtensions
    {
        public static int Restrict(this int value, int min, int max)
        {
            return Math.Max(min, Math.Min(max, value));
        }
    }
}