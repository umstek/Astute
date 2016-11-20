using System.Collections.Generic;
using System.Linq;

namespace Astute.Communication
{
    public static class InputConvertors
    {
        public static string TrimHash(string strWithHash)
        {
            return strWithHash.Trim().TrimEnd('#');
        }

        public static IEnumerable<string> SplitByColon(string str)
        {
            return str.Split(':').Select(s => s.Trim()).Where(s => s.Length > 0);
        }

        public static IEnumerable<string> SplitBySemicolon(string str)
        {
            return str.Split(';').Select(s => s.Trim()).Where(s => s.Length > 0);
        }

        public static IEnumerable<string> SplitByComma(string str)
        {
            return str.Split(',').Select(s => s.Trim()).Where(s => s.Length > 0);
        }

        public static string ScreamingSnakeCaseToCamelCase(string snakeCaseString)
        {
            return string.Join(
                "",
                snakeCaseString.Split('_')
                    .Select(word => word.Length < 1 ? "" : word[0] + word.Substring(1).ToLower()));
        }
    }
}