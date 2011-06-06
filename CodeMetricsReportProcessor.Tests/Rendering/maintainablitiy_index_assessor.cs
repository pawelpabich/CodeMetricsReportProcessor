using CodeMetricsReportProcessor.Rendering;
using NUnit.Framework;
using Shouldly;

namespace CodeMetricsReportProcessor.Tests.Rendering
{
    [TestFixture]
    public class maintainablitiy_index_assessor
    {
        [Test]
        public void shold_mark_value_as_poor()
        {
            var result = new MaintainabilityIndexQualityAssessor().Assess(35);              
            result.ShouldBe(QualityLevel.Poor);
        }

        [Test]
        public void shold_mark_value_as_average()
        {
            var result = new MaintainabilityIndexQualityAssessor().Assess(50);              
            result.ShouldBe(QualityLevel.Average);
        }

        [Test]
        public void shold_mark_value_as_good()
        {
            var result = new MaintainabilityIndexQualityAssessor().Assess(70);              
            result.ShouldBe(QualityLevel.Good);
        }

    }

}
