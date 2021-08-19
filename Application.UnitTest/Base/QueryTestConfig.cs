using AutoMapper;
using ServiceRequest.Application.Mappings;
using ServiceRequest.Persistence;
using System;
using Xunit;

namespace Application.UnitTest.Base
{
    public class QueryTestConfig : IDisposable
    {
        public AppDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryTestConfig()
        {
            Context = DbContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            DbContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestConfig> { }
}
