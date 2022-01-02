using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WorkFlow.Client.Shared;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Entities;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Client.Services {
    public class NavService : INavService {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ICompany _companyService;
        private readonly TextInfo _myTi = new CultureInfo("en-US", false).TextInfo;
        private readonly IUser _userService;
        private CompanyDto _currentCompany;

        public NavService(
            ICompany companyService,
            IUser userService,
            NavigationManager navigationManager,
            AuthenticationStateProvider authenticationStateProvider) {
            _companyService = companyService;
            _userService = userService;
            NavigationManager = navigationManager;
            _authenticationStateProvider = authenticationStateProvider;

            RestoreSession();
        }

        private NavigationManager NavigationManager { get; }

        public event Action OnChange;

        public Toast? Toast { get; set; }

        public Stack<CompanyDto> CompanyList { get; init; } = new Stack<CompanyDto>();

        public Boolean Fetched { get; set; }

        public UserDto? CurrentUser { get; set; }

        public CompanyDto GetCurrentCompany() {
            return _currentCompany;
        }

        public Boolean IsAdmin(Boolean redirect = false) {
            Boolean isAdmin = CurrentUser != null &&
                              (from userCompany in CurrentUser.UserCompany!
                                  where userCompany.CompanyId == _currentCompany.Id
                                  select userCompany.Role == UserRole.Admin).FirstOrDefault();
            if (!isAdmin && redirect)
            {
                Toast?.AddNotification("Unauthorized", "To access that page Admin role is required", "yellow");
                NavigateToHome(false);
            }

            return isAdmin;
        }

        public void SetCurrentCompany(String newCompanyUri, Boolean reload) {
            if (newCompanyUri == "create/company")
            {
                _currentCompany = CompanyList.Last();
                NavigationManager.NavigateTo("/create/company");
                return;
            }

            String newLocation = NavigationManager.Uri.Replace(_currentCompany.Uri, newCompanyUri);

            try
            {
                _currentCompany = GetCompanyByUri(newCompanyUri);
            }
            catch (NullReferenceException)
            {
                NavigateToHome(false);
            }

            OnChange.Invoke();

            if (reload)
                NavigationManager.NavigateTo(newLocation, true);
        }

        public String CurrentUrl() {
            return NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
        }

        public CompanyDto GetCompanyByUri(String uri) {
            foreach (var companyLink in CompanyList.Where(companyLink => companyLink.Uri == uri)) return companyLink;

            throw new NullReferenceException();
        }

        public String TitleCase(String? text) {
            return text == null ? "" : _myTi.ToTitleCase(text);
        }

        public void NavigateToProjects(Boolean reload) {
            NavigationManager.NavigateTo($"{_currentCompany.Uri}/project", reload);
        }

        public void NavigateToHome(Boolean reload) {
            NavigationManager.NavigateTo("/", reload);
        }

        public void Reload() {
            NavigationManager.NavigateTo(NavigationManager.Uri, true);
        }

        private async void RestoreSession() {
            AuthenticationState authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            ClaimsPrincipal user = authState.User;
            if (user.Identity is not {IsAuthenticated: true})
            {
                Fetched = true;
                NavigationManager.NavigateTo($"Identity/Account/Login?ReturnUrl=/{Uri.EscapeDataString(CurrentUrl())}",
                true);
                return;
            }

            CompanyList.Push(new CompanyDto {Name = "Create New Company", Uri = "create/company"});

            foreach (var company in await _companyService.List()) CompanyList.Push(company);

            _currentCompany = CompanyList.Peek();
            RestoreLastCompany();

            CurrentUser = await _userService.Get();

            Fetched = true;
            OnChange.Invoke();
        }

        private void RestoreLastCompany() {
            String currentPath = CurrentUrl();
            if (currentPath == "create/company")
            {
                _currentCompany = CompanyList.Last();
                return;
            }

            String firstSubPath = currentPath.Split("/").First();

            if (String.IsNullOrEmpty(firstSubPath) || firstSubPath.Equals("user")) return;
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
    }
}
