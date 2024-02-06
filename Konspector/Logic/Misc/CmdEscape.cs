//exclude potentially dangerous characters from command line arguments

using System;
using System.Text.RegularExpressions;

namespace Konspector.Misc
{
    public static class CmdEscape
    {
        public static string Escape(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return Regex.Replace(input, @"[^\w\s]", string.Empty);
        }
    }
}