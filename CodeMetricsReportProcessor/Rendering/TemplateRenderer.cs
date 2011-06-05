using System;
using RazorEngine;

namespace CodeMetricsReportProcessor.Rendering
{
    public class TemplateRenderer
    {
        public string Render<T>(string template, T model)
        {
            return Razor.Parse(template, model);
        }
    }
}