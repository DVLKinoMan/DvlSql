using System.Text;

namespace Dvl_Sql.Helpers
{
    internal static class SystemExtensions
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
    }
}
