using System.Collections.Generic;

namespace CodeMetricsReportProcessor.Parsing
{
    public class Module
    {
        public Module()
        {
            Namespaces = new List<Namespace>();
        }

        public string Name { get; set; }
        public Dictionary<string, int> Metrics { get; set; }
        public IList<Namespace> Namespaces { get; set; }
    }
}