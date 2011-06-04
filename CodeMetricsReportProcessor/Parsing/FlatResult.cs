using System.Collections.Generic;

namespace CodeMetricsReportProcessor.Parsing
{
    public class FlatResult
    {
        public string Target { get; set; }
        public string Module { get; set; }
        public string Namespace { get; set; }
        public string Type { get; set; }
        public string Member { get; set; }
        public Dictionary<string, int> Metrics { get; set; }
    }
}