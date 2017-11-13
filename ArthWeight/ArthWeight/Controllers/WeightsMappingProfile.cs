using ArthWeight.Data.Entities;
using ArthWeight.ViewModels;
using AutoMapper;
using System;

namespace ArthWeight.Controllers
{
    public class WeightsMappingProfile : Profile
    {
        public WeightsMappingProfile()
        {
            CreateMap<WeightEntry, WeightViewModel>();
            CreateMap<WeightViewModel, WeightEntry>()
                .ForMember(d => d.CreatedDate, opt => opt.UseValue<DateTime>(DateTime.UtcNow));
        }
    }
}
