using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace DvlSql
{
    public static class SystemExtensions
    {
        public static StringBuilder TrimEnd(this StringBuilder sb, bool leftOneWhiteSpace = false)
        {
            if (sb == null || sb.Length == 0) 
                return sb;

            int i = sb.Length - 1;
            for (; i >= 0; i--)
                if (!char.IsWhiteSpace(sb[i]))
                    break;

            if (i >= sb.Length - 1 || (leftOneWhiteSpace && i == sb.Length - 2)) 
                return sb;

            if (leftOneWhiteSpace)
                sb.Remove(i + 2, sb.Length - i);
            else sb.Remove(i + 1, sb.Length - i - 1);

            return sb;
        }

        public static string WithAlpha(this string str) =>
            !string.IsNullOrEmpty(str) && str.Length != 0 && str[0] != '@' ? $"@{str}" : str;

        public static string WithAliasBrackets(this string str) =>
            !string.IsNullOrEmpty(str) && str.Length != 0 && str[0] == '[' && str[^1] == ']' ? str : $"[{str}]";

        public static StringBuilder TrimIfLastCharacterIs(this StringBuilder sb, char character)
        {
            if (sb == null || sb.Length == 0)
                return sb;
            
            int i = sb.Length - 1;
            while (i > 0 && char.IsWhiteSpace(sb[i]))
                i--;

            if (i == 0 || sb[i] != character)
                return sb;

            return sb.TrimEnd();
        }
        
        public static string GetStringAfter(this string str, string what) => str.LastIndexOf(what) switch
        {
            var index when index != -1 => str[(index + 1)..],
            _ => str
        };

        public static string GetEscapedString(this string str, bool includeEdges = true) => includeEdges 
            ? str.Replace("'","''") 
            : str.Replace("'","''", 1, str.Length - 1);

        public static string Replace(this string str, string oldValue, string newValue, int fromIndex, int toIndex)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (i > fromIndex && i + oldValue.Length < toIndex &&
                    str.Substring(i, oldValue.Length) is { } s &&
                    s == oldValue)
                {
                    builder.Append(newValue);
                    i += oldValue.Length - 1;
                }
                else builder.Append(str[i]);
            }

            return builder.ToString();
        }

        public static string RemoveUnnecessaryNewlines(this string str) =>
            string.Join(Environment.NewLine, str.Split(Environment.NewLine).Where(s => !string.IsNullOrEmpty(s) && s != Environment.NewLine));

        public static IEnumerable<ITuple> ToTuples<T>(this IEnumerable<T> source) =>
            source.Select(s => s as ITuple).Where(s => s != null).ToArray();
    }
}
