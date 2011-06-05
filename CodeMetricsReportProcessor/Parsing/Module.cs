using System;
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

        public IEnumerable<String> AllPossibleMetrics()
        {
            var distinctMetrics = new HashSet<string>();

            AddMetrics(distinctMetrics, Metrics);
            foreach (var @namespace in Namespaces)
            {
                AddMetrics(distinctMetrics, @namespace.Metrics);
                foreach (var type in @namespace.Types)
                {
                    AddMetrics(distinctMetrics, type.Metrics);
                    foreach (var member in type.Members)
                    {
                        AddMetrics(distinctMetrics, member.Metrics);
                    }
                }    
            }

            return distinctMetrics;
        }

        public IEnumerable<FlatModuleScopedResult> Flatten()
        {
            var results = new List<FlatModuleScopedResult>();

            foreach (var @namespace in Namespaces)
            {
                results.Add(new FlatModuleScopedResult{Namespace = @namespace.Name, Metrics = @namespace.Metrics});
                foreach (var type in @namespace.Types)
                {
                    results.Add(new FlatModuleScopedResult {Namespace = @namespace.Name, Type = type.Name, Metrics = type.Metrics });
                    foreach (var member in type.Members)
                    {
                        results.Add(new FlatModuleScopedResult {Namespace = @namespace.Name, Type = type.Name, Member = member.Name, Metrics = member.Metrics });
                    }
                }
            }

            return results;
        }

        private void AddMetrics(HashSet<string> distinctMetrics, Dictionary<string, int> metrics)
        {
            foreach (var metric in metrics)
            {
                distinctMetrics.Add(metric.Key);
            }
        }
    }
}