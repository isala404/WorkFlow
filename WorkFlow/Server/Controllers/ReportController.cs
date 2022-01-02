using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [Microsoft.AspNetCore.Components.Inject]
        protected IReport ReportModel { get; set; }

        public ReportController(IReport reportModel)
        {
            ReportModel = reportModel;
        }

        // GET: api/report/forecast
        [HttpGet("forecast")]
        public async Task<IActionResult> Forecast([FromQuery(Name = "startDate")] String startDate,
            [FromQuery(Name = "endDate")] String endDate, [FromQuery(Name = "companyId")] Guid companyId)
        {
            try
            {
                DateTime startDateParsed = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime endDateParsed = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                ForecastReportDto forecastReport = await ReportModel.Forecast(startDateParsed, endDateParsed, companyId);
                return Ok(forecastReport);
            }
            catch (Exception e)
            {
                return e switch
                {
                    InvalidDataException => BadRequest(e.Message),
                    FormatException => BadRequest(e.Message),
                    UnauthorizedAccessException => Unauthorized(e.Message),
                    _ => e is DbUpdateException ? BadRequest(e.Message) : StatusCode(500, e)
                };
            }
        }
        
        // GET: api/report/user
        [HttpGet("user")]
        public async Task<IActionResult> UserProductivity([FromQuery(Name = "userId")] String userId, [FromQuery(Name = "companyId")] Guid companyId)
        {
            try
            {
                UserProductivityDto userProductivity = await ReportModel.UserProductivity(userId, companyId);
                return Ok(userProductivity);
            }
            catch (Exception e)
            {
                return e switch
                {
                    InvalidDataException => BadRequest(e.Message),
                    FormatException => BadRequest(e.Message),
                    UnauthorizedAccessException => Unauthorized(e.Message),
                    _ => e is DbUpdateException ? BadRequest(e.Message) : StatusCode(500, e)
                };
            }
        }
    }
}