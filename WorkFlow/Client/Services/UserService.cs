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

        public async Task<UserDto> Get()
        {
            var user = await _http.GetFromJsonAsync<UserDto>($"api/user/");
            if (user == null) throw new ApplicationException("Error while getting ticket");
            return user;
        }

        public async Task<UserDto> Update(UserDto user)
        {
            var response = await _http.PutAsJsonAsync($"api/user/", user);
            if (!response.IsSuccessStatusCode) throw new ApplicationException($"Operation Failed, Reason: {response.ReasonPhrase}");
            
            var updatedUser = await response.Content.ReadFromJsonAsync<UserDto>();
            if (updatedUser == null) throw new ApplicationException("Error while updating project");
            
            return updatedUser;
        }

        public async Task<bool> Delete()
        {
            var response = await _http.DeleteAsync($"api/user/");
            if (!response.IsSuccessStatusCode) throw new ApplicationException($"Operation Failed, Reason: {response.ReasonPhrase}");
            return await response.Content.ReadFromJsonAsync<bool>();
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