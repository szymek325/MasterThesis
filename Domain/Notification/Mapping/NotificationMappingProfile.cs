using AutoMapper;
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