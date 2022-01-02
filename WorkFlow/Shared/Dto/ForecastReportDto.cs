using System;

namespace WorkFlow.Shared.Dto {
    public class ForecastReportDto {

        public Int32 CurrentProject { get; set; }

        public Int32 NumberOfDays { get; set; }

        public Int32 Offset { get; set; }

        public Double ProjectGrowthRate { get; set; }

        public Double PeoplePerProject { get; set; }
    }
}
