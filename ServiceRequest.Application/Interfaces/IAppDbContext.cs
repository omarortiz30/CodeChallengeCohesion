using Microsoft.EntityFrameworkCore;
using ServiceRequest.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceRequest.Application
{
    public interface IAppDbContext
    {
        DbSet<ServiceRequestModel> ServiceRequestModels { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
