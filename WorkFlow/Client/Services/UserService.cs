using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Client.Services
{
    public class UserService : IUser
    {
        private readonly HttpClient _http;
        private const string EntityName = "user";

        public UserService(HttpClient http)
        {
            _http = http;
        }

        public async Task<UserDto> Get()
        {
            var user = await _http.GetFromJsonAsync<UserDto>($"api/user/");
            if (user == null) throw new ApplicationException($"Error while getting {EntityName}");
            return user;
        }
        
        public async Task<UserDto> Get(string id)
        {
            var user = await _http.GetFromJsonAsync<UserDto?>($"api/user/{id}/");
            if (user == null) throw new ApplicationException($"Error while getting {EntityName}");
            return user;
        }

        public async Task<List<UserDto>> GetUsersByCompany(Guid companyId)
        {
            var users = await _http.GetFromJsonAsync<List<UserDto>>($"api/user/company/{companyId}/");
            if (users == null) throw new ApplicationException($"Error while getting {EntityName}");
            return users;
        }

        public async Task<UserDto> Update(UserDto user)
        {
            var response = await _http.PutAsJsonAsync($"api/user/", user);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while updating ${EntityName}, Reason: {response.ReasonPhrase}");

            var updatedUser = await response.Content.ReadFromJsonAsync<UserDto>();
            if (updatedUser == null) throw new ApplicationException($"Error while updating ${EntityName}");

            return updatedUser;
        }

        public async Task<bool> Delete()
        {
            var response = await _http.DeleteAsync($"api/user/");
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while deleting ${EntityName}, Reason: {response.ReasonPhrase}");
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<UserCompanyDto> SetUserCompany(UserInvite userInvite)
        {
            var response = await _http.PutAsJsonAsync($"api/user/company/", userInvite);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while updating User Company, Reason: {response.ReasonPhrase}");

            var updateUserCompany = await response.Content.ReadFromJsonAsync<UserCompanyDto>();
            if (updateUserCompany == null) throw new ApplicationException($"Error while updating User Company");

            return updateUserCompany;
        }

        public async Task<CompanyDto> LeaveCompany(Guid companyId)
        {
            var response = await _http.PostAsJsonAsync("api/user/company/", companyId);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while leaving company, Reason: {response.ReasonPhrase}");

            var company = await response.Content.ReadFromJsonAsync<CompanyDto>();
            if (company == null) throw new ApplicationException($"Could not retrieved the ${EntityName}");

            return company;
        }

        public async Task<UserDto> ModifyProject(Guid projectId, String userId)
        {
            var response = await _http.PutAsJsonAsync($"api/user/project/",  new Tuple<Guid, string>(projectId, userId));
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while updating User Project, Reason: {response.ReasonPhrase}");

            var user = await response.Content.ReadFromJsonAsync<UserDto>();
            if (user == null) throw new ApplicationException($"Error while updating User Project");

            return user;
        }

        public Task<bool> RemoveFromProject(string companyId, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto?> GetUser(string id)
        {
            return await _http.GetFromJsonAsync<UserDto?>($"api/user/{id}/");
        }

        public async Task<List<UserDto>> GetUsersByProject(string projectUri)
        {
            var users = await _http.GetFromJsonAsync<List<UserDto>>($"api/user/project/{projectUri}");
            if (users == null) throw new ApplicationException($"Error while getting {EntityName}");
            return users;
        }
    }
}