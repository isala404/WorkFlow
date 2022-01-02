using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Client.Services
{
    public class CompanyService : ICompany
    {
        private readonly HttpClient _http;
        private const string EntityName = "company";

        public CompanyService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CompanyDto>> List()
        {
            List<CompanyDto>? company = await _http.GetFromJsonAsync<List<CompanyDto>>("api/company/");
            if (company == null) throw new ApplicationException("Error while getting companies");
            return company;
        }

        public async Task<CompanyDto> Get(Guid companyId)
        {
            CompanyDto? company = await _http.GetFromJsonAsync<CompanyDto>($"api/company/{companyId}/");
            if (company == null) throw new ApplicationException($"Error while getting {EntityName}");
            return company;
        }

        public async Task<CompanyDto> Create(CompanyDto company)
        {
            HttpResponseMessage response = await _http.PostAsJsonAsync("api/company/", company);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while creating {EntityName}, Reason: {response.ReasonPhrase}");

            CompanyDto? newCompany = await response.Content.ReadFromJsonAsync<CompanyDto>();
            if (newCompany == null) throw new ApplicationException($"Could not retrieved the created ${EntityName}");

            return newCompany;
        }

        public async Task<CompanyDto> Update(Guid companyId, CompanyDto company)
        {
            HttpResponseMessage response = await _http.PutAsJsonAsync($"api/company/{companyId}/", company);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while updating ${EntityName}, Reason: {response.ReasonPhrase}");

            CompanyDto? updatedCompany = await response.Content.ReadFromJsonAsync<CompanyDto>();
            if (updatedCompany == null) throw new ApplicationException($"Error while updating ${EntityName}");

            return updatedCompany;
        }

        public async Task<bool> Delete(Guid companyId)
        {
            HttpResponseMessage response = await _http.DeleteAsync($"api/company/{companyId}/");
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error while deleting ${EntityName}, Reason: {response.ReasonPhrase}");
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<CompanyDto> ModifyUser(Guid companyId, UserCompanyDto user)
        {
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