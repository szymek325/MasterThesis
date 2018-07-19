using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DataLayer.Entities;
using Domain.FaceDetection.DTO;

namespace Domain.FaceDetection.Mapping
{
    public class DetectionMappingProfile : Profile
    {
        public DetectionMappingProfile()
        {
            CreateMap<DetectionResult, DetectionResultOutput>()
                .ForMember(dest => dest.StartX, opts => opts.MapFrom(src => src.StartX))
                .ForMember(dest => dest.StartY, opts => opts.MapFrom(src => src.StartY))
                .ForMember(dest => dest.EndX, opts => opts.MapFrom(src => src.EndX))
                .ForMember(dest => dest.EndY, opts => opts.MapFrom(src => src.EndY))
                .ForMember(dest => dest.DetectionTypeName, opts => opts.MapFrom(src => src.DetectionType.Name))
                .ForMember(dest => dest.Image, opts => opts.MapFrom(src => src.Image))
                .ReverseMap();
        }
    }
}
