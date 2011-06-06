namespace CodeMetricsReportProcessor.Rendering
{
    public interface IQualityAssessor
    {
        QualityLevel Assess(int value);
    }
}