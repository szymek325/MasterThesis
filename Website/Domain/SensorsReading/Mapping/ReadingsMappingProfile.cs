using AutoMapper;
using Domain.SensorsReading.DTO;

namespace Domain.SensorsReading.Mapping
{
    public class ReadingsMappingProfile : Profile
    {
        public ReadingsMappingProfile()
        {
            CreateMap<DataLayer.Entities.SensorsReading, Reading>()
                .ForMember(dest => dest.CreationTime, opts => opts.MapFrom(src => src.CreationTime.TimeOfDay))
                .ReverseMap();
        }
    }
}