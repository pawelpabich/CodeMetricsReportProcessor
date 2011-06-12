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

            var qualityAssessor = new QualityAssessorFactory().Create(metric);

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

        public string ReplaceDotsWithSpaces(string value)
        {
            if (value == null) return value;
            return value.Replace(".", " ");
        }
    }
}