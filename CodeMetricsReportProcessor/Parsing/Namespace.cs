using System.Collections.Generic;

namespace CodeMetricsReportProcessor.Parsing
{
    public class Namespace
    {
        public Namespace()
        {
            Types = new List<Type>();
        }

        public List<Type> Types { get; set; }
        public string Name { get; set; }
        public Dictionary<string, int> Metrics { get; set; }
    }
}