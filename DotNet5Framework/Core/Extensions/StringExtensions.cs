using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class StringExtensions
    {
        public static int FindLevenshteinDistance(this string source, string target)
        {
            int n = source.Length;
            int m = target.Length;

            int[,] Matrix = new int[n + 1, m + 1];

            if (n == 0)
                return m;

            if (m == 0)
                return n;

            for (int i = 0; i <= n; i++)
                Matrix[i, 0] = i;

            for (int j = 0; j <= m; j++)
                Matrix[0, j] = j;

            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= m; j++)
                {
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;
                    Matrix[i, j] = Math.Min(Math.Min(Matrix[i - 1, j] + 1, Matrix[i, j - 1] + 1), Matrix[i - 1, j - 1] + cost);
                }

            return Matrix[n, m];
        }

        public static int FindLevenshteinDistance(this string source, string target, out int[,] Matrix)
        {
            int n = source.Length;
            int m = target.Length;

            Matrix = new int[n + 1, m + 1];

            if (n == 0)
                return m;

            if (m == 0)
                return n;

            for (int i = 0; i <= n; i++)
                Matrix[i, 0] = i;

            for (int j = 0; j <= m; j++)
                Matrix[0, j] = j;

            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= m; j++)
                {
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;
                    Matrix[i, j] = Math.Min(Math.Min(Matrix[i - 1, j] + 1, Matrix[i, j - 1] + 1), Matrix[i - 1, j - 1] + cost);
                }

            return Matrix[n, m];
        }

        public static List<string> SeparateBySize(this string source, int size)
        {
            var strList = new List<string>();

            for (int i = 0; i < source.Length; i++)
            {
                if (i + size > source.Length)
                    break;
                strList.Add(source.Substring(i, size));
            }

            return strList;
        }

        public static bool EqualsWithLevenshteinDistance(this string source, string wanted, int wantedValue = 0, bool ignoreCaseSensivity = true)
        {
            if (string.IsNullOrEmpty(wanted))
                return false;
            if (ignoreCaseSensivity)
            {
                source = source.ToLower();
                wanted = wanted.ToLower();
            }
            if (source.Length != wanted.Length)
            {
                return false;
            }

            int distance = source.FindLevenshteinDistance(wanted);
            if (wantedValue >= distance)
                return true;

            return false;
        }

        public static bool StartsWithLevenshteinDistance(this string source, string wanted, int wantedValue = 0, bool ignoreCaseSensivity = true)
        {
            if (string.IsNullOrEmpty(wanted))
                return false;
            if (ignoreCaseSensivity)
            {
                source = source.ToLower();
                wanted = wanted.ToLower();
            }
            if (source.Length == wanted.Length)
            {
                int distance = source.FindLevenshteinDistance(wanted);
                if (wantedValue >= distance)
                    return true;
            }

            if (source.Length > wanted.Length)
            {
                var strList = source.SeparateBySize(wanted.Length);
                int distance = strList[0].FindLevenshteinDistance(wanted);
                if (wantedValue >= distance)
                    return true;
            }

            return false;
        }

        public static bool ContainsWithLevenshteinDistance(this string source, string wanted, int wantedValue = 0, bool ignoreCaseSensivity = true)
        {
            if (string.IsNullOrEmpty(wanted))
                return false;
            if (ignoreCaseSensivity)
            {
                source = source.ToLower();
                wanted = wanted.ToLower();
            }
            if (source.Length == wanted.Length)
            {
                int distance = source.FindLevenshteinDistance(wanted);
                if (wantedValue >= distance)
                    return true;
            }

            if (source.Length > wanted.Length)
            {
                var strList = source.SeparateBySize(wanted.Length);
                foreach (var str in strList)
                {
                    int distance = str.FindLevenshteinDistance(wanted);
                    if (wantedValue >= distance)
                        return true;
                }
            }

            return false;
        }

        public static string SplitAndRemove(this string source, string splitValue, int index)
        {
            if (string.IsNullOrEmpty(source))
                return source;

            var strList = source.Split(splitValue).ToList();
            if (strList.Count <= 0)
                return source;

            if (index < strList.Count)
                strList.RemoveAt(index);

            var resp = "";
            for (int i = 0; i < strList.Count; i++)
            {
                resp += strList[i];
                if (i < strList.Count - 1)
                    resp += splitValue;
            }

            return resp;
        }
        public static string ToMask(this string word, char maskBy, int visibleCharCount, char seperator = ' ')
        {
            if (!string.IsNullOrWhiteSpace(word))
            {
                var seperatedValue = word.Split(seperator);

                if (seperatedValue is null || !seperatedValue.Any())
                {
                    return word;
                }

                var maskedStrings = new List<string>();

                foreach (var value in seperatedValue)
                {
                    var visibleChars = value.Substring(0, visibleCharCount);
                    var maskedChars = new string(maskBy, value.Length - visibleCharCount);
                    var maskedString = string.Concat(visibleChars, maskedChars);

                    maskedStrings.Add(maskedString);
                }

                word = string.Join(seperator, maskedStrings);
            }

            return word;
        }
    }
}
