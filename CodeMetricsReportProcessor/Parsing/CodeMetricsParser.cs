using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace CodeMetricsReportProcessor.Parsing
{
    public class CodeMetricsParser
    {
        readonly IList<FlatResult> results = new List<FlatResult>();

        public Report Parse(string content)
        {
            var document = XDocument.Parse(content);
            var report = new Report();

            foreach (var targetElement in document.Descendants("Target"))
            {
                var target = ParseTarget(targetElement);
                report.Targets.Add(target);
            }

            return report;
        }

        private Target ParseTarget(XElement targetElement)
        {
            var target = new Target()
            {
                Name = ParseName(targetElement),
                Metrics = ParseMetrics(targetElement)
            };


            foreach (var moduleElement in targetElement.Descendants("Module"))
            {
                var module = ParseModule(moduleElement);
                target.Modules.Add(module);
            }

            return target;
        }

        private Module ParseModule(XElement moduleElement)
        {
            var module = new Module
            {
                Name = ParseName(moduleElement),
                Metrics = ParseMetrics(moduleElement)
            };

            foreach (var namespaceElement in moduleElement.Descendants("Namespace"))
            {
                var @namespace = ParseNamespace(namespaceElement);
                module.Namespaces.Add(@namespace);
            }

            return module;
        }

        private Namespace ParseNamespace(XElement namespaceElement)
        {
            var @namespace = new Namespace()
            {
                Name = ParseName(namespaceElement),
                Metrics = ParseMetrics(namespaceElement)
            };

            foreach (var typeElement in namespaceElement.Descendants("Type"))
            {
                var type = ParseType(typeElement);
                @namespace.Types.Add(type);
            }

            return @namespace;
        }

        private Type ParseType(XElement typeElement)
        {
            var type = new Type
                           {
                               Name = ParseName(typeElement),
                               Metrics = ParseMetrics(typeElement)
                           };

            foreach (var memberElement in typeElement.Descendants("Member"))
            {
                var member = ParseMember(memberElement);
                type.Members.Add(member);
            }

            return type;
        }

        private Member ParseMember(XElement memberElement)
        {
            var member = new Member()
                            {
                                Name = ParseName(memberElement),
                                Metrics = ParseMetrics(memberElement)
                            };

            return member;
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