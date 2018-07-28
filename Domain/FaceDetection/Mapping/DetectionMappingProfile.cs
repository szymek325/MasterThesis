using AutoMapper;
using DataLayer.Entities;
using Domain.FaceDetection.DTO;

namespace Domain.FaceDetection.Mapping
{
    public class DetectionMappingProfile : Profile
    {
        public DetectionMappingProfile()
        {
            CreateMap<Detection, DetectionRequest>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.DnnFaces, opts => opts.MapFrom(src => src.DnnFaces))
                .ForMember(dest => dest.HaarFaces, opts => opts.MapFrom(src => src.HaarFaces))
                .ForMember(dest => dest.CreationTime, opts => opts.MapFrom(src => src.CreationTime))
                .ForMember(dest => dest.CompletionTime, opts => opts.MapFrom(src => src.CompletionTime))
                .ForMember(dest => dest.Thumbnail, opts => opts.MapFrom(src => src.Image.Thumbnail))
                .ForMember(dest => dest.FileLink, opts => opts.MapFrom(src => src.Image))
                .ReverseMap();

            CreateMap<DetectionResult, DetectionResultOutput>()
                //.ForMember(dest => dest.StartX, opts => opts.MapFrom(src => src.StartX))
                //.ForMember(dest => dest.StartY, opts => opts.MapFrom(src => src.StartY))
                //.ForMember(dest => dest.EndX, opts => opts.MapFrom(src => src.EndX))
                //.ForMember(dest => dest.EndY, opts => opts.MapFrom(src => src.EndY))
                .ForMember(dest => dest.DetectionTypeName, opts => opts.MapFrom(src => src.DetectionType.Name))
                .ForMember(dest => dest.Image, opts => opts.MapFrom(src => src.Image))
                .ReverseMap();
        }
    }
}
