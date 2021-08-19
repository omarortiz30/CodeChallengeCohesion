using MediatR;
using ServiceRequest.Application.Exceptions;
using ServiceRequest.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceRequest.Application.ServiceRequest.Commands
{
    public class DeleteServiceRequestCommand : IRequest
    {
        public Guid Id { get; set; }

        public class DeleteServiceRequestCommandHandler : IRequestHandler<DeleteServiceRequestCommand>
        {
            private readonly IAppDbContext _context;
            public DeleteServiceRequestCommandHandler(IAppDbContext efContext)
            {
                _context = efContext;
            }
            public async Task<Unit> Handle(DeleteServiceRequestCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.ServiceRequestModels.FindAsync(request.Id);

                if (entity == null)
                    throw new NotFoundException(nameof(ServiceRequestModel), request.Id);

                _context.ServiceRequestModels.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
