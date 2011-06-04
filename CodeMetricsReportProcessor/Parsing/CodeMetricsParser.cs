using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace CodeMetricsReportProcessor.Parsing
{
    public class CodeMetricsParser
    {
        readonly IList<CodeMetricsResult> results = new List<CodeMetricsResult>();

        public IEnumerable<CodeMetricsResult> Parse(string content)
        {
            var document = XDocument.Parse(content);

            foreach (var target in document.Descendants("Target"))
            {
                ParseTarget(target);
            }

            return results;
        }

        private void ParseTarget(XElement target)
        {
            var result = new CodeMetricsResult
            {
                Target = ParseName(target),
                Metrics = ParseMetrics(target)
            };

            results.Add(result);

            foreach (var module in target.Descendants("Module"))
            {
                ParseModule(module, result.Target);
            }
        }

        private void ParseModule(XElement module, string target)
        {
            var result = new CodeMetricsResult
            {
                Target = target,
                Module = ParseName(module),
                Metrics = ParseMetrics(module)
            };

            results.Add(result);

            foreach (var @namespace in module.Descendants("Namespace"))
            {
                ParseNamespace(@namespace, target, result.Module);
            }
        }

        private void ParseNamespace(XElement @namespace, string target, string module)
        {
            var result = new CodeMetricsResult
            {
                Target = target,
                Module = module,
                Namespace = ParseName(@namespace),
                Metrics = ParseMetrics(@namespace)
            };

            results.Add(result);

            foreach (var type in @namespace.Descendants("Type"))
            {
                ParseType(type, target, module, result.Namespace);
            }
        }

        private void ParseType(XElement type, string target, string module, string @namespace)
        {
            var result = new CodeMetricsResult
                            {
                                Target = target,
                                Module = module,
                                Namespace = @namespace,
                                Type = ParseName(type),
                                Metrics = ParseMetrics(type)
                            };

            results.Add(result);

            foreach (var memeber in type.Descendants("Member"))
            {
                ParseMember(memeber, target, module, @namespace, result.Type);
            }
        }

        private void ParseMember(XContainer member, string target, string module, string @namespace, string type)
        {

            var entry = new CodeMetricsResult
                            {
                                Target = target,
                                Module = module,
                                Namespace = @namespace,
                                Type = type,
                                Metrics = ParseMetrics(member)
                            };

            results.Add(entry);
        }

        private static Dictionary<string, int> ParseMetrics(XContainer element)
        {
            var rawMetrics = element.Descendants("Metrics").First().Descendants();
            var parsedMetrics = new Dictionary<string, int>();

            foreach (var metric in rawMetrics)
            {
                var metricName = metric.Attribute("Name").Value;
                var metricValue = int.Parse(metric.Attribute("Value").Value, NumberStyles.Any);
                parsedMetrics.Add(metricName, metricValue);

            }
            return parsedMetrics;
        }

        private string ParseName(XElement item)
        {
            return item.Attribute("Name").Value;
        }
    }
}