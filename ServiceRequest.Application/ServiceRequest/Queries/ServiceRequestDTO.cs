using ServiceRequest.Domain;
using AutoMapper;
using System;
using ServiceRequest.Application.Mappings;

namespace ServiceRequest.Application.ServiceRequest.Queries
{
    public class ServiceRequestDTO : IMapFrom<ServiceRequestModel>
    {
        public Guid Id { get; set; }
        public string BuildingCode { get; set; }
        public string Description { get; set; }
        public string CurrentStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ServiceRequestModel, ServiceRequestDTO>();                
        }
    }
}
