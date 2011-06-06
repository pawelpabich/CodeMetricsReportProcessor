using System;

namespace CodeMetricsReportProcessor.Rendering
{
    public class QualityCantBeDeterminedException : Exception
    {
        public QualityCantBeDeterminedException(string assessorName, int value) : base(assessorName + " can't deterimine quality level for the the following value: " + value)
        {
        }
    }
}