using RazorEngine;

namespace CodeMetricsReportProcessor.Rendering
{
    public class TemplateRenderer
    {
        public string Render<T>(string template, T model)
        {
            Razor.SetTemplateBase(typeof(CodeMetricesTemplateBase<>));
            return Razor.Parse(template, model);
        }
    }
}