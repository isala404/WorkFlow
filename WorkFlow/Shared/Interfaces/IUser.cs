using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkFlow.Shared.Dto;

namespace WorkFlow.Shared.Interfaces
{
    public interface IUser
    {
        public Task<UserDto> Get();
        public Task<UserDto?> Get(String id);
        public Task<List<UserDto>> GetUsersByProject(String projectUri);
        public Task<UserDto> Update(UserDto user);
        public Task<bool> Delete();
    }
}