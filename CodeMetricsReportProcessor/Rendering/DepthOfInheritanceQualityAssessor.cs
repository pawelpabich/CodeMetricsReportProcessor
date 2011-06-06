namespace CodeMetricsReportProcessor.Rendering
{
    public class DepthOfInheritanceQualityAssessor : RangeBasedQualityAssessor
    {
        public DepthOfInheritanceQualityAssessor() : base(10, int.MaxValue, 5, 9, int.MinValue, 4)
        {
        }
    }
}