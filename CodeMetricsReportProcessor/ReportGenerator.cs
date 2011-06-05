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
            CopyTemplates(reportOutputFolder);

            var parser = new CodeMetricsParser();
            var data = parser.Parse(GetContent(codeMetricsDataFile));

            var renderer = new TemplateRenderer();
            var summary = renderer.Render(GetContent(reportOutputFolder, "Summary.template.html"), data);
            SaveContent(reportOutputFolder, "Summary.html", summary);
        }

        private void CopyTemplates(string to)
        {
            var from = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates");
            if (Directory.Exists(to))
            {
                Directory.Delete(to, true);               
            }

            Directory.CreateDirectory(to);

            foreach (var fromFolderPath in Directory.GetDirectories(from, "*", SearchOption.AllDirectories))
            {
                var toFolderPath = fromFolderPath.Replace(from, to);
                Directory.CreateDirectory(toFolderPath);
            }

            foreach (var fromFilePath in Directory.GetFiles(from, "*.*", SearchOption.AllDirectories))
            {
                var toFilePath = fromFilePath.Replace(from, to);
                File.Copy(fromFilePath, toFilePath, true);
            }
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

        private string GetContent(string pathToFolder, string fileName)
        {
            return GetContent(Path.Combine(pathToFolder, fileName));
        }
    }
}
