using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Domain.Notification.DTO;
using Domain.NotificationSettings.DTO;

namespace Domain.NotificationSettings.Mapping
{
    class SettingsMappingProfile:Profile
    {
        public SettingsMappingProfile()
        {
            CreateMap<DataLayer.Entities.NotificationSettings, SettingOutput>()
                .ForMember(dest => dest.ParameterName, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Min, opts => opts.MapFrom(src => src.Min))
                .ForMember(dest => dest.Max, opts => opts.MapFrom(src => src.Max))
                .ReverseMap();
        }
    }
}
