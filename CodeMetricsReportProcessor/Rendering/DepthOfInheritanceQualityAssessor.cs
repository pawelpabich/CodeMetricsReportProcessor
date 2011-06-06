namespace CodeMetricsReportProcessor.Rendering
{
    public class DepthOfInheritanceQualityAssessor : RangeBasedQualityAssessor
    {
        public DepthOfInheritanceQualityAssessor() : base(10, 10000, 5, 9, 0, 4)
        {
        }
    }
}