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

        public async Task<ForecastReportDto> Forecast(DateTime startDate, DateTime endDate, Guid companyId) {
            User? user = await _utilityService.GetUser();
            if (user == null) throw new InvalidDataException("Invalid User.");
            UserCompany? userCompany =
                await _context.UserCompany.FirstOrDefaultAsync(uc => uc.CompanyId == companyId && uc.UserId == user.Id);

            if (userCompany == null) throw new InvalidDataException("Invalid Company.");
            if (userCompany.Role != UserRole.Admin) throw new UnauthorizedAccessException("admin permission required");

            if (startDate.CompareTo(DateTime.Today) < 0 || endDate.CompareTo(startDate) <= 0) throw new InvalidDataException("invalid date range");

            Int32 numOfDays = (endDate - startDate).Days;
            Int32 offset = (startDate - DateTime.Today).Days;

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

        public async Task<UserProductivityDto> UserProductivity(String userId, Guid companyId) {
            User? currentUser = await _utilityService.GetUser();
            if (currentUser == null) throw new InvalidDataException("Invalid User.");
            UserCompany? currentUserCompany =
                await _context.UserCompany.FirstOrDefaultAsync(uc =>
                    uc.CompanyId == companyId && uc.UserId == currentUser.Id);

            if (currentUserCompany == null) throw new InvalidDataException("Invalid Company.");
            if (currentUserCompany.Role != UserRole.Admin) throw new UnauthorizedAccessException("admin permission required");

            UserCompany? userCompany = await _context.UserCompany.Include("User.Projects").FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CompanyId == companyId);
            if (userCompany == null) throw new InvalidDataException("Invalid UserId.");

            Ticket[] userTickets = await _context.Tickets.Where(t => t.Assignee == userCompany.User).ToArrayAsync();

            UserProductivityDto report = new UserProductivityDto
                {Name = userCompany.User!.Name!, Email = userCompany.User!.Email, NumOfProjects = userCompany.User!.Projects!.Count, UserRole = userCompany.Role};

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
