using AutoMapper;
using Domain.Files.DTO;
using Microsoft.AspNetCore.Http;

namespace Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IFormFile, FileToUpload>()
                .ForMember(dest => dest.FileName, opts => opts.MapFrom(src => src.FileName))
                .ForMember(dest => dest.FileStream, opts => opts.MapFrom(src => src.OpenReadStream()))
                .ReverseMap();
        }
    }
}