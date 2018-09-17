﻿using AutoMapper;
using Din.Service.Clients.ResponseObjects;
using Din.Service.DTO.Content;

namespace Din.Mapping.Converters
{
    public class McCalendarConverter : ITypeConverter<McCalendarResponse, CalendarItemDto>
    {
        public CalendarItemDto Convert(McCalendarResponse source, CalendarItemDto destination, ResolutionContext context)
        {
            if (source.Downloaded)
            {

            }
            return new CalendarItemDto
            {
                Title = source.Title,
                Start = source.PhysicalRelease,
                TextColor = "#d0d2d5",
                Color = source.Downloaded? "rgba(0, 215, 124, .5)" : "rgba(180, 50, 50, .5)"
            };
        }
    }
}
