
using AutoMapper;
using LyncasSales.Domain.DTO;
using LyncasSales.Domain.Entities;
using LyncasSales.Domain.ViewModel;

namespace LyncasSales.Domain.Maps
{
    public class CreateSaleMap : Profile
    {
        public CreateSaleMap()
        {
            CreateMap<CreateSaleDTO, Sale>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore())
                 .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                 .ForMember(dest => dest.BillingDate, opt => opt.MapFrom(src => src.BillingDate))
                 .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.SaleDate));

            CreateMap<CreateItemDTO, SaleItem>();
            CreateMap<Sale, UpdateSaleDTO>();
            CreateMap<Sale, CreateSaleDTO>();
            CreateMap<UpdateSaleDTO, Sale>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<UpdateItemDTO, SaleItem>();
            
        }
    }
}
