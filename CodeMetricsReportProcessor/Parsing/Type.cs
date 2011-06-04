using System.Collections.Generic;

namespace CodeMetricsReportProcessor.Parsing
{
    public class Type
    {
        public Type()
        {
            Members = new List<Member>();
        }

        public List<Member> Members { get; set; }
        public string Name { get; set; }
        public Dictionary<string, int> Metrics { get; set; }
    }
}