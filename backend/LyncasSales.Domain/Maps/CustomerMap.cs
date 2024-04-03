using AutoMapper;
using LyncasSales.Domain.DTO;
using LyncasSales.Domain.Entities;
using LyncasSales.Domain.ViewModel;

namespace LyncasSales.Domain.Maps
{
    public class CustomerMap : Profile
    {
        public CustomerMap()
        {
            CreateMap<CreateCustomerDTO, Customer>();
            CreateMap<Customer, CreateCustomerDTO>();
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<CustomerViewModel, Customer>();

        }
    }
}
