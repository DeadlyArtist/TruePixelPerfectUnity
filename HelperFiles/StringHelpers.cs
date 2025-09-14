using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringHelpers
{
    public const string PathSeparator = "/";

    public static int ParseInt(this string from)
    {
        return int.Parse(from);
    }

    public static string FromTo(this string str, int start, int end)
    {
        return str[start..end];
    }

    /// <summary>
    /// Excludes separators
    /// </summary>
    public static string FromToMatch(this string str, string start, string end)
    {
        var parts = str.Split(start);
        return parts.Length == 1 ? "" : parts[1].Split(end)[0];
    }

    public static string Join(this string str1, string str2, string separator = "")
    {
        return str1 + separator + str2;
    }

    public static string JoinPath(this string str1, string str2) => Join(str1, str2, PathSeparator);

    public static string Repeat(this string str, int amount = 2)
    {
        for (int i = 1; i < amount; i++)
        {
            str += str;
        }
        return str;
    }
}