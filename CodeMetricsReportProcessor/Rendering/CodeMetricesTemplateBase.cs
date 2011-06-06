using System;
using RazorEngine.Templating;

namespace CodeMetricsReportProcessor.Rendering
{
    public class CodeMetricesTemplateBase<T> : TemplateBase<T>
    {
        // values are based on http://geekswithblogs.net/terje/archive/2008/11/25/code-metrics---suggestions-for-appropriate-limits.aspx
        public string GetQualityLevelFor(string metric, int? value)
        {
            if (value == null) return "notavailable";

            var normalizedMetricName = NormalizeMetricName(metric);
            IQualityAssessor qualityAssessor = null;
            switch(normalizedMetricName)
            {
                case "maintainabilityindex": qualityAssessor = new MaintainabilityIndexQualityAssessor(); break;
                case "cyclomaticcomplexity": qualityAssessor = new CyclomaticComplexityQualityAssessor(); break;
                case "classcoupling": qualityAssessor = new ClassCouplingQualityAssessor(); break;
                case "depthofinheritance": qualityAssessor = new DepthOfInheritanceQualityAssessor(); break;
                case "linesofcode": qualityAssessor = new LinesOfCodeQualityAssessor(); break;

            }

            var quality = qualityAssessor.Assess(value.Value);
            switch (quality)
            {
                case QualityLevel.Poor: return "poor";
                case QualityLevel.Average: return "average";
                case QualityLevel.Good: return "good";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string NormalizeMetricName(string metric)
        {
            return metric.ToLower().Trim().Replace(" ", String.Empty);
        }
    }
}