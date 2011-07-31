﻿using System.Text;
using System.Text.RegularExpressions;

public static class StringHelpers
{
    const string ELLIPSIS = "…";

    public static string Trim (this string value, int maxLength)
    {
        if (value.Length <= maxLength)
            return value;

        value = value.Substring (0, maxLength);
        value = value.Insert (value.Length, StringHelpers.ELLIPSIS);

        return value;
    }

    /// <summary>
    /// Trims a string to word boundary.
    /// </summary>
    /// <returns>
    /// The trimmed string
    /// </returns>
    /// <param name='value'>
    /// A string to trim
    /// </param>
    /// <param name='maxLength'>
    /// The maximum length of the returned string.
    /// </param>
    public static string TrimBoundary (this string value, int maxLength)
    {
        if (value.Length <= maxLength)
            return value;

        string pattern = @"^([^\b]{1," + maxLength.ToString () + @"}(?!\w))";
        var match = Regex.Match (value, pattern);
        if (match.Success) {
            return string.Format ("{0}{1}", match.Captures [0], StringHelpers.ELLIPSIS);
        } else {
            return StringHelpers.Trim (value, maxLength);            
        }
    }

    public static string Repeat (string repeatCharacter, int repeatCount)
    {
        StringBuilder sb = new StringBuilder ();

        for (int i = 0; i <= repeatCount; i++)
            sb.Append ("-");

        return sb.ToString ();
    }
 
    /// <summary>
    /// Repeat the specified character n times.
    /// </summary>
    /// <param name='repeat'>
    /// A single character to repeat
    /// </param>
    /// <param name='repeatCount'>
    /// The number of times to repeat the character
    /// </param>
    public static string Repeat (char repeat, int repeatCount)
    {
        // This is more efficient than the Repeat(string, int) method
        return new string (repeat, repeatCount);
    }
}