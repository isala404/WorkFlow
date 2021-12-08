using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkFlow.Server.Data;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Server.Models
{
    public class UserModel: IUser
    {
        private readonly ApplicationDbContext _context;

        public UserModel(ApplicationDbContext context)
        {
            _context = context;
        } 

        public async Task<UserDto?> GetUser(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
            return user == null ? null : new UserDto(user);
        }

        public async Task<List<UserDto>> GetUsersByProject(string projectUri)
        {
            return await _context.Users.Select(user => new UserDto(user)).ToListAsync();
        }

        public async Task<List<UserDto>> GetUsersByCompany(string companyUri)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InviteUserToProject(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InviteUserToCompany(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}