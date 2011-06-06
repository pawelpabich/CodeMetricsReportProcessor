using System.Collections.Generic;

namespace CodeMetricsReportProcessor.Parsing
{
    public class Member
    {
        public static IEnumerable<string> RelevantMetrics = new[] { "MaintainabilityIndex", "CyclomaticComplexity", "LinesOfCode" };

        public Dictionary<string, int> Metrics { get; set; }
        public string Name { get; set; }
    }
}