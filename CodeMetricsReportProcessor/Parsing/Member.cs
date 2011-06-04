using System.Collections.Generic;

namespace CodeMetricsReportProcessor.Parsing
{
    public class Member
    {
        public Dictionary<string, int> Metrics { get; set; }
        public string Name { get; set; }
    }
}