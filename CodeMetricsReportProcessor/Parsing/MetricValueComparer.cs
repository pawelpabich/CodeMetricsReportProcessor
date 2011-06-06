using System.Collections.Generic;
using CodeMetricsReportProcessor.Rendering;

namespace CodeMetricsReportProcessor.Parsing
{
    public class MetricValueComparer : IComparer<int>
    {
        private readonly IQualityAssessor qualityAssessor;

        public MetricValueComparer(IQualityAssessor qualityAssessor)
        {
            this.qualityAssessor = qualityAssessor;
        }

        public int Compare(int x, int y)
        {
            var result = qualityAssessor.IsFirstWorseThanSecond(x, y);
            if (result == null) return 0;
            return  result.Value ? -1 : 1;
        }
    }
}