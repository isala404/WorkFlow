using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WorkFlow.Shared.Entities;

namespace WorkFlow.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<User>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        DbSet<Company> Companies { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<Ticket> Tickets { get; set; }
        DbSet<UserCompany> UserCompanies { get; set; }
    }
}