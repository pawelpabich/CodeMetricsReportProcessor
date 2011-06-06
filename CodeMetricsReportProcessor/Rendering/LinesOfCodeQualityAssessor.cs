namespace CodeMetricsReportProcessor.Rendering
{
    public class LinesOfCodeQualityAssessor : RangeBasedQualityAssessor
    {
        public LinesOfCodeQualityAssessor(): base(50, 10000, 15, 49, 0, 14)
        {
        }
    }
}