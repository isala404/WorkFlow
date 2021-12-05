using Microsoft.AspNetCore.Components;

namespace WorkFlow.Client.Services
{
    public interface INavService
    {
        public NavigationManager? NavigationManager { get; set; }

        public List<CompanyLink> CompanyLinks { get; init; }
        public NavRoutes[] NavOptions { get; set; }
        public string GetCurrentCompany(bool pretty = false);
        public string TitleCase(string text);
        public void SetCurrentCompany(string company, bool reload = true);
        public void RestoreLastCompany();
    }

    public struct CompanyLink
    {
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

    public struct Breadcrumb
    {
        public string Name { get; init; }
        public string Url { get; init; }
    }
}
