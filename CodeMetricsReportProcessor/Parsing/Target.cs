using System.Collections.Generic;

namespace CodeMetricsReportProcessor.Parsing
{
    public class Target
    {
        public Target()
        {
            Modules = new List<Module>();
        }

        public string Name { get; set; }
        public Dictionary<string, int> Metrics { get; set; }
        public IList<Module> Modules { get; set; }
    }
}