using AutoMapper;
using Domain.FaceDetection.DTO;

namespace Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DataLayer.Entities.FaceDetection, FaceDetectionRequest>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.DnnFaces, opts => opts.MapFrom(src => src.DnnFaces))
                .ForMember(dest => dest.HaarFaces, opts => opts.MapFrom(src => src.HaarFaces));
        }
    }
}