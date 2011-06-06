namespace CodeMetricsReportProcessor.Rendering
{
    public class ClassCouplingQualityAssessor : RangeBasedQualityAssessor
    {
        public ClassCouplingQualityAssessor() : base(21, int.MaxValue, 20, 20, int.MinValue, 19)
        {
        }
    }
}