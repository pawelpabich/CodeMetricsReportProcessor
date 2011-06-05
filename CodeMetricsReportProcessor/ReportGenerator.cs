using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CodeMetricsReportProcessor.Parsing;
using CodeMetricsReportProcessor.Rendering;

namespace CodeMetricsReportProcessor
{
    public class ReportGenerator
    {
        public void GenerateFullReport(string codeMetricsDataFile, string reportOutputFolder)
        {
            var data = new CodeMetricsParser().Parse(GetContent(codeMetricsDataFile));

            var renderer = new TemplateRenderer();
            var summary = renderer.Render(GetContent(@"Templates\Summary.html"), data);
            SaveContent(reportOutputFolder, "Summary.html", summary);
        }

        private void SaveContent(string pathToFolder, string fileName, string content)
        {
            if (!Directory.Exists(pathToFolder)) Directory.CreateDirectory(pathToFolder);
            File.WriteAllText(Path.Combine(pathToFolder, fileName), content);
        }

        private string GetContent(string pathToFile)
        {
            return File.ReadAllText(pathToFile);
        }
    }
}
