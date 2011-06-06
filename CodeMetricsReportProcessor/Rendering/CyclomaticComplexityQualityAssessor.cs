namespace CodeMetricsReportProcessor.Rendering
{
    public class CyclomaticComplexityQualityAssessor : RangeBasedQualityAssessor
    {
        public CyclomaticComplexityQualityAssessor(): base(16, 10000, 10, 15, 0, 9)
        {
        }
    }
}