using System;
using RazorEngine;
using RazorEngine.Templating;

namespace CodeMetricsReportProcessor.Rendering
{
    public class TemplateRenderer
    {
        public string Render<T>(string template, T model)
        {
            Razor.SetTemplateBase(typeof(CodeMetricesTemplateBase<>));
            return Razor.Parse(template, model);
        }
    }

    public class CodeMetricesTemplateBase<T> : TemplateBase<T>
    {
        // values are based on http://geekswithblogs.net/terje/archive/2008/11/25/code-metrics---suggestions-for-appropriate-limits.aspx
        public string GetQualityLevelFor(string metric, int? value)
        {
            if (value == null) return "notavailable";

            var normalizedMetricName = NormalizeMetricName(metric);
            IQualityAssessor qualityAssessor = null;
            switch(normalizedMetricName)
            {
                case "maintainabilityindex": qualityAssessor = new MaintainabilityIndexQualityAssessor(); break;
                case "cyclomaticcomplexity": qualityAssessor = new CyclomaticComplexityQualityAssessor(); break;
                case "classcoupling": qualityAssessor = new ClassCouplingQualityAssessor(); break;
                case "depthofinheritance": qualityAssessor = new DepthOfInheritanceQualityAssessor(); break;
                case "linesofcode": qualityAssessor = new LinesOfCodeQualityAssessor(); break;

            }

            var quality = qualityAssessor.Assess(value.Value);
            switch (quality)
            {
                case QualityLevel.NotAvailable: return "notavailable";
                case QualityLevel.Poor: return "poor";
                case QualityLevel.Average: return "average";
                case QualityLevel.Good: return "good";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string NormalizeMetricName(string metric)
        {
            return metric.ToLower().Trim().Replace(" ", String.Empty);
        }
    }

    public class LinesOfCodeQualityAssessor : RangeBasedQualityAssessor
    {
        public LinesOfCodeQualityAssessor(): base(0, 20, 21, 40, 41, 100)
        {
        }
    }

    public class RangeBasedQualityAssessor : IQualityAssessor
    {
        private readonly int poorFrom;
        private readonly int poorTo;
        private readonly int averageFrom;
        private readonly int averageTo;
        private readonly int goodFrom;
        private readonly int goodTo;

        public RangeBasedQualityAssessor(int poorFrom, int poorTo, int averageFrom, int averageTo, int goodFrom, int goodTo)
        {
            this.poorFrom = poorFrom;
            this.poorTo = poorTo;
            this.averageFrom = averageFrom;
            this.averageTo = averageTo;
            this.goodFrom = goodFrom;
            this.goodTo = goodTo;
        }


        public QualityLevel Assess(int value)
        {
            if (poorFrom <= value && value <= poorTo) return QualityLevel.Poor;
            if (averageFrom <= value && value <= averageTo) return QualityLevel.Average;
            if (goodFrom <= value && value <= goodTo) return QualityLevel.Good;

            return QualityLevel.NotAvailable;
        }
    }

    public class DepthOfInheritanceQualityAssessor : RangeBasedQualityAssessor
    {
        public DepthOfInheritanceQualityAssessor() : base(0, 20, 21, 40, 41, 100)
        {
        }
    }

    public class ClassCouplingQualityAssessor : RangeBasedQualityAssessor
    {
        public ClassCouplingQualityAssessor() : base(0, 20, 21, 40, 41, 100)
        {
        }
    }

    public class CyclomaticComplexityQualityAssessor : RangeBasedQualityAssessor
    {
        public CyclomaticComplexityQualityAssessor(): base(0, 20, 21, 40, 41, 100)
        {
        }
    }

    public class MaintainabilityIndexQualityAssessor : RangeBasedQualityAssessor
    {
        public MaintainabilityIndexQualityAssessor(): base(0, 20, 21, 40, 41, 100)
        {
        }
    }

    public interface IQualityAssessor
    {
        QualityLevel Assess(int value);
    }

    public enum QualityLevel
    {
        NotAvailable,
        Poor,
        Average,
        Good
    }
}