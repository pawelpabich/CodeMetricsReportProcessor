using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace CodeMetricsReportProcessor.IntegrationTests
{
    [TestFixture]
    public class report_generator
    {
        [Test]
        public void should_generate_correct_html_report()
        {
            var reportGenerator = new ReportGenerator();
            reportGenerator.GenerateFullReport(@"Data\report1.xml", "Results");

            var moduleFiles = Directory.GetFiles(Path.GetFullPath("Results"), "*.dll.html");
            moduleFiles.Count().ShouldBe(2);

           // For debugging only
           // Process.Start(Path.GetFullPath(@"Results\Summary.html"));
        }
    }
}
