using System;
using System.IO;
using System.Linq;
using CodeMetricsReportProcessor.Parsing;
using CodeMetricsReportProcessor.Rendering;
using CodeMetricsReportProcessor.TemplateManagement;

namespace CodeMetricsReportProcessor
{
    public class ReportGenerator
    {
        //TODO: this method is doing way too much - Pawel
        public void GenerateFullReport(string codeMetricsDataFile, string reportOutputFolder)
        {
            reportOutputFolder = Path.GetFullPath(reportOutputFolder);
            codeMetricsDataFile = Path.GetFullPath(codeMetricsDataFile);

            CopyTemplatesToTheOutputFolder(reportOutputFolder);

            var parser = new CodeMetricsParser();
            var data = parser.Parse(GetContent(codeMetricsDataFile));

            var templateFinder = new TemplateFinder(reportOutputFolder);
            var summaryTemplate = templateFinder.FindTemplateFor("Summary");

            var summaryTemplateContent = GetContent(summaryTemplate.FullPath);
            var renderer = new TemplateRenderer();
            var summaryView = renderer.Render(summaryTemplateContent, data);
            SaveContent(reportOutputFolder, summaryTemplate.Name + summaryTemplate.Extension, summaryView);

            var moduleTemplate = templateFinder.FindTemplateFor("Module");

            foreach (var module in data.Targets.SelectMany(t => t.Modules))
            {
                var moduleTemplateContent = GetContent(moduleTemplate.FullPath);
                var moduleView = renderer.Render(moduleTemplateContent, module);
                SaveContent(reportOutputFolder, module.Name + moduleTemplate.Extension, moduleView);
            }

            DeleteAllTemplateFiles(reportOutputFolder);
        }

        private void DeleteAllTemplateFiles(string reportOutputFolder)
        {
            foreach (var file in Directory.GetFiles(reportOutputFolder, "*.template*"))
            {
                File.Delete(file);
            }
        }

        private void CopyTemplatesToTheOutputFolder(string to)
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
    }
}
