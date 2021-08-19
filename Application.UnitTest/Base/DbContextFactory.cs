using Microsoft.EntityFrameworkCore;
using ServiceRequest.Domain;
using ServiceRequest.Persistence;
using System;

namespace Application.UnitTest.Base
{
    public class DbContextFactory
    {
        public static AppDbContext Create()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            context.Database.EnsureCreated();

            context.ServiceRequestModels.AddRange(new[] {
                new ServiceRequestModel { Id = new Guid("b46e8c8c-b2b3-4864-a480-28eec503e9a9"), BuildingCode = "COH", CreatedBy = "Omar Ortiz", CreatedDate = DateTime.UtcNow, CurrentStatus = CurrentStatus.Created, Description = "Test description 1", LastModifiedBy = "Omar Ortiz", LastModifiedDate = DateTime.UtcNow },
                new ServiceRequestModel { BuildingCode = "APL", CreatedBy = "Pedro Santos", CreatedDate = DateTime.UtcNow, CurrentStatus = CurrentStatus.Created, Description = "Test description 2", LastModifiedBy = "Pedro Santos", LastModifiedDate = DateTime.UtcNow },
                new ServiceRequestModel { BuildingCode = "ARF", CreatedBy = "Diego Cavuoti", CreatedDate = DateTime.UtcNow, CurrentStatus = CurrentStatus.Created, Description = "Test description 3", LastModifiedBy = "Diego Cavuoti", LastModifiedDate = DateTime.UtcNow },
            });

            context.SaveChanges();

            return context;
        }

        public static void Destroy(AppDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
