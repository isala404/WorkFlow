using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using WorkFlow.Shared.Dto;

namespace WorkFlow.Client.Services
{
    public interface INavService
    {
        public event Action OnChange;
        public UserDto? CurrentUser  { get; }
        public bool Fetched { get; }
        public Stack<CompanyDto> CompanyList { get; }
        public CompanyDto GetCurrentCompany();
        public String TitleCase(string text);
        public void SetCurrentCompany(string newCompanyUri, bool reload);
        public string CurrentUrl();
        public CompanyDto GetCompanyByUri(string uri);
        public void NavigateToProjects(bool reload);
        public void NavigateToHome(bool reload);
        public void Reload();
        
    }
    
    public struct NavRoutes
    {
        public string Name { get; init; }
        public string Href { get; init; }
        public string HrefWithCompany { get; set; }
        public bool Selected { get; set; }
    }

    public readonly struct Breadcrumb
    {
        public string Name { get; init; }
        public string Url { get; init; }
    }
}
