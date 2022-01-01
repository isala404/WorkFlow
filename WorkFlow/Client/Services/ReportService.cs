using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Client.Services
{
    public class ReportService: IReport
    {
        private readonly HttpClient _http;
        private const string EntityName = "company";

        public ReportService(HttpClient http)
        {
            _http = http;
        }
        public async Task<ForecastReportDto> Forecast(DateTime startDate, DateTime endDate, Guid companyId)
        {
            var forecast = await _http.GetFromJsonAsync<ForecastReportDto>($"api/report/forecast?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}&companyId={companyId}");
            if (forecast == null) throw new ApplicationException($"Error while getting forecast report");
            return forecast;
        }
    }
}