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
    public class CreateServiceRequestCommand : IRequest<Guid>
    {
        public string BuildingCode { get; set; }
        public string Description { get; set; }
        public string CurrentStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public class CreateServiceRequestCommandHandler : IRequestHandler<CreateServiceRequestCommand, Guid>
        {
            private readonly IAppDbContext _context;
            public CreateServiceRequestCommandHandler(IAppDbContext efContext)
            {
                _context = efContext;
            }
            public async Task<Guid> Handle(CreateServiceRequestCommand request, CancellationToken cancellationToken)
            {
                CurrentStatus currentStatus;
                var isCurrentStatusValid = Enum.TryParse(request.CurrentStatus, out currentStatus);
                
                // TODO Validations could be improved by the validation layer
                if (!isCurrentStatusValid)
                    throw new BadRequestException("Current Status is not valid");

                if (string.IsNullOrWhiteSpace(request.BuildingCode) || 
                    string.IsNullOrWhiteSpace(request.Description) || 
                    string.IsNullOrWhiteSpace(request.CreatedBy) ||                     
                    string.IsNullOrWhiteSpace(request.LastModifiedBy) ||
                    request.CreatedDate == DateTime.MinValue ||
                    request.LastModifiedDate == DateTime.MinValue)
                        throw new BadRequestException("Missing mandatory property");

                var entity = new ServiceRequestModel
                {
                    Id = Guid.NewGuid(),
                    BuildingCode = request.BuildingCode,
                    Description = request.Description,
                    CreatedBy = request.CreatedBy,
                    CreatedDate = request.CreatedDate,
                    CurrentStatus = currentStatus,
                    LastModifiedBy = request.LastModifiedBy,
                    LastModifiedDate = request.LastModifiedDate,
                };

                _context.ServiceRequestModels.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }

        }
    }
}
