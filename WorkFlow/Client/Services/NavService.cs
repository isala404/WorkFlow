using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace WorkFlow.Client.Services
{
    public class NavService : INavService
    {
        private readonly HttpClient _http;
        private readonly TextInfo MyTi = new CultureInfo("en-US", false).TextInfo;
        [Inject] public NavigationManager? NavigationManager { get; set; }
        public NavRoutes[] NavOptions { get; set; } =
        {
            new NavRoutes {Name = "Home", Href = "/"},
            new NavRoutes {Name = "Projects", Href = "/project"},
            new NavRoutes {Name = "Reports", Href = "/report"},
        };
        public List<CompanyLink> CompanyLinks {get; init;} = new List<CompanyLink>();
        private string _currentCompany = "C# STOP COMPLAINING BUT USELESS THINGS";


        public NavService(HttpClient http)
        {
            _http = http;
            // TODO: Fetch this from database
            CompanyLinks.Add(new CompanyLink { Name = "Netflix", Uri = "netflix" });
            CompanyLinks.Add(new CompanyLink { Name = "Cloudflare", Uri = "cloudflare" });
            CompanyLinks.Add(new CompanyLink { Name = "Iconicto", Uri = "iconicto" });
            CompanyLinks.Add(new CompanyLink { Name = "Create New Company", Uri = "new" });

            if (CompanyLinks.Count > 0)
            {
                _currentCompany = CompanyLinks[0].Uri;
            }
        }

        public string GetCurrentCompany(bool pretty = false)
        {
            return pretty ? MyTi.ToTitleCase(_currentCompany) : _currentCompany;
        }

        public void SetCurrentCompany(string company, bool reload = true)
        {
            if (NavigationManager == null)
                return;

            if (company == "new")
            {
                NavigationManager.NavigateTo("/create/company");
                return;
            }

            var newLocation = NavigationManager.Uri.Replace(_currentCompany, company);
            _currentCompany = company;
            //Task.Run(() => ProtectedLocalStorage.SetAsync("CurrentCompany", _currentCompany)).Wait();

            if (reload)
                NavigationManager.NavigateTo(newLocation, true);
        }

        public string TitleCase(string text)
        {
            return MyTi.ToTitleCase(text);
        }

        public void RestoreLastCompany()
        {
            if (NavigationManager == null)
                return;

            var currentPath = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
            var firstSubPath = currentPath.Split("/").First();

            // Select the first company in the list by default
            // This value will be updated via the ASP.net Session Storage
            if (!string.IsNullOrEmpty(firstSubPath) && !firstSubPath.Equals("user"))
                SetCurrentCompany(firstSubPath, false);
   
        }
    }
}
