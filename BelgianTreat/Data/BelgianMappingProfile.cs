using AutoMapper;
using BelgianTreat.Data.Entities;
using BelgianTreat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelgianTreat.Data
{
    public class BelgianMappingProfile : Profile
    {
        public BelgianMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                // Needed because the member OrderId is not mapped auto
                .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
                .ReverseMap();
        }
    }
}
