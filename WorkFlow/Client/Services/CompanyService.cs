using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Client.Services
{
    public class CompanyService: ICompany
    {
        private readonly HttpClient _http;

        public CompanyService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CompanyDto>> List()
        {
            var company = await _http.GetFromJsonAsync<List<CompanyDto>>("api/company/");
            return company ?? new List<CompanyDto>();
        }

        public async Task<CompanyDto> Get(Guid companyId)
        {
            var company = await _http.GetFromJsonAsync<CompanyDto>($"api/company/{companyId}/");
            if (company == null) throw new ApplicationException("Error while creating company");
            return company;
        }

        public async Task<CompanyDto> Create(CompanyDto company)
        {
            var response = await _http.PostAsJsonAsync("api/company/", company);
            if (!response.IsSuccessStatusCode) throw new ApplicationException($"Operation Failed, Reason: {response.ReasonPhrase}");
            
            var newCompany = await response.Content.ReadFromJsonAsync<CompanyDto>();
            if (newCompany == null) throw new ApplicationException("Error while creating company");
            
            return newCompany;
        }

        public async Task<CompanyDto> Update(Guid companyId, CompanyDto company)
        {
            var response = await _http.PutAsJsonAsync($"api/company/{companyId}/", company);
            if (!response.IsSuccessStatusCode) throw new ApplicationException($"Operation Failed, Reason: {response.ReasonPhrase}");
            
            var updatedCompany = await response.Content.ReadFromJsonAsync<CompanyDto>();
            if (updatedCompany == null) throw new ApplicationException("Error while updating company");
            
            return updatedCompany;
        }

        public async Task<bool> Delete(Guid companyId)
        {
            var response = await _http.DeleteAsync($"api/company/{companyId}/");
            if (!response.IsSuccessStatusCode) throw new ApplicationException($"Operation Failed, Reason: {response.ReasonPhrase}");
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<CompanyDto> ModifyUser(Guid companyId, UserCompanyDto user)
        {
            var response = await _http.PostAsJsonAsync($"api/company/{companyId}/", user);
            if (!response.IsSuccessStatusCode) throw new ApplicationException($"Operation Failed, Reason: {response.ReasonPhrase}");
            
            var updatedCompany = await response.Content.ReadFromJsonAsync<CompanyDto>();
            if (updatedCompany == null) throw new ApplicationException("Error while modifying user from company");
            
            return updatedCompany;
        }
    }
}