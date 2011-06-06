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

        public IEnumerable<string> AllPossibleMetrics()
        {
            var distinctMetrics = new HashSet<string>();

            foreach (var target in Targets)
            {
                foreach (var module in target.Modules)
                {
                    foreach (var metric in module.AllPossibleMetrics())
                    {
                        distinctMetrics.Add(metric);
                    }
                }
            }

            return distinctMetrics;
        }

    }   
}