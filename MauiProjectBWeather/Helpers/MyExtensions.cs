using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiProjectBWeather.Helpers
{
    public static class MyExtensions
    {
        public static string FirstCharToUpper(this string s) =>
        string.IsNullOrEmpty(s) ? s : char.ToUpper(s[0]) + s[1..];
    }

}
