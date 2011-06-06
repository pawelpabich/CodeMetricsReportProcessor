namespace CodeMetricsReportProcessor.Rendering
{
    public class CyclomaticComplexityQualityAssessor : RangeBasedQualityAssessor
    {
        public CyclomaticComplexityQualityAssessor(): base(16, int.MaxValue, 10, 15, int.MinValue, 9)
        {
        }
    }
}