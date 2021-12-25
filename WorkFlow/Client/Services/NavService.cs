using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Linq;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Client.Services
{
    public class NavService : INavService
    {
        private readonly ICompany _companyService;
        private readonly TextInfo _myTi = new CultureInfo("en-US", false).TextInfo;
        [Inject] public NavigationManager? NavigationManager { get; set; }
        public event Action OnChange;

        public NavRoutes[] NavOptions { get; set; } =
        {
            new() {Name = "Home", Href = "/"},
            new() {Name = "Projects", Href = "/project"},
            new() {Name = "Reports", Href = "/report"},
        };

        public Stack<CompanyDto> CompanyList { get; init; } = new();
        private CompanyDto _currentCompany;


        public NavService(ICompany companyService)
        {
            _companyService = companyService;
            CompanyList.Push(new CompanyDto { Name = "Create New Company", Uri = "new" });
            FetchCompanies();
        }

        private async void FetchCompanies()
        {
            foreach (var company in await _companyService.List())
            {
                CompanyList.Push(company);
            }
            if (CompanyList.Count > 1)
            {
                _currentCompany = CompanyList.Peek();
            }
            RestoreLastCompany();
            OnChange.Invoke();
        }

        public CompanyDto GetCurrentCompany()
        {
            return _currentCompany;
        }

        public void SetCurrentCompany(string newCompanyUri, bool reload)
        {
            if (NavigationManager == null)
                return;

            if (newCompanyUri == "new")
            {
                NavigationManager.NavigateTo("/create/company");
                return;
            }
            
            var newLocation = NavigationManager.Uri.Replace(_currentCompany.Uri, newCompanyUri);

            try
            {
                _currentCompany = GetCompanyByUri(newCompanyUri);
            }
            catch (NullReferenceException)
            {
                NavigateToHome(false);
            }
            
            //Task.Run(() => ProtectedLocalStorage.SetAsync("CurrentCompany", _currentCompany)).Wait();
            OnChange.Invoke();
            
            if (reload)
                NavigationManager.NavigateTo(newLocation, true);
        }

        public CompanyDto GetCompanyByUri(string uri)
        {
            foreach (var companyLink in CompanyList.Where(companyLink => companyLink.Uri == uri))
            {
                return companyLink;
            }

            throw new NullReferenceException();
        }
        
        public String TitleCase(string? text)
        {
            return text == null ? "" : _myTi.ToTitleCase(text);
        }

        public void RestoreLastCompany()
        {
            if (NavigationManager == null)
                return;

            var currentPath = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
            var firstSubPath = currentPath.Split("/").First();
            
            if (string.IsNullOrEmpty(firstSubPath) || firstSubPath.Equals("user")) return;
            try
            {
                GetCompanyByUri(firstSubPath);
                SetCurrentCompany(firstSubPath, false);
            }
            catch (NullReferenceException)
            {
                // ignored
            }
        }

        public void NavigateToProjects(bool reload)
        {
            NavigationManager?.NavigateTo($"{_currentCompany.Uri}/project", reload);
        }

        public void NavigateToHome(bool reload)
        {
            NavigationManager?.NavigateTo("/", reload);
        }

        public void Reload()
        {
            NavigationManager?.NavigateTo(NavigationManager.Uri, true);
        }
    }
}