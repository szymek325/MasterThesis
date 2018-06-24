using System.Linq;
using AutoMapper;
using DataLayer.Entities;
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
            CreateMap<DataLayer.Entities.FaceDetection, FaceDetectionRequest>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.DnnFaces, opts => opts.MapFrom(src => src.DnnFaces))
                .ForMember(dest => dest.HaarFaces, opts => opts.MapFrom(src => src.HaarFaces))
                .ForMember(dest => dest.CreationTime, opts => opts.MapFrom(src => src.CreationTime))
                .ForMember(dest => dest.Thumbnail, opts => opts.MapFrom(src => src.Files.FirstOrDefault(x => x.Thumbnail != null).Thumbnail))
                .ForMember(dest => dest.FileLinks, opts => opts.MapFrom(src => src.Files));

            CreateMap<DataLayer.Entities.FaceRecognition, FaceRecoRequest>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.CreationTime, opts => opts.MapFrom(src => src.CreationTime))
                .ForMember(dest => dest.Thumbnail, opts => opts.MapFrom(src => src.Files.FirstOrDefault(x => x.Thumbnail != null).Thumbnail))
                .ForMember(dest => dest.FileLinks, opts => opts.MapFrom(src => src.Files))
                .ForMember(dest => dest.NeuralNetwork, opts => opts.MapFrom(src => src.NeuralNetwork));

            CreateMap<DataLayer.Entities.SensorsReading, Reading>().ReverseMap();

            CreateMap<File, FileLink>()
                .ForMember(dest => dest.FileName, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Url, opts => opts.MapFrom(src => src.Url));

            CreateMap<Person, PersonOutput>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Thumbnail, opts => opts.MapFrom(src => src.Files.FirstOrDefault(x=>x.Thumbnail!=null).Thumbnail))
                .ForMember(dest => dest.FileLinks, opts => opts.MapFrom(src => src.Files))
                .ReverseMap();

            CreateMap<DataLayer.Entities.NeuralNetwork, NeuralNetworkOutput>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.People, opts => opts.MapFrom(src => src.People))
                .ReverseMap();
        }
    }
}