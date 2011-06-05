using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace CodeMetricsReportProcessor.IntegrationTests
{
    [TestFixture]
    public class code_metrics_report_processor
    {
        [Test]
        public void should_generate_correct_html_report()
        {
            Program.Main(new[] { @"Data\report1.xml", "Results" });

            var moduleFiles = Directory.GetFiles(Path.GetFullPath("Results"), "*.dll.html");
            moduleFiles.Count().ShouldBe(2);

           // For debugging only
           // Process.Start(Path.GetFullPath(@"Results\Summary.html"));
        }
    }
}
