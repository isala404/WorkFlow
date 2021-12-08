using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Client.Services
{
    public class UserService: IUser
    {
        private readonly HttpClient _http;
        
        public UserService(HttpClient http)
        {
            _http = http;
        }

        public async Task<UserDto?> GetUser(string id)
        {
            return await _http.GetFromJsonAsync<UserDto?>($"api/user/{id}/");
        }

        public async Task<List<UserDto>> GetUsersByProject(string projectUri)
        {
            var users = await _http.GetFromJsonAsync<List<UserDto>>($"api/user/project/{projectUri}");
            return users ?? new List<UserDto>();
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