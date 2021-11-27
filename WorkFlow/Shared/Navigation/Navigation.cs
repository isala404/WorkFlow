using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace WorkFlow.Shared.Navigation
{
    public class Navigation : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private ProtectedLocalStorage ProtectedLocalStorage { get; set; }
        
        public readonly NavRoutes[] NavOptions =
        {
            new NavRoutes { Name = "Home", Href = "/" },
            new NavRoutes { Name = "Projects", Href = "/projects" },
            new NavRoutes { Name = "Reports", Href = "/reports" },
        };
        public readonly List<CompanyLink> CompanyLinks = new();
        private string _currentCompany;

        private readonly TextInfo _myTi = new CultureInfo("en-US", false).TextInfo;

        public Navigation(NavigationManager navigationManager, ProtectedLocalStorage protectedLocalStore)
        {
            this.NavigationManager = navigationManager;
            this.ProtectedLocalStorage = protectedLocalStore;

            // TODO: Fetch this from database
            this.CompanyLinks.Add(new CompanyLink { Name = "Netflix", Uri = "netflix" });
            this.CompanyLinks.Add(new CompanyLink { Name = "Cloudflare", Uri = "cloudflare" });
            this.CompanyLinks.Add(new CompanyLink { Name = "Iconicto", Uri = "iconicto" });
            this.CompanyLinks.Add(new CompanyLink { Name = "Create New Company", Uri = "new" });

            // Select the first company in the list by default
            // This value will be updated via the ASP.net Session Storage
            _currentCompany = CompanyLinks[0].Uri;
        }

        public async Task<string> RestoreLastCompany()
        {
            var result = await this.ProtectedLocalStorage.GetAsync<String>("CurrentCompany");
            if (result.Success && result.Value != null)
            {
                this._currentCompany = result.Value;
            }
            return this._currentCompany;
        }

        public string GetCurrentCompany(bool pretty=false)
        {
            if (pretty)
                return this._myTi.ToTitleCase(this._currentCompany);
            return this._currentCompany;
        }

        public void SetCurrentCompany(string company)
        {
            if (company == "new")
            {
                this.NavigationManager.NavigateTo("/create/company");
                return;
            }
            var newLocation = this.NavigationManager.Uri.Replace(this._currentCompany, company);
            this._currentCompany = company;
            Task.Run(() => this.ProtectedLocalStorage.SetAsync("CurrentCompany", _currentCompany)).Wait();
            this.NavigationManager.NavigateTo(newLocation, true);
        }

    }

    public class NavRoutes
    {
        public string Name { get; init; }
        public string Href { get; init; }
        public string HrefWithCompany { get; set; }
        public bool Selected { get; set; }
    }

    public class CompanyLink
    {
        public string Name { get; init; }
        public string Uri { get; init; }
    }
}
