using Application.UnitTest.Base;
using AutoMapper;
using ServiceRequest.Application.ServiceRequest.Queries;
using ServiceRequest.Persistence;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static ServiceRequest.Application.ServiceRequest.Queries.GetServiceRequestQuery;

namespace Application.UnitTest.ServiceRequest.Queries
{
    [Collection("QueryCollection")]
    public class GetServiceRequestQueryHandlerTest
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GetServiceRequestQueryHandlerTest(QueryTestConfig config)
        {
            _context = config.Context;
            _mapper = config.Mapper;
        }

        [Fact]
        public async Task GetServiceRequestById()
        {
            var sut = new GetServiceRequestHandler(_context, _mapper);

            var result = await sut.Handle(new GetServiceRequestQuery { Id = new Guid("b46e8c8c-b2b3-4864-a480-28eec503e9a9") }, CancellationToken.None);

            result.ShouldBeOfType<ServiceRequestDTO>();
            result.CreatedBy.ShouldBe("Omar Ortiz");
        }
    }
}
