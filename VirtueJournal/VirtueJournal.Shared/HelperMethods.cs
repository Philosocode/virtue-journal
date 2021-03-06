using System;

namespace VirtueJournal.Shared
{
    public static class HelperMethods
    {
        public static string GetTimeRemaining(DateTimeOffset futureDate)
        {
            var time = futureDate - DateTimeOffset.UtcNow;
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