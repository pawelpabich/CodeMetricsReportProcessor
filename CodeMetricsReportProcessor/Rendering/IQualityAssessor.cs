namespace CodeMetricsReportProcessor.Rendering
{
    public interface IQualityAssessor
    {
        QualityLevel Assess(int value);
        bool? IsFirstWorseThanSecond(int first, int second);
    }
}