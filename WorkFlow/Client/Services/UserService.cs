using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Client.Services {
    public class UserService : IUser {
        private const String EntityName = "user";
        private readonly HttpClient _http;

        public UserService(HttpClient http) {
            _http = http;
        }

        /// <summary>
        /// Get current user data
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<UserDto> Get() {
            UserDto? user = await _http.GetFromJsonAsync<UserDto>("api/user/");
            if (user == null) throw new ApplicationException($"Error while getting {EntityName}");
            return user;
        }

        /// <summary>
        /// Get a user by user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<UserDto> Get(String id) {
            UserDto? user = await _http.GetFromJsonAsync<UserDto?>($"api/user/{id}/");
            if (user == null) throw new ApplicationException($"Error while getting {EntityName}");
            return user;
        }

        /// <summary>
        /// Get Users by company id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<List<UserDto>> GetUsersByCompany(Guid companyId) {
            List<UserDto>? users = await _http.GetFromJsonAsync<List<UserDto>>($"api/user/company/{companyId}/");
            if (users == null) throw new ApplicationException($"Error while getting {EntityName}");
            return users;
        }

        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<UserDto> Update(UserDto user) {
            HttpResponseMessage response = await _http.PutAsJsonAsync("api/user/", user);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while updating ${EntityName}, Reason: {response.ReasonPhrase}");

            UserDto? updatedUser = await response.Content.ReadFromJsonAsync<UserDto>();
            if (updatedUser == null) throw new ApplicationException($"Error while updating ${EntityName}");

            return updatedUser;
        }

        /// <summary>
        /// Delete current user's account
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<Boolean> Delete() {
            HttpResponseMessage response = await _http.DeleteAsync("api/user/");
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while deleting ${EntityName}, Reason: {response.ReasonPhrase}");
            return await response.Content.ReadFromJsonAsync<Boolean>();
        }

        /// <summary>
        /// Add or Remove a user from a company
        /// Update the user's Role of the company
        /// </summary>
        /// <param name="userInvite"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<UserCompanyDto> SetUserCompany(UserInvite userInvite) {
            HttpResponseMessage response = await _http.PutAsJsonAsync("api/user/company/", userInvite);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while updating User Company, Reason: {response.ReasonPhrase}");

            UserCompanyDto? updateUserCompany = await response.Content.ReadFromJsonAsync<UserCompanyDto>();
            if (updateUserCompany == null) throw new ApplicationException("Error while updating User Company");

            return updateUserCompany;
        }

        /// <summary>
        /// Remove the current user from the selected company
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<CompanyDto> LeaveCompany(Guid companyId) {
            HttpResponseMessage response = await _http.PostAsJsonAsync("api/user/company/", companyId);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while leaving company, Reason: {response.ReasonPhrase}");

            CompanyDto? company = await response.Content.ReadFromJsonAsync<CompanyDto>();
            if (company == null) throw new ApplicationException($"Could not retrieved the ${EntityName}");

            return company;
        }

        /// <summary>
        /// Remove the given user from the given project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<UserDto> ModifyProject(Guid projectId, String userId) {
            HttpResponseMessage response = await _http.PutAsJsonAsync("api/user/project/", new Tuple<Guid, String>(projectId, userId));
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while updating User Project, Reason: {response.ReasonPhrase}");

            UserDto? user = await response.Content.ReadFromJsonAsync<UserDto>();
            if (user == null) throw new ApplicationException("Error while updating User Project");

            return user;
        }

        /// <summary>
        /// Get all project current user is part of 
        /// </summary>
        /// <param name="projectUri"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Raised if there was issue while getting the requested data</exception>
        public async Task<List<UserDto>> GetUsersByProject(String projectUri) {
            List<UserDto>? users = await _http.GetFromJsonAsync<List<UserDto>>($"api/user/project/{projectUri}");
            if (users == null) throw new ApplicationException($"Error while getting {EntityName}");
            return users;
        }
    }
}
