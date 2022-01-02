using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WorkFlow.Server.Data;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Entities;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Server.Models {
    public class UserModel : IUser {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IUtility _utilityService;

        public UserModel(ApplicationDbContext context, IUtility utilityService, UserManager<User> userManager) {
            _context = context;
            _utilityService = utilityService;
            _userManager = userManager;
        }

        /// <summary>
        /// Get current user's data
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        public async Task<UserDto> Get() {
            User? currentUser = await _utilityService.GetUser();
            if (currentUser == null) throw new InvalidDataException("Invalid User.");
            User user = await _context.Users.Include("Companies.Company").SingleAsync(u => u.Id == currentUser.Id);
            return new UserDto(user);
        }

        /// <summary>
        /// Get a user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        public async Task<UserDto> Get(String id) {
            User? user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
            if (user == null) throw new InvalidDataException("User not found.");
            return new UserDto(user);
        }

        /// <summary>
        /// Get a user of a specific project 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        public async Task<List<UserDto>> GetUsersByCompany(Guid companyId) {
            User? currentUser = await _utilityService.GetUser();
            if (currentUser == null) throw new InvalidDataException("Invalid User.");

            Company? companies = await _context.Companies.Include("Users.User").FirstOrDefaultAsync(c => c.Id == companyId);
            if (companies == null) throw new InvalidDataException("Invalid CompanyID.");

            return companies.Users!.Select(user => new UserDto(user.User!)).ToList();
        }

        /// <summary>
        /// Update current user's data
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        public async Task<UserDto> Update(UserDto user) {
            User? currentUser = await _utilityService.GetUser();
            if (currentUser == null) throw new InvalidDataException("Invalid User.");

            User targetUser = await _context.Users.Include("Companies.Company").SingleAsync(u => u.Id == currentUser.Id);
            targetUser.Name = user.Name;
            targetUser.UserName = user.UserName;
            targetUser.Email = user.Email;

            if (user.Password != null) targetUser.PasswordHash = _userManager.PasswordHasher.HashPassword(targetUser, user.Password);

            await _context.SaveChangesAsync();
            return new UserDto(targetUser);
        }

        /// <summary>
        /// Delete current user's account
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        public async Task<Boolean> Delete() {
            User? currentUser = await _utilityService.GetUser();
            if (currentUser == null) throw new InvalidDataException("Invalid User.");

            _context.Users.Remove(currentUser);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Add the user to the company if user and company doesn't have relationship
        /// Remove user from the company if the select role is NULL
        /// If not update the role of the user 
        /// </summary>
        /// <param name="userInvite"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        /// <exception cref="UnauthorizedAccessException">Raised if the request is not allowed for the current user (HTTP 401)</exception>
        public async Task<UserCompanyDto> SetUserCompany(UserInvite userInvite) {
            User? currentUser = await _utilityService.GetUser();
            if (currentUser == null) throw new InvalidDataException("Invalid User.");

            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userInvite.Email);
            if (user == null) throw new InvalidDataException("User not found.");

            UserCompany? currentUserCompany = await _context.UserCompany.FirstOrDefaultAsync(u =>
                u.UserId == currentUser.Id && u.CompanyId == userInvite.CompanyId && u.Role == UserRole.Admin);
            if (currentUserCompany == null)
                throw new UnauthorizedAccessException("User does not have required permission");

            UserCompany? userCompany = await _context.UserCompany.FirstOrDefaultAsync(u =>
                u.CompanyId == userInvite.CompanyId && u.UserId == user.Id);

            switch (userCompany)
            {
                case null when userInvite.Role != null:
                {
                    UserCompany newUserCompany = new UserCompany
                    {
                        UserId = user.Id,
                        CompanyId = userInvite.CompanyId,
                        Role = (UserRole)userInvite.Role
                    };

                    EntityEntry<UserCompany> result = await _context.UserCompany.AddAsync(newUserCompany);
                    await _context.SaveChangesAsync();
                    return new UserCompanyDto(result.Entity);
                }
                case null:
                    throw new InvalidDataException("UserCompany not found.");
            }

            if (userInvite.Role == null)
                _context.UserCompany.Remove(userCompany);
            else
                userCompany.Role = (UserRole)userInvite.Role;

            await _context.SaveChangesAsync();
            return new UserCompanyDto(userCompany);
        }

        /// <summary>
        /// Remove the current user from the selected company
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        public async Task<CompanyDto> LeaveCompany(Guid companyId) {
            User? currentUser = await _utilityService.GetUser();
            if (currentUser == null) throw new InvalidDataException("Invalid User.");

            UserCompany? company = await _context.UserCompany.Include("Company")
                .FirstOrDefaultAsync(uc => uc.CompanyId == companyId && uc.UserId == currentUser.Id);
            if (company == null) throw new InvalidDataException("Invalid CompanyId.");

            _context.UserCompany.Remove(company);
            await _context.SaveChangesAsync();
            return new CompanyDto(company.Company!);
        }

        /// <summary>
        /// Add the user to the project if user and project doesn't have relationship
        /// Remove the user from the project if user is already existing in the project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        public async Task<UserDto> ModifyProject(Guid projectId, String userId) {
            User? currentUser = await _utilityService.GetUser();
            if (currentUser == null) throw new InvalidDataException("Invalid User.");

            Project? project = await _context.Projects.Include("Users").FirstOrDefaultAsync(p => p.Id == projectId);
            if (project == null) throw new InvalidDataException("Invalid ProjectId.");

            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) throw new InvalidDataException("Invalid UserId.");

            if (project.Users!.Contains(user))
                project.Users!.Remove(user);
            else
                project.Users!.Add(user);

            await _context.SaveChangesAsync();
            return new UserDto(user);
        }

        /// <summary>
        /// Get Users by a the project URI
        /// </summary>
        /// <param name="projectUri"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Raised if the request has invalid data (HTTP 400)</exception>
        public async Task<List<UserDto>> GetUsersByProject(String projectUri) {
            Project? project = await _context.Projects.Include("Users").FirstOrDefaultAsync(p => p.Uri == projectUri);
            if (project == null) throw new InvalidDataException("Invalid Project.");
            return project.Users!.Select(user => new UserDto(user)).ToList();
        }
    }
}
