using System.Collections.Generic;
using System.Linq;

namespace CodeMetricsReportProcessor.Parsing
{
    public static class DictionaryExtensions
    {
        public static Dictionary<string ,int> LimitTo(this IDictionary<string, int> source, IEnumerable<string> limit)
        {
            var target = new Dictionary<string, int>();
            foreach(var keyvalue in source)
            {
                if (limit.Contains(keyvalue.Key))
                {
                    target.Add(keyvalue.Key, keyvalue.Value);
                }
            }

            return target;
        }
    }
}