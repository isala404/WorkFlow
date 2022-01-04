using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WorkFlow.Server.Data;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Entities;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Server.Models {
    public class ProjectModel : IProject {
        private readonly ApplicationDbContext _context;
        private readonly IUtility _utilityService;

        public ProjectModel(ApplicationDbContext context, IUtility utilityService) {
            _context = context;
            _utilityService = utilityService;
        }

        /// <summary>
        /// Get project for the selected user
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        public async Task<List<ProjectDto>> List() {
            User? user = await _utilityService.GetUser();
            if (user == null) throw new InvalidDataException("Invalid User.");

            List<ProjectDto> projects = new List<ProjectDto>();

            List<Project> userProjects = await _context.Projects.Where(project => project.Users!.Contains(user)).ToListAsync();
            projects.AddRange(userProjects.Select(userProject => new ProjectDto(userProject)));

            return projects;
        }

        /// <summary>
        /// Get projects by company id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        public async Task<List<ProjectDto>> List(Guid companyId) {
            User? user = await _utilityService.GetUser();
            if (user == null) throw new InvalidDataException("Invalid User.");

            UserCompany? userCompany = await _context.UserCompany.FirstOrDefaultAsync(userCompany =>
                userCompany.UserId == user.Id && userCompany.CompanyId == companyId);
            if (userCompany == null) throw new InvalidDataException("Invalid Company.");

            List<ProjectDto> projects = new List<ProjectDto>();

            // If the user is admin return all the project
            // Else return projects only the user is apart of
            if (userCompany.Role == UserRole.Admin)
            {
                List<Project> userProjects = await _context.Projects.Include("Tickets").Include("Users").Include("Company")
                    .Where(project => project.Company!.Id == userCompany.CompanyId).ToListAsync();
                projects.AddRange(userProjects.Select(userProject => new ProjectDto(userProject)));
            }
            else
            {
                List<Project> userProjects = await _context.Projects.Include("Tickets").Include("Users").Include("Company")
                    .Where(project => project.Company!.Id == userCompany.CompanyId && project.Users!.Contains(user))
                    .ToListAsync();
                projects.AddRange(userProjects.Select(userProject => new ProjectDto(userProject)));
            }


            return projects;
        }

        /// <summary>
        /// Get project by Id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        public async Task<ProjectDto> Get(Guid projectId) {
            User? user = await _utilityService.GetUser();
            if (user == null) throw new InvalidDataException("Invalid User.");

            Project? project =
                await _context.Projects.FirstOrDefaultAsync(project =>
                    project.Id == projectId && project.Users!.Contains(user));
            if (project == null) throw new InvalidDataException("Invalid ProjectId.");

            return new ProjectDto(project);
        }

        /// <summary>
        /// Get project by URI (ex - iconicto/chari-lake)
        /// </summary>
        /// <param name="companyUri"></param>
        /// <param name="projectUri"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        public async Task<ProjectDto> Get(String companyUri, String projectUri) {
            User? user = await _utilityService.GetUser();
            if (user == null) throw new InvalidDataException("Invalid User.");

            // Select that has correct projectUri and companyUri and check if the user has access to read it  
            Project? project = await _context.Projects.Include("Users").FirstOrDefaultAsync(p =>
                p.Uri == projectUri &&
                p.Company!.Uri == companyUri &&
                (
                    p.Users!.Contains(user) ||
                    p.Company!.Users!.FirstOrDefault(uc => uc.User!.Id == user.Id && uc.Role == UserRole.Admin) != null
                )
            );
            if (project == null) throw new InvalidDataException("Invalid Project Uri.");

            return new ProjectDto(project);
        }

        /// <summary>
        /// Create a new project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task<ProjectDto> Create(ProjectDto project) {
            (_, Company company) = await VerifyRequest(project);
            Project newProject = new Project
            {
                Id = default,
                Name = project.Name,
                Status = project.Status,
                DueDate = project.DueDate,
                Uri = project.Uri,
                Company = company
            };
            EntityEntry<Project> result = _context.Projects.Add(newProject);
            await _context.SaveChangesAsync();
            return new ProjectDto(result.Entity);
        }

        /// <summary>
        /// Update existing project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        public async Task<ProjectDto> Update(Guid projectId, ProjectDto project) {
            await VerifyRequest(project);

            Project? targetProject = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
            if (targetProject == null) throw new InvalidDataException("Invalid projectId.");

            targetProject.Name = project.Name;
            targetProject.Status = project.Status;
            targetProject.DueDate = project.DueDate;
            targetProject.Uri = project.Uri;

            await _context.SaveChangesAsync();
            return new ProjectDto(targetProject);
        }

        /// <summary>
        /// Delete existing project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        public async Task<Boolean> Delete(Guid projectId) {
            Project? project = await _context.Projects.Include("Company")
                .FirstOrDefaultAsync(project => project.Id == projectId);
            if (project == null) throw new InvalidDataException("Invalid projectId.");

            await VerifyRequest(new ProjectDto(project));

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Add or Remove a user from project (Deprecated, use  UserModel.ModifyProject())
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        [Obsolete("Please use the UserModel.ModifyProject()")]
        public async Task<ProjectDto> ModifyUser(Guid projectId, UserDto userDto) {
            Project? project = await _context.Projects.FirstOrDefaultAsync(project => project.Id == projectId);
            if (project == null) throw new InvalidDataException("Invalid projectId.");

            await VerifyRequest(new ProjectDto(project));

            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id);
            if (user == null) throw new InvalidDataException("Invalid UserId.");

            if (project.Users!.Contains(user))
                project.Users.Remove(user);
            else
                project.Users.Add(user);

            await _context.SaveChangesAsync();

            return new ProjectDto(project);
        }

        /// <summary>
        /// Verify the incoming request is valid and has permission to do the required operation
        /// </summary>
        /// <param name="project">ProjectDto that's intended to be interacted with</param>
        /// <param name="admin">Admin only Method</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        /// <exception cref="UnauthorizedAccessException">Raised if the request is only allowed for admins (HTTP 401)</exception>
        private async Task<Tuple<User, Company>> VerifyRequest(ProjectDto project, Boolean admin = true) {
            User? user = await _utilityService.GetUser();
            if (user == null) throw new InvalidDataException("Invalid User.");

            Company? company = await _context.Companies.FirstOrDefaultAsync(company => company.Id == project.Company.Id);
            if (company == null) throw new InvalidDataException("Invalid Company.");

            UserCompany? userCompany = await _context.UserCompany.FirstOrDefaultAsync(userCompany =>
                userCompany.Company == company && userCompany.User == user);

            if (userCompany == null) throw new UnauthorizedAccessException("User does not have required permission");

            if (admin && userCompany.Role != UserRole.Admin)
                throw new UnauthorizedAccessException("User does not have required permission");

            return new Tuple<User, Company>(user, company);
        }
    }
}
