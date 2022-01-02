using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkFlow.Server.Data;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Entities;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Server.Models {
    public class ReportModel : IReport {
        private const Int32 SampleWindow = 30;
        private readonly ApplicationDbContext _context;
        private readonly IUtility _utilityService;

        public ReportModel(ApplicationDbContext context, IUtility utilityService) {
            _context = context;
            _utilityService = utilityService;
        }

        /// <summary>
        /// Calculate Projects Growth Rate and People required Per Project for the given time period in the given company
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        /// <exception cref="UnauthorizedAccessException">Raised if the request is not allowed for the current user (HTTP 401)</exception>
        public async Task<ForecastReportDto> Forecast(DateTime startDate, DateTime endDate, Guid companyId) {
            // Validate request data
            User? user = await _utilityService.GetUser();
            if (user == null) throw new InvalidDataException("Invalid User.");
            UserCompany? userCompany =
                await _context.UserCompany.FirstOrDefaultAsync(uc => uc.CompanyId == companyId && uc.UserId == user.Id);

            if (userCompany == null) throw new InvalidDataException("Invalid Company.");
            if (userCompany.Role != UserRole.Admin) throw new UnauthorizedAccessException("admin permission required");

            if (startDate.CompareTo(DateTime.Today) < 0 || endDate.CompareTo(startDate) <= 0) throw new InvalidDataException("invalid date range");

            Int32 numOfDays = (endDate - startDate).Days;
            // Offset is used by frontend to jump the time between today and the first selected date
            Int32 offset = (startDate - DateTime.Today).Days;

            // Get project that was created in past SampleWindow (30 days)
            List<Project> projects = await _context.Projects.Include("Users").Where(p =>
                p.Company!.Id == companyId &&
                p.CreatedAt.CompareTo(DateTime.Today.AddDays(-SampleWindow)) >= 0 &&
                p.CreatedAt.CompareTo(DateTime.Now) <= 0
            ).ToListAsync();

            if (projects.Count == 0) return new ForecastReportDto {NumberOfDays = numOfDays, Offset = offset};

            Double avgNumberOfUsers = (Double)projects.Sum(project => project.Users!.Count) / projects.Count;

            return new ForecastReportDto
            {
                NumberOfDays = numOfDays,
                CurrentProject = projects.Count,
                Offset = offset,
                ProjectGrowthRate = (Double)projects.Count / SampleWindow,
                PeoplePerProject = avgNumberOfUsers
            };
        }

        /// <summary>
        /// Create a summery of user's activities for the given company
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        /// <exception cref="UnauthorizedAccessException">Raised if the request is not allowed for the current user (HTTP 401)</exception>
        public async Task<UserProductivityDto> UserProductivity(String userId, Guid companyId) {
            // Request Validation
            User? currentUser = await _utilityService.GetUser();
            if (currentUser == null) throw new InvalidDataException("Invalid User.");
            UserCompany? currentUserCompany =
                await _context.UserCompany.FirstOrDefaultAsync(uc =>
                    uc.CompanyId == companyId && uc.UserId == currentUser.Id);

            if (currentUserCompany == null) throw new InvalidDataException("Invalid Company.");
            if (currentUserCompany.Role != UserRole.Admin) throw new UnauthorizedAccessException("admin permission required");

            // Collect user data
            UserCompany? userCompany = await _context.UserCompany.Include("User.Projects").FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CompanyId == companyId);
            if (userCompany == null) throw new InvalidDataException("Invalid UserId.");
            
            Ticket[] userTickets = await _context.Tickets.Where(t => t.Assignee == userCompany.User).ToArrayAsync();

            UserProductivityDto report = new UserProductivityDto
                {Name = userCompany.User!.Name!, Email = userCompany.User!.Email, NumOfProjects = userCompany.User!.Projects!.Count, UserRole = userCompany.Role};

            // Aggregate the user ticket data
            foreach (var ticket in userTickets)
                switch (ticket.Status)
                {
                    case Status.ToDo:
                        report.NumOfTodoTickets += 1;
                        break;
                    case Status.InProgress:
                        report.NumOfInProgressTickets += 1;
                        break;
                    case Status.Completed:
                        report.NumOfCompletedTickets += 1;
                        break;
                }

            return report;
        }
    }
}
