namespace CodeMetricsReportProcessor.Rendering
{
    public class MaintainabilityIndexQualityAssessor : RangeBasedQualityAssessor
    {
        public MaintainabilityIndexQualityAssessor(): base(0, 39, 40, 59, 60, 10000)
        {
        }
    }
}