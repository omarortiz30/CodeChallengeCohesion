using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceRequest.Application.ServiceRequest.Queries
{
    public class GetServiceRequestListQuery : IRequest<IList<ServiceRequestDTO>>
    {
        public class GetServiceRequestListHandler : IRequestHandler<GetServiceRequestListQuery, IList<ServiceRequestDTO>>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public GetServiceRequestListHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IList<ServiceRequestDTO>> Handle(GetServiceRequestListQuery request, CancellationToken cancellationToken)
            {
                var serviceRequestList = await _context.ServiceRequestModels
                    .ProjectTo<ServiceRequestDTO>(_mapper.ConfigurationProvider)                    
                    .ToListAsync(cancellationToken);

                return serviceRequestList;
            }
        }
    }
}
