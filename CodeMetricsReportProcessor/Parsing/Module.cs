using System;
using System.Collections.Generic;
using System.Linq;
using CodeMetricsReportProcessor.Rendering;

namespace CodeMetricsReportProcessor.Parsing
{
    public class Module
    {
        public Module()
        {
            Namespaces = new List<Namespace>();
        }

        public static IEnumerable<string> RelevantMetrics = new[] { "MaintainabilityIndex" };

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
                results.Add(new FlatModuleScopedResult{Namespace = @namespace.Name, Metrics = @namespace.Metrics.LimitTo(Namespace.RelevantMetrics)});
                foreach (var type in @namespace.Types)
                {
                    results.Add(new FlatModuleScopedResult {Namespace = @namespace.Name, Type = type.Name, Metrics = type.Metrics.LimitTo(Type.RelevantMetrics) });
                    foreach (var member in type.Members)
                    {
                        results.Add(new FlatModuleScopedResult {Namespace = @namespace.Name, Type = type.Name, Member = member.Name, Metrics = member.Metrics.LimitTo(Member.RelevantMetrics) });
                    }
                }
            }

            return results;
        }

        public int GetWorstValueFor(string metric)
        {
            var qualityAssessor = new QualityAssessorFactory().Create(metric);
            var results = Flatten();
            var allMetricValues = results.Where(r => r.Metrics.ContainsKey(metric)).Select(r => r.Metrics[metric]);

            return allMetricValues.OrderBy(v => v, new MetricValueComparer(qualityAssessor)).First();
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