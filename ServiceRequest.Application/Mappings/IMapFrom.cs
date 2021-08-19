﻿using AutoMapper;

namespace ServiceRequest.Application.Mappings
{
    public interface IMapFrom<T>
   {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
