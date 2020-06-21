using AutoMapper;
using Orders.Api.Models;
using Orders.Domain;

namespace Orders.Api.Mappings
{
    public class DomainToVmMappingProfile : Profile
    {
        public DomainToVmMappingProfile()
        {
            CreateMap<Order, OrderVm>();
            CreateMap<OrderLine, OrderLineVm>();
        }
    }
}