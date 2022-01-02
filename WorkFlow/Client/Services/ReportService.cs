using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Client.Services {
    public class ReportService : IReport {
        private const String EntityName = "report";
        private readonly HttpClient _http;

        public ReportService(HttpClient http) {
            _http = http;
        }
        
        /// <summary>
        /// Get the ProjectGrowthRate and PeoplePerProject for given time range for a company
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<ForecastReportDto> Forecast(DateTime startDate, DateTime endDate, Guid companyId) {
            ForecastReportDto? forecast = await _http.GetFromJsonAsync<ForecastReportDto>($"api/report/forecast?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}&companyId={companyId}");
            if (forecast == null) throw new ApplicationException("Error while getting forecast report");
            return forecast;
        }

        /// <summary>
        /// Get summery of a user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<UserProductivityDto> UserProductivity(String userId, Guid companyId) {
            UserProductivityDto? userReport = await _http.GetFromJsonAsync<UserProductivityDto>($"api/report/user?userId={userId}&companyId={companyId}");
            if (userReport == null) throw new ApplicationException("Error while getting user report");
            return userReport;
        }
    }
}
