namespace CodeMetricsReportProcessor.Rendering
{
    public class ClassCouplingQualityAssessor : RangeBasedQualityAssessor
    {
        public ClassCouplingQualityAssessor() : base(21, 10000, 20, 20, 0, 19)
        {
        }
    }
}