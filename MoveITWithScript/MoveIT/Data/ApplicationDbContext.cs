using MoveIT.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MoveITWeb.Models;

namespace MoveIT.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {

        public DbSet<RelocationOffer> RelocationOffers { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<RelocationOfferReference> RelocationOfferReferences { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
            
        }
    }
}
