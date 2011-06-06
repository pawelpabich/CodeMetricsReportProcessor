using System;

namespace CodeMetricsReportProcessor.Rendering
{
    public class QualityAssessorFactory
    {
        public IQualityAssessor Create(string metric)
        {
            var normalizedMetricName = NormalizeMetricName(metric);
            switch (normalizedMetricName)
            {
                case "maintainabilityindex": return new  MaintainabilityIndexQualityAssessor();
                case "cyclomaticcomplexity": return new CyclomaticComplexityQualityAssessor();
                case "classcoupling": return new ClassCouplingQualityAssessor();
                case "depthofinheritance": return new DepthOfInheritanceQualityAssessor();
                case "linesofcode": return new LinesOfCodeQualityAssessor();
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private string NormalizeMetricName(string metric)
        {
            return metric.ToLower().Trim().Replace(" ", String.Empty);
        }
    }
}