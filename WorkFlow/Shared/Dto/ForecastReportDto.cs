using System;

namespace WorkFlow.Shared.Dto
{
    public class ForecastReportDto
    {
        public ForecastReportDto(){}
        public int CurrentProject { get; set; }
        public int NumberOfDays { get; set; }
        public int Offset { get; set; }
        public double ProjectGrowthRate { get; set; }
        public double PeoplePerProject { get; set; }
    }
}