using System;
using System.Collections.Generic;
using WorkFlow.Client.Shared;
using WorkFlow.Shared.Dto;

namespace WorkFlow.Client.Services {
    public interface INavService {
        public Toast? Toast { get; set; }

        public UserDto? CurrentUser { get; }

        public Boolean Fetched { get; }

        public Stack<CompanyDto> CompanyList { get; }

        public event Action OnChange;

        public CompanyDto GetCurrentCompany();
        public Boolean IsAdmin(Boolean redirect = false);
        public String TitleCase(String text);
        public void SetCurrentCompany(String newCompanyUri, Boolean reload);
        public String CurrentUrl();
        public CompanyDto GetCompanyByUri(String uri);
        public void NavigateToProjects(Boolean reload);
        public void NavigateToHome(Boolean reload);
        public void Reload();
    }

    public struct NavRoutes {
        public String Name { get; init; }

        public String Href { get; init; }

        public String HrefWithCompany { get; set; }

        public Boolean Selected { get; set; }

        public Boolean AdminOnly { get; set; }
    }

    public class Notification : IEquatable<Notification> {
        public String? Title { get; init; }

        public String Message { get; init; }

        public String Color { get; init; }

        public Boolean Equals(Notification? other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Title == other.Title && Message == other.Message && Color == other.Color;
        }
    }

    public readonly struct Breadcrumb {
        public String Name { get; init; }

        public String Url { get; init; }
    }
}
