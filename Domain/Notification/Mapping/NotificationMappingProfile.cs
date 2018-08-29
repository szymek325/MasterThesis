using AutoMapper;
using DataLayer.Helpers;
using Domain.Notification.DTO;

namespace Domain.Notification.Mapping
{
    public class NotificationMappingProfile : Profile
    {
        public NotificationMappingProfile()
        {
            CreateMap<DataLayer.Entities.Notification, NotificationOutput>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Message, opts => opts.MapFrom(src => src.Message))
                .ForMember(dest => dest.CreationTime, opts => opts.MapFrom(src => src.CreationTime))
                .ForMember(dest => dest.TypeName, opts => opts.MapFrom(src=>NotificationTypesEnum.SensorReading))
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src=> (int)NotificationTypesEnum.SensorReading))
                .ReverseMap();

            CreateMap<DataLayer.Entities.Movement, NotificationOutput>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Message, opts => opts.MapFrom(src => src.Message))
                .ForMember(dest => dest.CreationTime, opts => opts.MapFrom(src => src.CreationTime))
                .ForMember(dest => dest.TypeName, opts => opts.MapFrom(src => NotificationTypesEnum.Movement))
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => (int)NotificationTypesEnum.SensorReading))
                .ForMember(dest => dest.Thumbnail, opts =>
                    {
                        opts.Condition(src => src.Image != null);
                        opts.MapFrom(src => src.Image.Thumbnail);
                    }
                )
                .ForMember(dest => dest.FileLink, opts =>
                {
                    opts.Condition(src => src.Image != null);
                    opts.MapFrom(src => src.Image);
                })
                .ReverseMap();
        }
    }
}