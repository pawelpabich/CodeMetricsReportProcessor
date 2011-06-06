using CodeMetricsReportProcessor.Rendering;
using NUnit.Framework;
using Shouldly;

namespace CodeMetricsReportProcessor.Tests.Rendering
{
    [TestFixture]
    public class class_coupling_assessor
    {
        [Test]
        public void shold_mark_value_as_poor()
        {
            var result = new ClassCouplingQualityAssessor().Assess(22);              
            result.ShouldBe(QualityLevel.Poor);
        }

        [Test]
        public void shold_mark_value_as_average()
        {
            var result = new ClassCouplingQualityAssessor().Assess(20);              
            result.ShouldBe(QualityLevel.Average);
        }

        [Test]
        public void shold_mark_value_as_good()
        {
            var result = new ClassCouplingQualityAssessor().Assess(12);              
            result.ShouldBe(QualityLevel.Good);
        }

    }

}
