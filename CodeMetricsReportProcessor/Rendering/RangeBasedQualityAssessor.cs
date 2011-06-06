using System;

namespace CodeMetricsReportProcessor.Rendering
{
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

            throw new QualityCantBeDeterminedException(this.GetType().Name, value);
        }

        public bool? IsFirstWorseThanSecond(int first, int second)
        {
            if (first == second) return null;
            var bestPossibleValue = goodFrom == 0 ? goodFrom : goodTo;
            return Math.Abs(bestPossibleValue - first) > Math.Abs(bestPossibleValue - second);
        }
    }
}