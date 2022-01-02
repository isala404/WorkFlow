using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WorkFlow.Shared.Entities;

namespace WorkFlow.Server.Data {
    public class ApplicationDbContext : ApiAuthorizationDbContext<User> {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions) {
        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<UserCompany> UserCompany { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<Company>().HasIndex(e => e.Uri).IsUnique();
            builder.Entity<Project>().HasIndex(e => e.Uri).IsUnique();
            builder.Entity<UserCompany>().HasIndex(p => new {p.UserId, p.CompanyId}).IsUnique();
        }
    }
}
