using AutoMapper;
using EstateManager.Domain.Entities;
using EstateManager.Application.DTOs;

namespace EstateManager.Application.Mappings
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {

            CreateMap<CreatePropertyDto, Property>();
            CreateMap<UpdatePropertyDto, Property>();
            CreateMap<Property, PropertyDto>();
            CreateMap<PropertyImage, PropertyImageDto>();
            CreateMap<PropertyTrace, PropertyTraceDto>();
            CreateMap<PropertyTraceDto, PropertyTrace>();
        }

        
    }
}
