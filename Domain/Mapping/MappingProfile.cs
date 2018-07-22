using System.Linq;
using AutoMapper;
using DataLayer.Entities;
using Domain.FaceRecognition.DTO;
using Domain.Files.DTO;
using Domain.People.DTO;
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

            CreateMap<Person, PersonOutput>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Thumbnail, opts => opts.MapFrom(src => src.Images.FirstOrDefault(x => x.Thumbnail != null).Thumbnail))
                .ForMember(dest => dest.FileLinks, opts =>
                {
                    opts.Condition(src=>src.Images!=null);
                    opts.MapFrom(src => src.Images);
                })
                .ReverseMap();
        }
    }
}