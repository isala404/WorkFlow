using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkFlow.Server.Data;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Entities;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Server.Models
{
    public class UserModel : IUser
    {
        private readonly ApplicationDbContext _context;
        private readonly IUtility _utilityService;
        private readonly UserManager<User> _userManager;

        public UserModel(ApplicationDbContext context, IUtility utilityService, UserManager<User> userManager)
        {
            _context = context;
            _utilityService = utilityService;
            _userManager = userManager;
        }

        public async Task<UserDto> Get()
        {
            var currentUser = await _utilityService.GetUser();
            if (currentUser == null) throw new InvalidDataException("Invalid User.");
            var user = await _context.Users.Include("Companies.Company").SingleAsync(u => u.Id == currentUser.Id);
            return new UserDto(user);
        }

        public async Task<UserDto> Get(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
            if (user == null) throw new InvalidDataException("User not found.");
            return new UserDto(user);
        }

        public async Task<List<UserDto>> GetUsersByCompany(Guid companyId)
        {
            var currentUser = await _utilityService.GetUser();
            if (currentUser == null) throw new InvalidDataException("Invalid User.");
            var companies = await _context.Companies.Include("Users.User").FirstOrDefaultAsync(c => c.Id == companyId);
            if (companies == null) throw new InvalidDataException("Invalid CompanyID.");
            return companies.Users!.Select(user => new UserDto(user.User!)).ToList();
        }

        public async Task<UserDto> Update(UserDto user)
        {
            var currentUser = await _utilityService.GetUser();
            if (currentUser == null) throw new InvalidDataException("Invalid User.");

            var targetUser = await _context.Users.Include("Companies.Company").SingleAsync(u => u.Id == currentUser.Id);
            targetUser.Name = user.Name;
            targetUser.UserName = user.UserName;
            targetUser.Email = user.Email;

            if (user.Password != null)
            {
                targetUser.PasswordHash = _userManager.PasswordHasher.HashPassword(targetUser, user.Password);
            }

            await _context.SaveChangesAsync();
            return new UserDto(targetUser);
        }

        public async Task<bool> Delete()
        {
            var currentUser = await _utilityService.GetUser();
            if (currentUser == null) throw new InvalidDataException("Invalid User.");
            _context.Users.Remove(currentUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserCompanyDto> SetUserCompany(UserInvite userInvite)
        {
            var currentUser = await _utilityService.GetUser();
            if (currentUser == null) throw new InvalidDataException("Invalid User.");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userInvite.Email);
            if (user == null) throw new InvalidDataException("User not found.");
            
            var currentUserCompany = await _context.UserCompany.FirstOrDefaultAsync(u =>
                u.UserId == currentUser.Id && u.CompanyId == userInvite.CompanyId && u.Role == UserRole.Admin);
            if (currentUserCompany == null) throw new UnauthorizedAccessException("User does not have required permission");

            var userCompany =  await _context.UserCompany.FirstOrDefaultAsync(u =>
                u.CompanyId == userInvite.CompanyId && u.UserId == user.Id);

            if (userCompany == null && userInvite.Role != null)
            {
                var newUserCompany = new UserCompany
                {
                    UserId = user.Id,
                    CompanyId = userInvite.CompanyId,
                    Role = (UserRole) userInvite.Role
                };

                var result = await _context.UserCompany.AddAsync(newUserCompany);
                await _context.SaveChangesAsync();
                return new UserCompanyDto(result.Entity);
            }

            if (userCompany == null) throw new InvalidDataException("UserCompany not found.");
            
            if (userInvite.Role == null)
            {
                _context.UserCompany.Remove(userCompany);
            }
            else
            {
                userCompany.Role = (UserRole) userInvite.Role;
            }
            
            await _context.SaveChangesAsync();
            return new UserCompanyDto(userCompany);
        }

        public async Task<UserDto> ModifyProject(Guid projectId, String userId)
        {
            var currentUser = await _utilityService.GetUser();
            if (currentUser == null) throw new InvalidDataException("Invalid User.");
            var project = await _context.Projects.Include("Users").FirstOrDefaultAsync(p => p.Id == projectId);
            if (project == null) throw new InvalidDataException("Invalid ProjectId.");
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) throw new InvalidDataException("Invalid UserId.");

            if (project.Users!.Contains(user))
                project.Users!.Remove(user);                
            else
                project.Users!.Add(user);   

            await _context.SaveChangesAsync();
            return new UserDto(user);
        }
        
        public async Task<List<UserDto>> GetUsersByProject(string projectUri)
        {
            var project = await _context.Projects.Include("Users").FirstOrDefaultAsync(p => p.Uri == projectUri);
            if (project == null) throw new InvalidDataException("Invalid Project.");
            return project.Users!.Select(user => new UserDto(user)).ToList();
        }
    }
}