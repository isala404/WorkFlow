using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using WorkFlow.Shared.Dto;

namespace WorkFlow.Client.Services
{
    public interface INavService
    {
        public NavigationManager? NavigationManager { get; set; }
        public event Action OnChange;
        public Stack<CompanyLink> CompanyLinks { get; init; }
        public NavRoutes[] NavOptions { get; set; }
        public CompanyLink GetCurrentCompany();
        public String TitleCase(string text);
        public void SetCurrentCompany(string newCompanyUri, bool reload);
        public void RestoreLastCompany();
        public CompanyLink GetCompanyByUri(string uri);
        public void NavigateToProjects();
        public void NavigateToProject();

        public void NavigateToHome(bool reload);
        public void Reload();
        
    }

    public readonly struct CompanyLink
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Uri { get; init; }
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
