using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Entities;

namespace WorkFlow.Shared.Interfaces
{
    public interface IUser
    {
        public Task<UserDto> Get();
        public Task<UserDto> Update(UserDto user);
        public Task<bool> Delete();
        public Task<UserDto?> GetUser(String id);
        public Task<List<UserDto>> GetUsersByProject(String projectUri);
        public Task<List<UserDto>> GetUsersByCompany(String companyUri);
        public Task<bool> InviteUserToProject(Guid id);
        public Task<bool> InviteUserToCompany(Guid id);
    }
}