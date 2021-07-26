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
                if (i + size >= source.Length)
                    break;
                strList.Add(source.Substring(i, size));
            }

            return strList;
        }

        public static bool ContainsWithLevenshteinDistance(this string source, string wanted, int wantedValue = 0)
        {
            if (string.IsNullOrEmpty(wanted))
                return false;
            source = source.ToLower();
            wanted = wanted.ToLower();
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

    }
}
