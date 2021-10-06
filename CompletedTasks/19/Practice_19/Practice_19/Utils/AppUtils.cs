using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_19.Utils
{
    public class AppUtils
    {
        public CultureInfo[] AvailableCultures => new CultureInfo[]
        {
            new CultureInfo("en"),
            new CultureInfo("ru"),
            new CultureInfo("fr"),
        };
        public string DefaultCultureName => "ru";
        public string[] ListAvailableLanguages => AvailableCultures.Select(c => c.Name).ToArray();
    }
}
