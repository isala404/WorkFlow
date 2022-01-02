﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Components.Authorization;
using WorkFlow.Client.Shared;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Entities;
using WorkFlow.Shared.Interfaces;

namespace WorkFlow.Client.Services
{
    public class NavService : INavService
    {
        private readonly ICompany _companyService;
        private readonly IUser _userService;
        private readonly TextInfo _myTi = new CultureInfo("en-US", false).TextInfo;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private NavigationManager NavigationManager { get; }
        public event Action OnChange;
        public Toast? Toast { get; set; }
        public Stack<CompanyDto> CompanyList { get; init; } = new();
        public bool Fetched { get; set; }
        private CompanyDto _currentCompany;
        public UserDto? CurrentUser { get; set; }

        public NavService(ICompany companyService, IUser userService, NavigationManager navigationManager,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _companyService = companyService;
            _userService = userService;
            NavigationManager = navigationManager;
            _authenticationStateProvider = authenticationStateProvider;

            RestoreSession();
        }

        private async void RestoreSession()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            if (user.Identity is not {IsAuthenticated: true})
            {
                Fetched = true;
                NavigationManager.NavigateTo($"Identity/Account/Login?ReturnUrl=/{Uri.EscapeDataString(CurrentUrl())}",
                    true);
                return;
            }

            CompanyList.Push(new CompanyDto {Name = "Create New Company", Uri = "create/company"});

            foreach (var company in await _companyService.List())
            {
                CompanyList.Push(company);
            }

            _currentCompany = CompanyList.Peek();
            RestoreLastCompany();

            CurrentUser = await _userService.Get();

            Fetched = true;
            OnChange.Invoke();
        }

        public CompanyDto GetCurrentCompany()
        {
            return _currentCompany;
        }

        public bool IsAdmin(bool redirect = false)
        {
            var isAdmin = CurrentUser != null &&
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

        public void SetCurrentCompany(string newCompanyUri, bool reload)
        {
            if (newCompanyUri == "create/company")
            {
                _currentCompany = CompanyList.Last();
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

            OnChange.Invoke();

            if (reload)
                NavigationManager.NavigateTo(newLocation, true);
        }

        public String CurrentUrl()
        {
            return NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
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

        private void RestoreLastCompany()
        {
            var currentPath = CurrentUrl();
            if (currentPath == "create/company")
            {
                _currentCompany = CompanyList.Last();
                return;
            }

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
            NavigationManager.NavigateTo($"{_currentCompany.Uri}/project", reload);
        }

        public void NavigateToHome(bool reload)
        {
            NavigationManager.NavigateTo("/", reload);
        }

        public void Reload()
        {
            NavigationManager.NavigateTo(NavigationManager.Uri, true);
        }
    }
}