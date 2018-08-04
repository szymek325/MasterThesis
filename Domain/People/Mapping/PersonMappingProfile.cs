using System.Linq;
using AutoMapper;
using DataLayer.Entities;
using Domain.People.DTO;

namespace Domain.People.Mapping
{
    public class PersonMappingProfile : Profile
    {
        public PersonMappingProfile()
        {
            CreateMap<Person, PersonOutput>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Thumbnail,
                    opts => opts.MapFrom(src => src.Images.FirstOrDefault(x => x.Thumbnail != null).Thumbnail))
                .ForMember(dest => dest.FileLinks, opts =>
                {
                    opts.Condition(src => src.Images != null);
                    opts.MapFrom(src => src.Images);
                })
                .ReverseMap();

            CreateMap<Person, PersonAsCheckbox>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsChecked, opts => opts.Ignore())
                .ReverseMap();
        }
    }
}