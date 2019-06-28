using System.Linq;
using AutoMapper;
using DataLayer.Entities;
using Domain.NeuralNetwork.DTO;

namespace Domain.NeuralNetwork.Mapping
{
    public class NeuralNetworkMappingProfile:Profile
    {
        public NeuralNetworkMappingProfile()
        {
            CreateMap<DataLayer.Entities.NeuralNetwork, NeuralNetworkBaseInfoOutput>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<NeuralNetworkFile, NeuralNetworkFileOutput>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.TypeName, opts => opts.MapFrom(src => src.NeuralNetworkType.Name))
                .ForMember(dest => dest.ProcessingTime, opts => opts.MapFrom(src => src.ProcessingTime))
                .ForMember(dest => dest.TrainingTime, opts => opts.MapFrom(src => src.TrainingTime))
                .ForMember(dest => dest.FileSize, opts => opts.MapFrom(src => src.FileSize))
                .ReverseMap();

            CreateMap<DataLayer.Entities.NeuralNetwork, AllNeuralNetworksOutput>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.StatusName, opts => opts.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.CreationTime, opts => opts.MapFrom(src => src.CreationTime))
                .ForMember(dest => dest.CompletionTime, opts => opts.MapFrom(src => src.CompletionTime))
                .ForMember(dest => dest.PeopleCount, opts => opts.MapFrom(src => src.People.Count()))
                .ForMember(dest => dest.FilesCount, opts => opts.MapFrom(src => src.Files.Count()))
                .ReverseMap();

            CreateMap<DataLayer.Entities.NeuralNetwork, NeuralNetworkRequest>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.People, opts => opts.MapFrom(src => src.People))
                .ForMember(dest => dest.CreationTime, opts => opts.MapFrom(src => src.CreationTime))
                .ForMember(dest => dest.CompletionTime, opts => opts.MapFrom(src => src.CompletionTime))
                .ForMember(dest => dest.Files, opts => opts.MapFrom(src => src.Files))
                .ForMember(dest => dest.PhotosPerPerson, opts => opts.MapFrom(src => src.MaxNumberOfPhotosPerPerson))
                .ReverseMap();
        }
    }
}
