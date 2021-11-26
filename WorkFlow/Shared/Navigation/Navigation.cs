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
        public ProtectedLocalStorage ProtectedLocalStorage { get; set; }
        
        public readonly NavRoutes[] NavOptions =
        {
            new NavRoutes { Name = "Home", Href = "/" },
            new NavRoutes { Name = "Projects", Href = "/projects" },
            new NavRoutes { Name = "Reports", Href = "/reports" },
        };
        public List<CompanyLink> CompanyLinks = new();
        private string currentCompany = "C# STOP COMPLAINING YOU LITTLE BITCH";

        private readonly TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

        public Navigation(NavigationManager navigationManager, ProtectedLocalStorage protectedLocalStore)
        {
            this.NavigationManager = navigationManager;
            this.ProtectedLocalStorage = protectedLocalStore;

            // TODO: Fetch this from database
            this.CompanyLinks.Add(new CompanyLink { Name = "Netflix", URI = "netflix" });
            this.CompanyLinks.Add(new CompanyLink { Name = "Cloudflare", URI = "cloudflare" });
            this.CompanyLinks.Add(new CompanyLink { Name = "Iconicto", URI = "iconicto" });
            this.CompanyLinks.Add(new CompanyLink { Name = "Create New Company", URI = "new" });

            // Select the first company in the list by default
            // This value will be updated via the ASP.net Session Storage
            currentCompany = CompanyLinks[0].URI;
        }

        public async Task<string> RestoreLastCompany()
        {
            var result = await this.ProtectedLocalStorage.GetAsync<String>("CurrentCompany");
            if (result.Success && result.Value != null)
            {
                this.currentCompany = result.Value;
            }
            return this.currentCompany;
        }

        public string GetCurrentCompany(bool pretty=false)
        {
            if (pretty)
                return this.myTI.ToTitleCase(this.currentCompany);
            return this.currentCompany;
        }

        public void SetCurrentCompany(string company)
        {
            if (company == "new")
            {
                this.NavigationManager.NavigateTo("/create/company");
                return;
            }
            string newLocation = this.NavigationManager.Uri.Replace(this.currentCompany, company);
            this.currentCompany = company;
            Task.Run(() => this.ProtectedLocalStorage.SetAsync("CurrentCompany", currentCompany)).Wait();
            this.NavigationManager.NavigateTo(newLocation, true);
        }

    }

    public class NavRoutes
    {
        public string Name { get; set; }
        public string Href { get; set; }
        public string HrefWithComapny { get; set; }
        public bool Selected { get; set; }
    }

    public class CompanyLink
    {
        public string Name { get; set; }
        public string URI { get; set; }
    }
}
