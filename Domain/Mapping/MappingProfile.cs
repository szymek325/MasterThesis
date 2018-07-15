using System.Linq;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.Entities.Common;
using Domain.FaceDetection.DTO;
using Domain.FaceRecognition.DTO;
using Domain.Files.DTO;
using Domain.NeuralNetwork.DTO;
using Domain.People.DTO;
using Domain.SensorsReading.DTO;

namespace Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Detection, DetectionRequest>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.DnnFaces, opts => opts.MapFrom(src => src.DnnFaces))
                .ForMember(dest => dest.HaarFaces, opts => opts.MapFrom(src => src.HaarFaces))
                .ForMember(dest => dest.CreationTime, opts => opts.MapFrom(src => src.CreationTime))
                .ForMember(dest => dest.CompletionTime, opts => opts.MapFrom(src => src.CompletionTime))
                .ForMember(dest => dest.Thumbnail,
                    opts => opts.MapFrom(src => src.Images.FirstOrDefault(x => x.Thumbnail != null).Thumbnail))
                .ForMember(dest => dest.FileLinks, opts => opts.MapFrom(src => src.Images));

            CreateMap<Recognition, RecognitionRequest>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.CreationTime, opts => opts.MapFrom(src => src.CreationTime))
                .ForMember(dest => dest.CompletionTime, opts => opts.MapFrom(src => src.CompletionTime))
                .ForMember(dest => dest.Thumbnail,
                    opts => opts.MapFrom(src => src.Images.FirstOrDefault(x => x.Thumbnail != null).Thumbnail))
                .ForMember(dest => dest.FileLinks, opts => opts.MapFrom(src => src.Images))
                .ForMember(dest => dest.NeuralNetwork, opts => opts.MapFrom(src => src.NeuralNetwork));

            CreateMap<DataLayer.Entities.SensorsReading, Reading>().ReverseMap();

            CreateMap<IImage, FileLink>()
                .ForMember(dest => dest.FileName, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Url, opts => opts.MapFrom(src => src.Url));

            CreateMap<Person, PersonOutput>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Thumbnail,
                    opts => opts.MapFrom(src => src.Images.FirstOrDefault(x => x.Thumbnail != null).Thumbnail))
                .ForMember(dest => dest.FileLinks, opts => opts.MapFrom(src => src.Images))
                .ReverseMap();

            CreateMap<NeuralNetworkFile, NeuralNetworkFileOutput>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.TypeName, opts => opts.MapFrom(src => src.NeuralNetworkType.Name))
                .ReverseMap();

            CreateMap<RecognitionResult, RecognitionResultOutput>()
                .ForMember(dest => dest.IdentifiedPersonId, opts => opts.MapFrom(src => src.IdentifiedPersonId))
                .ForMember(dest => dest.Confidence, opts => opts.MapFrom(src => src.Confidence))
                .ForMember(dest => dest.NeuralNetworkFileName, opts => opts.MapFrom(src => src.NeuralNetworkFile.Name))
                .ForMember(dest => dest.NeuralNetworkTypeName, opts => opts.MapFrom(src => src.NeuralNetworkFile.NeuralNetworkType.Name))
                .ReverseMap();

            CreateMap<DataLayer.Entities.NeuralNetwork, NeuralNetworkRequest>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.People, opts => opts.MapFrom(src => src.People))
                .ForMember(dest => dest.CreationTime, opts => opts.MapFrom(src => src.CreationTime))
                .ForMember(dest => dest.CompletionTime, opts => opts.MapFrom(src => src.CompletionTime))
                .ForMember(dest => dest.Files, opts => opts.MapFrom(src => src.Files))
                .ReverseMap();
        }
    }
}