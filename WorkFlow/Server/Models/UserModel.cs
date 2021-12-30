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

        public async Task<UserDto?> Get(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
            return user == null ? null : new UserDto(user);
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

        public async Task<List<UserDto>> GetUsersByProject(string projectUri)
        {
            return await _context.Users.Select(user => new UserDto(user)).ToListAsync();
        }
    }
}