using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Client.Services {
    public class CompanyService : ICompany {
        private const String EntityName = "company";
        private readonly HttpClient _http;

        public CompanyService(HttpClient http) {
            _http = http;
        }

        /// <summary>
        /// Get list of companies for the current user
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<List<CompanyDto>> List() {
            List<CompanyDto>? company = await _http.GetFromJsonAsync<List<CompanyDto>>("api/company/");
            if (company == null) throw new ApplicationException("Error while getting companies");
            return company;
        }

        /// <summary>
        /// Get Company by Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<CompanyDto> Get(Guid companyId) {
            CompanyDto? company = await _http.GetFromJsonAsync<CompanyDto>($"api/company/{companyId}/");
            if (company == null) throw new ApplicationException($"Error while getting {EntityName}");
            return company;
        }

        /// <summary>
        /// Create a new company from the given DTO
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<CompanyDto> Create(CompanyDto company) {
            HttpResponseMessage response = await _http.PostAsJsonAsync("api/company/", company);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while creating {EntityName}, Reason: {response.ReasonPhrase}");

            CompanyDto? newCompany = await response.Content.ReadFromJsonAsync<CompanyDto>();
            if (newCompany == null) throw new ApplicationException($"Could not retrieved the created ${EntityName}");

            return newCompany;
        }

        /// <summary>
        /// Update the selected company
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<CompanyDto> Update(Guid companyId, CompanyDto company) {
            HttpResponseMessage response = await _http.PutAsJsonAsync($"api/company/{companyId}/", company);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while updating ${EntityName}, Reason: {response.ReasonPhrase}");

            CompanyDto? updatedCompany = await response.Content.ReadFromJsonAsync<CompanyDto>();
            if (updatedCompany == null) throw new ApplicationException($"Error while updating ${EntityName}");

            return updatedCompany;
        }

        /// <summary>
        /// Remove the Company from database
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<Boolean> Delete(Guid companyId) {
            HttpResponseMessage response = await _http.DeleteAsync($"api/company/{companyId}/");
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while deleting ${EntityName}, Reason: {response.ReasonPhrase}");
            return await response.Content.ReadFromJsonAsync<Boolean>();
        }

        /// <summary>
        /// Add or Remove a user from the company
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<CompanyDto> ModifyUser(Guid companyId, UserCompanyDto user) {
            HttpResponseMessage response = await _http.PostAsJsonAsync($"api/company/user/{companyId}/", user);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(
                $"Error while modifying ${EntityName} users, Reason: {response.ReasonPhrase}");

            CompanyDto? updatedCompany = await response.Content.ReadFromJsonAsync<CompanyDto>();
            if (updatedCompany == null) throw new ApplicationException($"Error while modifying ${EntityName} users");

            return updatedCompany;
        }
    }
}
