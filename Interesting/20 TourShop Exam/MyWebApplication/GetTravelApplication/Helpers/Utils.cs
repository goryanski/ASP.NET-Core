using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetTravelApplication.Helpers
{
    static public class Utils
    {
        public static List<string> UnavailableIps { get; } = new List<string>
        {
            //"::1",
            "801::1",
            "80::1",
            "10::1"
        };
    }
}
