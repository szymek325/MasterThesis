using AutoMapper;
using DataLayer.Entities;
using Domain.Files.DTO;
using Domain.SensorsReading.DTO;

namespace Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DataLayer.Entities.SensorsReading, Reading>().ReverseMap();

            CreateMap<ImageAttachment, FileLink>()
                .ForMember(dest => dest.FileName, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Url, opts => opts.MapFrom(src => src.Url));
        }
    }
}