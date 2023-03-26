using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Common.Extensions
{
    public static class StringExtensions
    {
        public static string RemoteFix(this string str, params string[]? postFixes)
        {
            if (str == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            if (postFixes is null)
            {
                return str;
            }

            foreach (string text in postFixes)
            {
                if (str.EndsWith(text))
                {
                    return str.Left(str.Length - text.Length);
                }
            }

            return str;
        }


        public static string Left(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(0, len);
        }
    }
}
