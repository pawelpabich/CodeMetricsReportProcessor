namespace CodeMetricsReportProcessor.Rendering
{
    public class LinesOfCodeQualityAssessor : RangeBasedQualityAssessor
    {
        public LinesOfCodeQualityAssessor(): base(50, int.MaxValue, 15, 49, int.MinValue, 14)
        {
        }
    }
}