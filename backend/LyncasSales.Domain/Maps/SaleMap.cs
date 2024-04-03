using AutoMapper;
using LyncasSales.Domain.Entities;
using LyncasSales.Domain.ViewModel;

namespace LyncasSales.Domain.Maps
{
    public class SaleMap : Profile
    {
        public SaleMap()
        {
            CreateMap<Sale, SaleViewModel>()
             .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
             .ForMember(dest => dest.BillingDate, opt => opt.MapFrom(src => src.BillingDate))
             .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.SaleDate))
             .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
             .ForMember(dest => dest.SaleId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
