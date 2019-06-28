using AutoMapper;
using DataLayer.Entities;
using Domain.Files.DTO;

namespace Domain.Files.Mapping
{
    public class FilesMappingProfile : Profile
    {
        public FilesMappingProfile()
        {
            CreateMap<ImageAttachment, FileLink>()
                .ForMember(dest => dest.FileName, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Url, opts => opts.MapFrom(src => src.Url))
                .ReverseMap();
        }
    }
}