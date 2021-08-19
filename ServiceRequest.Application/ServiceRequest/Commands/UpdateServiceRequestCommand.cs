using MediatR;
using ServiceRequest.Application.Exceptions;
using ServiceRequest.Domain;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceRequest.Application
{
    public class UpdateServiceRequestCommand : IRequest
    {
        public Guid Id { get; set; }
        public string BuildingCode { get; set; }
        public string Description { get; set; }
        public string CurrentStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }

    public class UpdateServiceRequestCommandHandler : IRequestHandler<UpdateServiceRequestCommand>
    {
        private readonly IAppDbContext _context;
        public UpdateServiceRequestCommandHandler(IAppDbContext efContext)
        {
            _context = efContext;
        }
        public async Task<Unit> Handle(UpdateServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ServiceRequestModels.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(ServiceRequestModel), request.Id);

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

            entity.BuildingCode = request.BuildingCode;
            entity.Description = request.Description;
            entity.CreatedBy = request.CreatedBy;
            entity.CreatedDate = request.CreatedDate;
            entity.CurrentStatus = currentStatus;
            entity.LastModifiedBy = request.LastModifiedBy;
            entity.LastModifiedDate = request.LastModifiedDate;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;             
        }
    }
   

}
