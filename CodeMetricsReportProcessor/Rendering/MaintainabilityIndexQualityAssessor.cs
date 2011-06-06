namespace CodeMetricsReportProcessor.Rendering
{
    public class MaintainabilityIndexQualityAssessor : RangeBasedQualityAssessor
    {
        public MaintainabilityIndexQualityAssessor(): base(int.MinValue, 39, 40, 59, 60, int.MaxValue)
        {
        }
    }
}