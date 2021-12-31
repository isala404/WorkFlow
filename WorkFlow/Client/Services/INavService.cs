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
    public class Notification : IEquatable<Notification>
    {
        public string? Title { get; init; }
        public string Message { get; init; }
        public string Color { get; init; }
        public bool Equals(Notification? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Title == other.Title && Message == other.Message && Color == other.Color;
        }
    }

    public readonly struct Breadcrumb
    {
        public string Name { get; init; }
        public string Url { get; init; }
    }
}
