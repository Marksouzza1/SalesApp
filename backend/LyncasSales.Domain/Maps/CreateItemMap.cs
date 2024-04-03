using AutoMapper;
using LyncasSales.Domain.DTO;
using LyncasSales.Domain.Entities;

namespace LyncasSales.Domain.Maps
{
    public class CreateItemMap : Profile
    {
        public CreateItemMap() 
        {
            CreateMap<SaleItem, CreateItemDTO>();
        }
    }
}
