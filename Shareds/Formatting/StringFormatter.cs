using System.Collections.Generic;
using System.Linq;

namespace Shareds.Formatting
{
    public static class StringFormatter
    {
        public static Dictionary<string, string> ToDictionary(this string @string)
        {
            char attrsplit = ';';
            char pairsplit = '=';

            Dictionary<string, string> @dictionary = new Dictionary<string, string>();

            string[] attributes = @string.Split(attrsplit).Where(x => !string.IsNullOrEmpty(x)).ToArray();
            string[] pair = null;
            string key = string.Empty;
            string value = string.Empty;


            foreach (string attribute in attributes)
            {
                pair = attribute.Split(pairsplit);
                key = pair[0];
                value = pair[1];

                @dictionary.Add(key, value);
            }
            return @dictionary;
        }
    }
}
