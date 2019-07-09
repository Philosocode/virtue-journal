using System;

namespace VirtueApi.Enums
{
    public static class Helpers
    {
        public static string GetTimeRemaining(DateTime warrantyDate)
        {
            var time = warrantyDate - DateTime.Now;
            string output = String.Empty;

            if (time.Days > 0)
                output += time.Days + "d";

            if ((time.Days == 0 || time.Days == 1) && time.Hours > 0)
                output += time.Hours + "h";

            if (time.Days == 0 && time.Minutes > 0)
                output += time.Minutes + "min";

            if (output.Length == 0)
                output += time.Seconds + "s";

            return output;
        }
    }
}