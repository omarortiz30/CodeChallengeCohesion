using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceRequest.Application.Exceptions;
using ServiceRequest.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceRequest.Application.ServiceRequest.Queries
{
    public class GetServiceRequestQuery : IRequest<ServiceRequestDTO>
    {
        public Guid Id { get; set; }
        public class GetServiceRequestHandler : IRequestHandler<GetServiceRequestQuery, ServiceRequestDTO>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public GetServiceRequestHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ServiceRequestDTO> Handle(GetServiceRequestQuery request, CancellationToken cancellationToken)
            {
                var serviceRequest = await _context.ServiceRequestModels
                    .ProjectTo<ServiceRequestDTO>(_mapper.ConfigurationProvider)                    
                    .FirstOrDefaultAsync(sr => sr.Id == request.Id, cancellationToken);

                if (serviceRequest == null)
                    throw new NotFoundException(nameof(ServiceRequestModel), request.Id);

                return serviceRequest;
            }
        }
    }
}
