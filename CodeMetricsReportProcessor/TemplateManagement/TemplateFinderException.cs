using System;

namespace CodeMetricsReportProcessor.TemplateManagement
{
    public class TemplateFinderException : Exception
    {
        public TemplateFinderException(string message):base(message)
        {
        }
    }
}