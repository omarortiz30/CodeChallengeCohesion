using Microsoft.EntityFrameworkCore;
using ServiceRequest.Application;
using ServiceRequest.Domain;
using ServiceRequest.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceRequest.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ServiceRequestModel> ServiceRequestModels { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
   
        }

    }
}
