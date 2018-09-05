using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMBDatabase.Classes
{
    public static class Extensions
    {
        public static string ToTitleCase(this string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
    }
}