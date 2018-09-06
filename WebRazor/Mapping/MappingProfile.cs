using AutoMapper;
using Domain.Files.DTO;
using Domain.NeuralNetwork.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebRazor.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IFormFile, FileToUpload>()
                .ForMember(dest => dest.FileName, opts => opts.MapFrom(src => src.FileName))
                .ForMember(dest => dest.FileStream, opts => opts.MapFrom(src => src.OpenReadStream()))
                .ReverseMap();

            CreateMap<NeuralNetworkBaseInfoOutput, SelectListItem>()
                .ForMember(dest => dest.Text, opts => opts.MapFrom(src => $"{src.Id} {src.Name}"))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}