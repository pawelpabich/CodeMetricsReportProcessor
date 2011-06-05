using System;
using System.Linq;
using CodeMetricsReportProcessor.Parsing;
using CodeMetricsReportProcessor.Rendering;
using NUnit.Framework;
using Shouldly;

namespace CodeMetricsReportProcessor.Tests.Rendering
{
    [TestFixture]
    public class template_renderer
    {
        [Test]
        public void renders_template_correclty()
        {        
            const string output = @"Constant test and single VALUE";
            const string template = @"Constant test and single @Model.Property";

            var parser = new TemplateRenderer();
            var report = parser.Render(template, new Test {Property = "VALUE"});

           report.ShouldBe(output);
        }
    }

    public class Test
    {
        public string Property { get; set; }
    }
}
