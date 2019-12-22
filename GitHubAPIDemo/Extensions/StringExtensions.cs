using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubAPIDemo.Extensions
{
    public static class StringExtensions
    {

        public static bool IsEmptyString(this string text)
        {
            return string.IsNullOrEmpty(text);
        }

        public static bool IsNotEmptyString(this string text)
        {
            return !IsEmptyString(text);
        }

    }
}
