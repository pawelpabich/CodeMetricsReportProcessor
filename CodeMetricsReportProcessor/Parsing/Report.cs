using System.Collections.Generic;

namespace CodeMetricsReportProcessor.Parsing
{
    public class Report
    {
        public Report()
        {
            Targets = new List<Target>();
        }

        public IList<Target> Targets { get; set; }
    }
}