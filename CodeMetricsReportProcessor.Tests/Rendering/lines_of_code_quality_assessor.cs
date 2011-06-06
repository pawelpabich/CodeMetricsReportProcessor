using CodeMetricsReportProcessor.Rendering;
using NUnit.Framework;
using Shouldly;

namespace CodeMetricsReportProcessor.Tests.Rendering
{
    [TestFixture]
    public class lines_of_code_quality_assessor
    {
        [Test]
        public void shold_mark_value_as_poor()
        {
            var result = new LinesOfCodeQualityAssessor().Assess(55);              
            result.ShouldBe(QualityLevel.Poor);
        }

        [Test]
        public void shold_mark_value_as_average()
        {
            var result = new LinesOfCodeQualityAssessor().Assess(25);              
            result.ShouldBe(QualityLevel.Average);
        }

        [Test]
        public void shold_mark_value_as_good()
        {
            var result = new LinesOfCodeQualityAssessor().Assess(8);              
            result.ShouldBe(QualityLevel.Good);
        }

    }

}
