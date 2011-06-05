using System;
using System.IO;
using System.Linq;

namespace CodeMetricsReportProcessor.TemplateManagement
{
    public class TemplateFinder
    {
        private readonly string pathToFolderWithTemplates;

        public TemplateFinder(string pathToFolderWithTemplates)
        {
            this.pathToFolderWithTemplates = pathToFolderWithTemplates;
        }

        public Template FindTemplateFor(string templateName)
        {
            var result = Directory.GetFiles(pathToFolderWithTemplates, templateName + ".*");
            if (result.Length == 0) throw new TemplateFinderException("Couldn't find " + templateName + " template");
            if (result.Length > 1) throw new TemplateFinderException("Found more than one matching template for " + templateName + 
                                                                     "Candidates: " + String.Join(",", result) );
            var match = result.Single();
            return new Template {Name = templateName, Extension = Path.GetExtension(match), FullPath = match};
        }
    }
}