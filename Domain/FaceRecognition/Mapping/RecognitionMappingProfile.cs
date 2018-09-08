using AutoMapper;
using DataLayer.Entities;
using Domain.FaceRecognition.DTO;

namespace Domain.FaceRecognition.Mapping
{
    public class RecognitionMappingProfile : Profile
    {
        public RecognitionMappingProfile()
        {
            CreateMap<RecognitionResult, RecognitionResultOutput>()
                .ForMember(dest => dest.IdentifiedPersonId, opts => opts.MapFrom(src => src.IdentifiedPersonId))
                .ForMember(dest => dest.Confidence, opts => opts.MapFrom(src => src.Confidence))
                .ForMember(dest => dest.NeuralNetworkFileName, opts => opts.MapFrom(src => src.NeuralNetworkFile.Name))
                .ForMember(dest => dest.Comments, opts => opts.MapFrom(src => src.Comments))
                .ForMember(dest => dest.ProcessingTime, opts => opts.MapFrom(src => src.ProcessingTime))
                .ForMember(dest => dest.NeuralNetworkTypeName,
                    opts => opts.MapFrom(src => src.NeuralNetworkFile.NeuralNetworkType.Name))
                .ReverseMap();

            CreateMap<Recognition, RecognitionRequest>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.CreationTime, opts => opts.MapFrom(src => src.CreationTime))
                .ForMember(dest => dest.CompletionTime, opts => opts.MapFrom(src => src.CompletionTime))
                .ForMember(dest => dest.Thumbnail,
                    opts => opts.MapFrom(src => src.Image.Thumbnail))
                .ForMember(dest => dest.FileLink, opts => opts.MapFrom(src => src.Image))
                .ForMember(dest => dest.Results, opts => opts.MapFrom(src => src.RecognitionResults));
        }
    }
}