using System;
using System.Threading.Tasks;
using WorkFlow.Shared.Dto;

namespace WorkFlow.Shared.Interfaces {
    public interface IReport {
        Task<ForecastReportDto> Forecast(DateTime startDate, DateTime endDate, Guid companyId);
        Task<UserProductivityDto> UserProductivity(String userId, Guid companyId);
    }
}
