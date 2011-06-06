using CodeMetricsReportProcessor.Rendering;
using NUnit.Framework;
using Shouldly;

namespace CodeMetricsReportProcessor.Tests.Rendering
{
    [TestFixture]
    public class cyclomatic_complexity_assessor
    {
        [Test]
        public void shold_mark_value_as_poor()
        {
            var result = new CyclomaticComplexityQualityAssessor().Assess(17);              
            result.ShouldBe(QualityLevel.Poor);
        }

        [Test]
        public void shold_mark_value_as_average()
        {
            var result = new CyclomaticComplexityQualityAssessor().Assess(12);              
            result.ShouldBe(QualityLevel.Average);
        }

        [Test]
        public void shold_mark_value_as_good()
        {
            var result = new CyclomaticComplexityQualityAssessor().Assess(8);              
            result.ShouldBe(QualityLevel.Good);
        }

    }

}
