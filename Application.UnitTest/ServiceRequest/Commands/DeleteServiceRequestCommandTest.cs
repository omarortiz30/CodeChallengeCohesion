using Application.UnitTest.Base;
using ServiceRequest.Application.Exceptions;
using ServiceRequest.Application.ServiceRequest.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static ServiceRequest.Application.ServiceRequest.Commands.DeleteServiceRequestCommand;

namespace Application.UnitTest.ServiceRequest.Commands
{
    public class DeleteServiceRequestCommandTest : CommandBase
    {
        private readonly DeleteServiceRequestCommandHandler _sut;

        public DeleteServiceRequestCommandTest()
            : base()
        {
            _sut = new DeleteServiceRequestCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_GivenInvalidId_ThrowsNotFoundException()
        {
            Guid invalidId = Guid.NewGuid();

            var command = new DeleteServiceRequestCommand { Id = invalidId };

            await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_GivenValidId_DeletesServiceRequest()
        {
            Guid validId = new Guid("b46e8c8c-b2b3-4864-a480-28eec503e9a9");

            var command = new DeleteServiceRequestCommand { Id = validId };

            await _sut.Handle(command, CancellationToken.None);

            var serviceRequest = await _context.ServiceRequestModels.FindAsync(validId);

            Assert.Null(serviceRequest);
        }
    }
}
